﻿using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ZenithEngine
{
    public class Note
    {
        public double start;
        public double end;
        public bool hasEnded;
        public byte channel;
        public byte key;
        public byte vel;
        public bool delete = false;
        public object meta = null;
        public int track;
        public NoteColor color;
    }

    public struct NoteColor
    {
        public Color4 left;
        public Color4 right;
        public bool isDefault;// = true;
    }

    public struct PlaybackEvent
    {
        public double pos;
        public int val;
    }

    public class Tempo
    {
        public long pos;
        public int tempo;
    }

    public class ColorChange
    {
        public double pos;
        public Color4 col1;
        public Color4 col2;
        public byte channel;
        public MidiTrack track;
    }

    public class TimeSignature
    {
        public int numerator /*{ get; internal set; }*/;
        public int denominator/* { get; internal set; }*/;
    }

    public class MidiTrack : IDisposable
    {
        public int trackID;

        public bool trackEnded = false;

        public long trackTime = 0;
        public long lastStepTime = 0;
        public double trackFlexTime = 0;
        public long noteCount = 0;
        public int zerothTempo = -1;

        byte channelPrefix = 0;

        MidiFile midi;

        public FastList<Note>[] UnendedNotes = null;
        public FastList<Tempo> Tempos = new FastList<Tempo>();

        FastList<Note> globalDisplayNotes;
        FastList<Tempo> globalTempoEvents;
        FastList<ColorChange> globalColorEvents;
        FastList<PlaybackEvent> globalPlaybackEvents;

        public NoteColor[] trkColors;
        public NoteColor[] zeroTickTrkColors;

        public TimeSignature foundTimeSig = null;

        bool readDelta = false;

        BufferByteReader reader;

        public void Reset()
        {
            if (UnendedNotes != null) foreach (var un in UnendedNotes) un.Unlink();
            reader.Reset();
            ResetColors();
            trackTime = 0;
            lastStepTime = 0;
            trackFlexTime = 0;
            trackEnded = false;
            readDelta = false;
            channelPrefix = 0;
            noteCount = 0;
            UnendedNotes = null;
        }

        public void ResetColors()
        {
            trkColors = new NoteColor[16];
            var Gray = Color4.Gray;
            for (int i = 0; i < 16; ++i)
            {
                trkColors[i] = new NoteColor() { left = Gray, right = Gray, isDefault = true };
                ++i;
                trkColors[i] = new NoteColor() { left = Gray, right = Gray, isDefault = true };
                ++i;
                trkColors[i] = new NoteColor() { left = Gray, right = Gray, isDefault = true };
                ++i;
                trkColors[i] = new NoteColor() { left = Gray, right = Gray, isDefault = true };
            }
        }

        RenderSettings settings;
        public MidiTrack(int id, BufferByteReader reader, MidiFile file, RenderSettings settings)
        {
            this.settings = settings;
            globalDisplayNotes = file.globalDisplayNotes;
            globalTempoEvents = file.globalTempoEvents;
            globalColorEvents = file.globalColorEvents;
            globalPlaybackEvents = file.globalPlaybackEvents;
            midi = file;
            this.reader = reader;
            trackID = id;
            ResetColors();

            zeroTickTrkColors = new NoteColor[16];
            // for (int i = 0; i < 16; i++) zeroTickTrkColors[i] = null;
            for (int i = 0; i < 16; ++i)
            {
                zeroTickTrkColors[i] = default;
                ++i;
                zeroTickTrkColors[i] = default;
                ++i;
                zeroTickTrkColors[i] = default;
                ++i;
                zeroTickTrkColors[i] = default;

            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        long ReadVariableLen()
        {
            byte c;
            int val = 0;
            /*for (int i = 0; i < 4; i++)
            {
                c = reader.ReadFast();
                if (c > 0x7F)
                {
                    val = (val << 7) | (c & 0x7F);
                }
                else
                {
                    val = val << 7 | c;
                    return val;
                }
            }*/
            c = reader.ReadFast();
            if (c > 0x7F)
            {
                val = (val << 7) | (c & 0x7F);
            }
            else
            {
                val = val << 7 | c;
                return val;
            }
            c = reader.ReadFast();
            if (c > 0x7F)
            {
                val = (val << 7) | (c & 0x7F);
            }
            else
            {
                val = val << 7 | c;
                return val;
            }
            c = reader.ReadFast();
            if (c > 0x7F)
            {
                val = (val << 7) | (c & 0x7F);
            }
            else
            {
                val = val << 7 | c;
                return val;
            }
            c = reader.ReadFast();
            if (c > 0x7F)
            {
                val = (val << 7) | (c & 0x7F);
            }
            else
            {
                val = val << 7 | c;
                return val;
            }
            return val;
        }

        public void Step(long time)
        {
            timebase = settings.timeBasedNotes;
            trackFlexTime += (time - lastStepTime) / midi.tempoTickMultiplier;
            lastStepTime = time;
            try
            {
                if (time >= trackTime)
                {
                    if (readDelta)
                    {
                        long d = trackTime;
                        do
                        {
                            ParseNextEvent();
                            if (trackEnded) return;
                            trackTime += ReadVariableLen();
                            readDelta = true;
                        }
                        while (trackTime == d);
                    }
                    else
                    {
                        if (trackEnded) return;
                        trackTime += ReadVariableLen();
                        readDelta = true;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                EndTrack();
            }
        }

        void EndTrack()
        {
            trackEnded = true;
            if (UnendedNotes != null)
                foreach (var un in UnendedNotes)
                {
                    var iter = un.Iterate();
                    while (iter.MoveNext(out Note n))
                    {
                        n.end = trackTime;
                        n.hasEnded = true;
                    }
                    un.Unlink();
                }
            UnendedNotes = null;
        }

        byte prevCommand = 0;
        bool timebase = false;
        /*public void ParseNextEvent()
        {
            try
            {
                if (!readDelta)
                {
                    trackTime += ReadVariableLen();
                }
                readDelta = false;

                double time = trackTime;
                if (timebase)
                    time = trackFlexTime;

                byte command = reader.ReadFast();
                if (command < 0x80)
                {
                    reader.Pushback = command;
                    command = prevCommand;
                }
                prevCommand = command;
                byte comm = (byte)(command & 0b11110000);
                if (comm == 0b10010000)
                {
                    byte channel = (byte)(command & 0b00001111);
                    byte note = reader.Read();
                    byte vel = reader.ReadFast();

                    if (settings.playbackEnabled && vel > 10)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (note << 8) | (vel << 16)
                        });
                    }
                    if (vel == 0)
                    {
                        var l = UnendedNotes[note << 4 | channel];
                        if (!l.ZeroLen)
                        {
                            Note n = l.Pop();
                            n.end = time;
                            n.hasEnded = true;
                        }
                    }
                    else
                    {
                        Note n = new Note
                        {
                            start = time,
                            key = note,
                            color = trkColors[channel],
                            channel = channel,
                            vel = vel,
                            track = trackID
                        };
                        if (UnendedNotes == null)
                        {
                            UnendedNotes = new FastList<Note>[256 * 16];
                            for (int i = 0; i < 256 * 16; ++i)
                            {
                                UnendedNotes[i] = new FastList<Note>();
                            }
                        }
                        UnendedNotes[note << 4 | channel].Add(n);
                        globalDisplayNotes.Add(n);
                    }
                }
                else if (comm == 0b10000000)
                {
                    int channel = command & 0b00001111;
                    byte note = reader.Read();
                    byte vel = reader.ReadFast();
                    var l = UnendedNotes[note << 4 | channel];
                    if (!l.ZeroLen)
                    {
                        try
                        {
                            Note n = l.Pop();

                            if (settings.playbackEnabled && n.vel > 10)
                            {
                                globalPlaybackEvents.Add(new PlaybackEvent()
                                {
                                    pos = time,
                                    val = command | (note << 8) | (vel << 16)
                                });
                            }
                            n.end = time;
                            n.hasEnded = true;
                        }
                        catch
                        { }
                    }
                }
                else if (comm == 0b10100000)
                {
                    int channel = command & 0b00001111;
                    byte note = reader.Read();
                    byte vel = reader.Read();
                    if (settings.playbackEnabled)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (note << 8) | (vel << 16)
                        });
                    }
                }
                else if (comm == 0b11000000)
                {
                    int channel = command & 0b00001111;
                    byte program = reader.Read();
                    if (settings.playbackEnabled)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (program << 8)
                        });
                    }
                }
                else if (comm == 0b11010000)
                {

                    int channel = command & 0b00001111;
                    byte pressure = reader.Read();
                    if (settings.playbackEnabled)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (pressure << 8)
                        });
                    }
                }
                else if (comm == 0b11100000)
                {
                    int channel = command & 0b00001111;
                    byte l = reader.Read();
                    byte m = reader.Read();
                    if (settings.playbackEnabled)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (l << 8) | (m << 16)
                        });
                    }
                }
                else if (comm == 0b10110000)
                {
                    int channel = command & 0b00001111;
                    byte cc = reader.Read();
                    byte vv = reader.Read();
                    if (settings.playbackEnabled)
                    {
                        globalPlaybackEvents.Add(new PlaybackEvent()
                        {
                            pos = time,
                            val = command | (cc << 8) | (vv << 16)
                        });
                    }
                }
                else if (command == 0b11110000)
                {
                    while (reader.Read() != 0b11110111) ;
                }
                else if (command == 0b11110100 || command == 0b11110001 || command == 0b11110101 || command == 0b11111001 || command == 0b11111101)
                {
                    //printf("Undefined\n");
                }
                else if (command == 0b11110010)
                {
                    int channel = command & 0b00001111;
                    byte ll = reader.Read();
                    byte mm = reader.Read();

                }
                else if (command == 0b11110011)
                {
                    byte ss = reader.Read();
                }
                else if (command == 0b11110110)
                {
                }
                else if (command == 0b11110111)
                {
                }
                else if (command == 0b11111000)
                {
                }
                else if (command == 0b11111010)
                {
                }
                else if (command == 0b11111100)
                {
                }
                else if (command == 0b11111110)
                {
                }
                else if (command == 0xFF)
                {
                    command = reader.Read();
                    if (command == 0x00)
                    {
                        if (reader.Read() != 2)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Read(); 
                        reader.Read();
                    }
                    else if (command == 0x01)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x02)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x03)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x04)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x05)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x06)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x07)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x08)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x09)
                    {
                        int size = (int)ReadVariableLen();
                        char[] text = new char[size];
                        for (int i = 0; i < size; i++)
                        {
                            text[i] = (char)reader.Read();
                        }
                        string str = new string(text);
                    }
                    else if (command == 0x0A)
                    {
                        int size = (int)ReadVariableLen();
                        byte[] data = new byte[size];
                        for (int i = 0; i < size; i++)
                        {
                            data[i] = reader.Read();
                        }
                        if (data.Length == 8 || data.Length == 12)
                        {
                            if (data[0] == 0x00 &&
                                data[1] == 0x0F)
                            {
                                Color4 col1 = new Color4(data[4], data[5], data[6], data[7]);
                                Color4 col2;
                                if (data.Length == 12)
                                    col2 = new Color4(data[8], data[9], data[10], data[11]);
                                else col2 = col1;
                                if (data[2] < 0x10 || data[2] == 0x7F)
                                {
                                    var c = new ColorChange() { pos = time, col1 = col1, col2 = col2, channel = data[2], track = this };
                                    globalColorEvents.Add(c);
                                }
                            }
                        }
                    }
                    else if (command == 0x20)
                    {
                        command = reader.Read();
                        if (command != 1)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        channelPrefix = reader.Read();
                    }
                    else if (command == 0x21)
                    {
                        command = reader.Read();
                        if (command != 1)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(1);
                        //TODO:  MIDI port
                    }
                    else if (command == 0x2F)
                    {
                        command = reader.Read();
                        if (command != 0)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        EndTrack();
                    }
                    else if (command == 0x51)
                    {
                        command = reader.Read();
                        if (command != 3)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        int btempo = 0;
                        for (int i = 0; i != 3; i++)
                            btempo = (int)((btempo << 8) | reader.Read());
                        if (!timebase)
                        {
                            Tempo t = new Tempo();
                            t.pos = trackTime;
                            t.tempo = btempo;

                            lock (globalTempoEvents)
                            {
                                globalTempoEvents.Add(t);
                            }
                        }
                        midi.tempoTickMultiplier = ((double)midi.division / btempo) * 1000;
                    }
                    else if (command == 0x54)
                    {
                        command = reader.Read();
                        if (command != 5)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(4);
                    }
                    else if (command == 0x58)
                    {
                        command = reader.Read();
                        if (command != 4)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(4);
                    }
                    else if (command == 0x59)
                    {
                        command = reader.Read();
                        if (command != 2)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(2);
                        //TODO: Key Signature
                    }
                    else if (command == 0x7F)
                    {
                        int size = (int)ReadVariableLen();
                        byte[] data = new byte[size];
                        for (int i = 0; i < size; i++)
                        {
                            data[i] = reader.Read();
                        }
                    }
                    else
                    {
                        throw new Exception("Corrupt Track");
                    }
                }
                else
                {
                    throw new Exception("Corrupt Track");
                }
            }
            catch (IndexOutOfRangeException)
            {
                EndTrack();
            }
            catch
            { }
        }*/
        public void ParseNextEvent()
        {
            try
            {

                if (!readDelta)
                {
                    trackTime += ReadVariableLen();
                }
                readDelta = false;

                double time = trackTime;
                if (timebase)
                    time = trackFlexTime;

                byte command = reader.ReadFast();
                if (command < 0x80)
                {
                    reader.Pushback = command;
                    command = prevCommand;
                }
                prevCommand = command;
                byte comm = (byte)(command & 0b11110000);
                // variables
                int channel;
                byte note;
                byte vel;
                int size;
                char[] text;
                string str;
                FastList<Note> l;
                // end
                switch (comm)
                {
                    case 0b10010000:
                        channel = (byte)(command & 0b00001111);
                        note = reader.Read();
                        vel = reader.ReadFast();

                        if (settings.playbackEnabled && vel > 10)
                        {
                            globalPlaybackEvents.Add(new PlaybackEvent()
                            {
                                pos = time,
                                val = command | (note << 8) | (vel << 16)
                            });
                        }
                        if (vel == 0)
                        {
                            l = UnendedNotes[note << 4 | channel];
                            if (!l.ZeroLen)
                            {
                                Note n = l.Pop();
                                n.end = time;
                                n.hasEnded = true;
                            }
                        }
                        else
                        {
                            Note n = new Note
                            {
                                start = time,
                                key = note,
                                color = trkColors[channel],
                                channel = (byte)channel,
                                vel = vel,
                                track = trackID
                            };
                            if (UnendedNotes == null)
                            {
                                UnendedNotes = new FastList<Note>[256 * 16];
                                for (int i = 0; i < 256 * 16; ++i)
                                {
                                    UnendedNotes[i] = new FastList<Note>();
                                }
                            }
                            UnendedNotes[note << 4 | channel].Add(n);
                            globalDisplayNotes.Add(n);
                        }
                        break;
                    case 0b10000000:
                        channel = command & 0b00001111;
                        note = reader.Read();
                        vel = reader.ReadFast();
                        l = UnendedNotes[note << 4 | channel];
                        if (!l.ZeroLen)
                        {
                            try
                            {
                                Note n = l.Pop();

                                if (settings.playbackEnabled && n.vel > 10)
                                {
                                    globalPlaybackEvents.Add(new PlaybackEvent()
                                    {
                                        pos = time,
                                        val = command | (note << 8) | (vel << 16)
                                    });
                                }
                                n.end = time;
                                n.hasEnded = true;
                            }
                            catch
                            { }
                        }
                        break;
                    case 0b10100000:
                        channel = command & 0b00001111;
                        note = reader.Read();
                        vel = reader.Read();
                        if (settings.playbackEnabled)
                        {
                            globalPlaybackEvents.Add(new PlaybackEvent()
                            {
                                pos = time,
                                val = command | (note << 8) | (vel << 16)
                            });
                        }
                        break;
                    case 0b11000000:
                        channel = command & 0b00001111;
                        byte program = reader.Read();
                        if (settings.playbackEnabled)
                        {
                            globalPlaybackEvents.Add(new PlaybackEvent()
                            {
                                pos = time,
                                val = command | (program << 8)
                            });
                        }
                        break;
                    case 0b11010000:
                            channel = command & 0b00001111;
                            byte pressure = reader.Read();
                            if (settings.playbackEnabled)
                            {
                                globalPlaybackEvents.Add(new PlaybackEvent()
                                {
                                    pos = time,
                                    val = command | (pressure << 8)
                                });
                            }
                        break;
                    case 0b11100000:
                            channel = command & 0b00001111;
                            byte _l = reader.Read();
                            byte m = reader.Read();
                            if (settings.playbackEnabled)
                            {
                                globalPlaybackEvents.Add(new PlaybackEvent()
                                {
                                    pos = time,
                                    val = command | (_l << 8) | (m << 16)
                                });
                            }
                        break;
                    case 0b10110000:
                            channel = command & 0b00001111;
                            byte cc = reader.Read();
                            byte vv = reader.Read();
                            if (settings.playbackEnabled)
                            {
                                globalPlaybackEvents.Add(new PlaybackEvent()
                                {
                                    pos = time,
                                    val = command | (cc << 8) | (vv << 16)
                                });
                            }
                            break;
                    default:
                        switch (command)
                        {
                            case 0b11110000:
                                while (reader.Read() != 0b11110111) ;
                                break;
                            case 0b11110100:
                            case 0b11110001:
                            case 0b11110101:
                            case 0b11111001:
                            case 0b11111101:
                                break;
                            case 0b11110010:
                                channel = command & 0b00001111;
                                byte ll = reader.Read();
                                byte mm = reader.Read();
                                break;
                            case 0b11110011:
                                byte ss = reader.Read();
                                break;
                            case 0b11110110:
                            case 0b11110111:
                            case 0b11111000:
                            case 0b11111010:
                            case 0b11111100:
                            case 0b11111110:
                                break;
                            case 0xFF:
                                command = reader.Read();
                                byte[] data;
                                switch (command)
                                {
                                    case 0x00:
                                        if (reader.Read() != 2)
                                        {
                                            throw new Exception("Corrupt Track");
                                        }
                                        reader.Read();
                                        reader.Read();
                                        break;
                                    case 0x01:
                                    case 0x02:
                                    case 0x03:
                                    case 0x04:
                                    case 0x05:
                                    case 0x06:
                                    case 0x07:
                                    case 0x08:
                                    case 0x09:
                                        size = (int)ReadVariableLen();
                                        text = new char[size];
                                        for (int i = 0; i < size; ++i)
                                        {
                                            text[i] = (char)reader.Read();
                                        }
                                        str = new string(text);
                                        break;
                                    case 0x0A:
                                        size = (int)ReadVariableLen();
                                        data = new byte[size];
                                        for (int i = 0; i < size; i++)
                                        {
                                            data[i] = reader.Read();
                                        }
                                        if (data.Length == 8 || data.Length == 12)
                                        {
                                            if (data[0] == 0x00 &&
                                                data[1] == 0x0F)
                                            {
                                                Color4 col1 = new Color4(data[4], data[5], data[6], data[7]);
                                                Color4 col2;
                                                if (data.Length == 12)
                                                    col2 = new Color4(data[8], data[9], data[10], data[11]);
                                                else col2 = col1;
                                                if (data[2] < 0x10 || data[2] == 0x7F)
                                                {
                                                    var c = new ColorChange() { pos = time, col1 = col1, col2 = col2, channel = data[2], track = this };
                                                    globalColorEvents.Add(c);
                                                }
                                            }
                                        }
                                        break;
                                    case 0x20:
                                            command = reader.Read();
                                            if (command != 1)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            channelPrefix = reader.Read();
                                        break;
                                    case 0x21:
                                            command = reader.Read();
                                            if (command != 1)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(1);
                                        //TODO:  MIDI port
                                        break;
                                    case 0x2F:
                                            command = reader.Read();
                                            if (command != 0)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            EndTrack();
                                        break;
                                    case 0x51:
                                        command = reader.Read();
                                        if (command != 3)
                                        {
                                            throw new Exception("Corrupt Track");
                                        }
                                        int btempo = 0;
                                        btempo = (btempo << 8) | reader.Read();
                                        btempo = (btempo << 8) | reader.Read();
                                        btempo = (btempo << 8) | reader.Read();
                                        if (!timebase)
                                        {
                                            Tempo t = new Tempo
                                            {
                                                pos = trackTime,
                                                tempo = btempo
                                            };
                                            lock (globalTempoEvents)
                                            {
                                                globalTempoEvents.Add(t);
                                            }
                                        }
                                        midi.tempoTickMultiplier = ((double)midi.division / btempo) * 1000;
                                        break;
                                    case 0x54:
                                            command = reader.Read();
                                            if (command != 5)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(4);
                                        break;
                                    case 0x58:
                                            command = reader.Read();
                                            if (command != 4)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(4);
                                        break;
                                    case 0x59:
                                            command = reader.Read();
                                            if (command != 2)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(2);
                                        //TODO: Key Signature
                                        break;
                                    case 0x7F:
                                            size = (int)ReadVariableLen();
                                            data = new byte[size];
                                            for (int i = 0; i < size; i++)
                                            {
                                                data[i] = reader.Read();
                                            }
                                        break;
                                    default:
                                        throw new Exception("Corrupt Track");
                                }
                                break;
                            default:
                                throw new Exception("Corrupt Track");
                        }
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                EndTrack();
            }
            catch { }
        }

        public FastList<Tempo> TempoEvents = new FastList<Tempo>();
        /*public void ParseNextEventFast()
        {
            try
            {
                trackTime += ReadVariableLen();
                byte command = reader.Read();
                if (command < 0x80)
                {
                    reader.Pushback = command;
                    command = prevCommand;
                }
                prevCommand = command;
                byte comm = (byte)(command & 0b11110000);
                if (comm == 0b10010000)
                {
                    byte channel = (byte)(command & 0b00001111);
                    reader.Skip(1);
                    byte vel = reader.Read();
                    if (vel != 0)
                        noteCount++;
                }
                else if (comm == 0b10000000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(2);
                }
                else if (comm == 0b10100000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(2);
                }
                else if (comm == 0b11000000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(1);
                }
                else if (comm == 0b11010000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(1);
                }
                else if (comm == 0b11100000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(2);
                }
                else if (comm == 0b10110000)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(2);
                }
                else if (command == 0b11110000)
                {
                    while (reader.Read() != 0b11110111) ;
                }
                else if (command == 0b11110100 || command == 0b11110001 || command == 0b11110101 || command == 0b11111001 || command == 0b11111101)
                {
                    //printf("Undefined\n");
                }
                else if (command == 0b11110010)
                {
                    int channel = command & 0b00001111;
                    reader.Skip(2);

                }
                else if (command == 0b11110011)
                {
                    byte ss = reader.Read();
                }
                else if (command == 0b11110110)
                {
                }
                else if (command == 0b11110111)
                {
                }
                else if (command == 0b11111000)
                {
                }
                else if (command == 0b11111010)
                {
                }
                else if (command == 0b11111100)
                {
                }
                else if (command == 0b11111110)
                {
                }
                else if (command == 0xFF)
                {
                    command = reader.Read();
                    if (command == 0x00)
                    {
                        if (reader.Read() != 2)
                        {
                            throw new Exception("Corrupt Track");
                        }
                    }
                    else if (command >= 0x01 &&
                            command <= 0x0A)
                    {
                        int size = (int)ReadVariableLen();
                        if (command != 0x0A || trackTime != 0)
                        {
                            reader.Skip(size);
                        }
                        else
                        {
                            byte[] data = new byte[size];
                            for (int i = 0; i < size; i++)
                            {
                                data[i] = reader.Read();
                            }
                            if (data.Length == 8 || data.Length == 12)
                            {
                                if (data[0] == 0x00 &&
                                    data[1] == 0x0F)
                                {
                                    Color4 col1 = new Color4(data[4], data[5], data[6], data[7]);
                                    Color4 col2;
                                    if (data.Length == 12)
                                        col2 = new Color4(data[8], data[9], data[10], data[11]);
                                    else col2 = col1;
                                    if (data[2] < 0x10)
                                    {
                                        zeroTickTrkColors[data[2]] = new NoteColor() { left = col1, right = col2 };
                                    }
                                    else if (data[2] == 0x7F)
                                    {
                                        for (int i = 0; i < 16; i++)
                                            zeroTickTrkColors[i] = new NoteColor() { left = col1, right = col2 };
                                    }
                                }
                            }
                        }
                    }
                    else if (command == 0x20)
                    {
                        command = reader.Read();
                        if (command != 1)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        channelPrefix = reader.Read();
                    }
                    else if (command == 0x21)
                    {
                        command = reader.Read();
                        if (command != 1)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(1);
                        //TODO:  MIDI port
                    }
                    else if (command == 0x2F)
                    {
                        command = reader.Read();
                        if (command != 0)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        EndTrack();
                    }
                    else if (command == 0x51)
                    {
                        command = reader.Read();
                        if (command != 3)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        int btempo = 0;
                        for (int i = 0; i != 3; i++)
                            btempo = (int)((btempo << 8) | reader.Read());
                        if (trackTime == 0)
                        {
                            zerothTempo = btempo;
                        }

                        Tempo t = new Tempo();
                        t.pos = trackTime;
                        t.tempo = btempo;
                        TempoEvents.Add(t);
                    }
                    else if (command == 0x54)
                    {
                        command = reader.Read();
                        if (command != 5)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(4);
                    }
                    else if (command == 0x58)
                    {
                        command = reader.Read();
                        if (command != 4)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        int nn = reader.ReadFast();
                        int dd = reader.ReadFast();
                        dd = (int)Math.Pow(2, dd);
                        foundTimeSig = new TimeSignature() { numerator = nn, denominator = dd };
                        reader.Skip(2);
                    }
                    else if (command == 0x59)
                    {
                        command = reader.Read();
                        if (command != 2)
                        {
                            throw new Exception("Corrupt Track");
                        }
                        reader.Skip(2);
                        //TODO: Key Signature
                    }
                    else if (command == 0x7F)
                    {
                        int size = (int)ReadVariableLen();
                        reader.Skip(size);
                    }
                    else
                    {
                        throw new Exception("Corrupt Track");
                    }
                }
                else
                {
                    throw new Exception("Corrupt Track");
                }
            }
            catch (IndexOutOfRangeException)
            {
                EndTrack();
            }
            catch
            { }
        }*/
        public void ParseNextEventFast()
        {
            try
            {
                trackTime += ReadVariableLen();
                byte command = reader.Read();
                if (command < 0x80)
                {
                    reader.Pushback = command;
                    command = prevCommand;
                }
                prevCommand = command;
                byte comm = (byte)(command & 0b11110000);
                int channel;
                byte ss;
                switch (comm)
                {
                    case 0b10010000:
                        channel = command & 0b00001111;
                        reader.Skip(1);
                        byte vel = reader.Read();
                        if (vel != 0)
                            ++noteCount;
                        break;
                    case 0b10000000:
                        channel = command & 0b00001111;
                        reader.Skip(2);
                        break;
                    case 0b10100000:
                        channel = command & 0b00001111;
                        reader.Skip(2);
                        break;
                    case 0b11000000:
                        channel = command & 0b00001111;
                        reader.Skip(1);
                        break;
                    case 0b11010000:
                        channel = command & 0b00001111;
                        reader.Skip(1);
                        break;
                    case 0b11100000:
                        channel = command & 0b00001111;
                        reader.Skip(2);
                        break;
                    case 0b10110000:
                        channel = command & 0b00001111;
                        reader.Skip(2);
                        break;
                    default:
                        switch (command)
                        {
                            case 0b11110000:
                                while (reader.Read() != 0b11110111) ;
                                break;
                            case 0b11110100:
                                break;
                            case 0b11110001:
                                break;
                            case 0b11110101:
                                break;
                            case 0b11111001:
                                break;
                            case 0b11111101:
                                break;
                            case 0b11110010:
                                channel = command & 0b00001111;
                                reader.Skip(2);
                                break;
                            case 0b11110011:
                                ss = reader.Read();
                                break;
                            case 0b11110110:
                                break;
                            case 0b11110111:
                                break;
                            case 0b11111000:
                                break;
                            case 0b11111010:
                                break;
                            case 0b11111100:
                                break;
                            case 0b11111110:
                                break;
                            case 0xFF:
                                command = reader.Read();
                                bool hasMatched = false;
                                if (command == 0x00)
                                {
                                    if (reader.Read() != 2)
                                    {
                                        throw new Exception("Corrupt Track");
                                    }
                                    hasMatched = true;
                                }
                                else if (command >= 0x01 && command <= 0x0A)
                                {
                                    int size = (int)ReadVariableLen();
                                    if (command != 0x0A || trackTime != 0)
                                    {
                                        reader.Skip(size);
                                    }
                                    else
                                    {
                                        byte[] data = new byte[size];
                                        for (int i = 0; i < size; i++)
                                        {
                                            data[i] = reader.Read();
                                        }
                                        if (data.Length == 8 || data.Length == 12)
                                        {
                                            if (data[0] == 0x00 &&
                                                data[1] == 0x0F)
                                            {
                                                Color4 col1 = new Color4(data[4], data[5], data[6], data[7]);
                                                Color4 col2;
                                                if (data.Length == 12)
                                                    col2 = new Color4(data[8], data[9], data[10], data[11]);
                                                else col2 = col1;
                                                if (data[2] < 0x10)
                                                {
                                                    zeroTickTrkColors[data[2]] = new NoteColor() { left = col1, right = col2 };
                                                }
                                                else if (data[2] == 0x7F)
                                                {
                                                    for (int i = 0; i < 16; i++)
                                                        zeroTickTrkColors[i] = new NoteColor() { left = col1, right = col2 };
                                                }
                                            }
                                        }
                                    }
                                    hasMatched = true;
                                }
                                if (!hasMatched)
                                {
                                    switch (command)
                                    {
                                        case 0x20:
                                            command = reader.Read();
                                            if (command != 1)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            channelPrefix = reader.Read();
                                            break;
                                        case 0x21:
                                            command = reader.Read();
                                            if (command != 1)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(1);
                                            break;
                                        //TODO:  MIDI port
                                        case 0x2F:
                                            command = reader.Read();
                                            if (command != 0)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            EndTrack();
                                            break;
                                        case 0x51:
                                            command = reader.Read();
                                            if (command != 3)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            int btempo = 0;
                                            for (int i = 0; i != 3; i++)
                                                btempo = (btempo << 8) | reader.Read();
                                            if (trackTime == 0)
                                            {
                                                zerothTempo = btempo;
                                            }

                                            Tempo t = new Tempo
                                            {
                                                pos = trackTime,
                                                tempo = btempo
                                            };
                                            TempoEvents.Add(t);
                                            break;

                                        case 0x54:
                                            command = reader.Read();
                                            if (command != 5)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(4);
                                            break;
                                        case 0x58:
                                            command = reader.Read();
                                            if (command != 4)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            int nn = reader.ReadFast();
                                            int dd = reader.ReadFast();
                                            dd = (int)Math.Pow(2, dd);
                                            foundTimeSig = new TimeSignature() { numerator = nn, denominator = dd };
                                            reader.Skip(2);
                                            break;
                                        case 0x59:
                                            command = reader.Read();
                                            if (command != 2)
                                            {
                                                throw new Exception("Corrupt Track");
                                            }
                                            reader.Skip(2);
                                            //TODO: Key Signature
                                            break;
                                        case 0x7F:
                                            int size = (int)ReadVariableLen();
                                            reader.Skip(size);
                                            break;
                                        default:
                                            throw new Exception("Corrupt Track");
                                            // break;
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                EndTrack();
            }
            catch { }
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
