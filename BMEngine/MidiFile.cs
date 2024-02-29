﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZenithEngine
{
    public class MidiFile : IDisposable
    {
        Stream MidiFileReader;
        public ushort division;
        public int trackcount;
        public ushort format;

        public int zerothTempo = 500000;

        List<long> trackBeginnings = new List<long>();
        List<uint> trackLengths = new List<uint>();

        public MidiTrack[] tracks;

        public MidiInfo info;

        public long maxTrackTime;
        public long noteCount = 0;

        public long currentSyncTime = 0;
        public double currentFlexSyncTime = 0;

        public FastList<Note> globalDisplayNotes = new FastList<Note>();
        public FastList<Tempo> globalTempoEvents = new FastList<Tempo>();
        public FastList<ColorChange> globalColorEvents = new FastList<ColorChange>();
        public FastList<PlaybackEvent> globalPlaybackEvents = new FastList<PlaybackEvent>();

        public double tempoTickMultiplier = 0;

        public int unendedTracks = 0;

        RenderSettings settings;

        public MidiFile(string filename, RenderSettings settings)
        {
            this.settings = settings;
            MidiFileReader = new StreamReader(filename).BaseStream;
            ParseHeaderChunk();
            while (MidiFileReader.Position < MidiFileReader.Length)
            {
                ParseTrackChunk();
            }
            tracks = new MidiTrack[trackcount];

            Console.WriteLine("Loading tracks into memory");
            info = new MidiInfo();
            Task LoadMidi = new Task(() => LoadAndParseAll(true), TaskCreationOptions.LongRunning | TaskCreationOptions.RunContinuationsAsynchronously);
            LoadMidi.Start();
            // LoadAndParseAll(true);
            LoadMidi.GetAwaiter().GetResult();
            // SpinWait.SpinUntil(() => loadedMidi == true);

            Console.WriteLine("Loaded!");
            Console.WriteLine("Note count: " + noteCount);
            unendedTracks = trackcount;

            info.division = division;
            info.firstTempo = zerothTempo;
            info.noteCount = noteCount;
            info.tickLength = maxTrackTime;
            info.trackCount = trackcount;
            tempoTickMultiplier = (double)division / 500000 * 1000;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void AssertText(string text)
        {
            foreach (char c in text)
            {
                if (MidiFileReader.ReadByte() != c)
                {
                    throw new Exception("Corrupt chunk headers");
                }
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        uint ReadInt32()
        {
            uint length = 0;
            // for (int i = 0; i != 4; i++)
            length = (length << 8) | (byte)MidiFileReader.ReadByte();
            length = (length << 8) | (byte)MidiFileReader.ReadByte();
            length = (length << 8) | (byte)MidiFileReader.ReadByte();
            length = (length << 8) | (byte)MidiFileReader.ReadByte();
            return length;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        ushort ReadInt16()
        {
            ushort length = 0;
            // for (int i = 0; i != 2; i++)
            length = (ushort)((length << 8) | (byte)MidiFileReader.ReadByte());
            length = (ushort)((length << 8) | (byte)MidiFileReader.ReadByte());
            return length;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void ParseHeaderChunk()
        {
            AssertText("MThd");
            uint length = ReadInt32();
            if (length != 6) throw new Exception("Header chunk size isn't 6");
            format = ReadInt16();
            ReadInt16();
            division = ReadInt16();
            if (format == 2) throw new Exception("Midi type 2 not supported");
            if (division < 0) throw new Exception("Division < 0 not supported");
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void ParseTrackChunk()
        {
            AssertText("MTrk");
            uint length = ReadInt32();
            trackBeginnings.Add(MidiFileReader.Position);
            trackLengths.Add(length);
            MidiFileReader.Position += length;
            trackcount++;
            Console.WriteLine("Track " + trackcount + ", Size " + length);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool ParseUpTo(double targetTime)
        {
            lock (globalDisplayNotes)
            {
                if (settings.timeBasedNotes)
                    for (; currentFlexSyncTime <= targetTime && settings.running; ++currentSyncTime)
                    {
                        currentFlexSyncTime += 1 / tempoTickMultiplier;
                        int ut = 0;
                        /*for (int trk = 0; trk < trackcount; ++trk)
                        {
                            var t = tracks[trk];
                            if (!t.trackEnded)
                            {
                                ut++;
                                t.Step(currentSyncTime);
                            }
                        }*/
                        foreach (var trk in tracks)
                        {
                            if (!trk.trackEnded)
                            {
                                ut++;
                                trk.Step(currentSyncTime);
                            }
                        }
                        unendedTracks = ut;
                    }
                else
                    for (; currentSyncTime <= targetTime && settings.running; ++currentSyncTime)
                    {
                        int ut = 0;
                        /*for (int trk = 0; trk < trackcount; ++trk)
                        {
                            var t = tracks[trk];
                            if (!t.trackEnded)
                            {
                                ++ut;
                                t.Step(currentSyncTime);
                            }
                        }*/
                        foreach (var trk in tracks)
                        {
                            if (!trk.trackEnded)
                            {
                                ut++;
                                trk.Step(currentSyncTime);
                            }
                        }
                        unendedTracks = ut;
                    }
                foreach (var t in tracks)
                {
                    if (!t.trackEnded) return true;
                }
                return false;
            }
        }

        public void LoadAndParseAll(bool useBufferStream = false)
        {
            long[] tracklens = new long[tracks.Length];
            int p = 0;
            List<FastList<Tempo>> tempos = new List<FastList<Tempo>>();
            if (!settings.useMultithreadToLoadMidi)
            {
                // Parallel.For(0, tracks.Length, (i) =>
                for (int i = 0, trkLength = tracks.Length; i < trkLength; ++i)
                {
                    var reader = new BufferByteReader(MidiFileReader, settings.maxTrackBufferSize, trackBeginnings[i], trackLengths[i]);
                    tracks[i] = new MidiTrack(i, reader, this, settings);
                    var t = tracks[i];
                    while (!t.trackEnded)
                    {
                        try
                        {
                            t.ParseNextEventFast();
                        }
                        catch
                        {
                            break;
                        }
                    }
                    noteCount += t.noteCount;
                    tracklens[i] = t.trackTime;
                    if (t.foundTimeSig != null)
                        info.timeSig = t.foundTimeSig;
                    if (t.zerothTempo != -1)
                    {
                        zerothTempo = t.zerothTempo;
                    }
                    lock (tempos) tempos.Add(t.TempoEvents);
                    t.Reset();
                    Console.WriteLine("Loaded track " + ++p + "/" + tracks.Length);
                    GC.Collect();
                }// );
            }
            else
            {
                Parallel.For(0, tracks.Length, (i) =>
                {
                    var reader = new BufferByteReader(MidiFileReader, settings.maxTrackBufferSize, trackBeginnings[i], trackLengths[i]);
                    tracks[i] = new MidiTrack(i, reader, this, settings);
                    var t = tracks[i];
                    while (!t.trackEnded)
                    {
                        try
                        {
                            t.ParseNextEventFast();
                        }
                        catch
                        {
                            break;
                        }
                    }
                    noteCount += t.noteCount;
                    tracklens[i] = t.trackTime;
                    if (t.foundTimeSig != null)
                        info.timeSig = t.foundTimeSig;
                    if (t.zerothTempo != -1)
                    {
                        zerothTempo = t.zerothTempo;
                    }
                    lock (tempos) tempos.Add(t.TempoEvents);
                    t.Reset();
                    Console.WriteLine("Loaded track " + ++p + "/" + tracks.Length);
                    GC.Collect();
                });
            }
            maxTrackTime = tracklens.Max();
            Console.WriteLine("Processing Tempos");
            FastList<Tempo> Tempos = new FastList<Tempo>();
            var iters = tempos.Select(t => t.GetEnumerator()).ToArray();
            bool[] unended = new bool[iters.Length];
            for (int i = 0, itersLength = iters.Length; i < itersLength; ++i) unended[i] = iters[i].MoveNext();
            while (true)
            {
                long smallest = 0;
                bool first = true;
                int id = 0;
                // for (int i = 0; i < iters.Length; ++i)
                for (int i = 0, itersLength = iters.Length; i < itersLength; ++i)
                {
                    if (!unended[i]) continue;
                    if (first)
                    {
                        smallest = iters[i].Current.pos;
                        id = i;
                        first = false;
                        continue;
                    }
                    if (iters[i].Current.pos < smallest)
                    {
                        smallest = iters[i].Current.pos;
                        id = i;
                    }
                }
                if (first)
                {
                    break;
                }
                Tempos.Add(iters[id].Current);
                unended[id] = iters[id].MoveNext();
            }

            double time = 0;
            long ticks = maxTrackTime;
            double multiplier = ((double)500000 / division) / 1000000;
            long lastt = 0;
            foreach (var t in Tempos)
            {
                var offset = t.pos - lastt;
                time += offset * multiplier;
                ticks -= offset;
                lastt = t.pos;
                multiplier = ((double)t.tempo / division) / 1000000;
            }

            time += ticks * multiplier;

            info.secondsLength = time;

            maxTrackTime = tracklens.Max();
            unendedTracks = trackcount;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            globalDisplayNotes.Unlink();
            globalTempoEvents.Unlink();
            globalColorEvents.Unlink();
            globalPlaybackEvents.Unlink();
            currentSyncTime = 0;
            currentFlexSyncTime = 0;
            unendedTracks = trackcount;
            tempoTickMultiplier = (double)division / 500000 * 1000;
            foreach (var t in tracks) t.Reset();
        }

        public void Dispose()
        {
            foreach (var t in tracks) t.Dispose();
            MidiFileReader.Dispose();
        }
    }
}
