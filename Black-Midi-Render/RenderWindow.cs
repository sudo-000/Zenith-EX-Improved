﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZenithEngine;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Zenith_MIDI
{
    interface INoteRender : IDisposable
    {
        long Render(FastList<Note> notes, double midiTime);
    }

    interface IKeyboardRender : IDisposable
    {
        void Render();
    }

    class RenderWindow : GameWindow
    {
        #region Shaders
        const string postShaderVert = @"#version 330 compatibility
in vec3 position;
out vec2 UV;
void main()
{
    gl_Position = vec4(position.x * 2 - 1, position.y * 2 - 1, position.z * 2 - 1, 1.0f);
	//color = glColor;
    UV = vec2(position.x, position.y);
}
";
        const string postShaderFlipVert = @"#version 330 compatibility
in vec3 position;
out vec2 UV;
void main()
{
    gl_Position = vec4(position.x * 2 - 1, -(position.y * 2 - 1), position.z * 2 - 1, 1.0f);
	//color = glColor;
    UV = vec2(position.x, position.y);
}
";
        const string postShaderFrag = @"#version 330 compatibility
in vec2 UV;
out vec4 color;
uniform sampler2D TextureSampler;
void main()
{
    color = texture2D( TextureSampler, UV );
    color.a = sqrt(color.a);
    color.rgb /= color.a;
}
";
        const string postShaderFragDownscale = @"#version 330 compatibility
in vec2 UV;
out vec4 color;
uniform sampler2D TextureSampler;
uniform vec2 res;
uniform int factor;
void main()
{
    color = vec4(0, 0, 0, 0);
    float stepX = 1 / res.x / factor;
    float stepY = 1 / res.y / factor;
    for(int i = 0; i < factor; i += 1){
        for(int j = 0; j < factor; j += 1){
            color += texture2D(TextureSampler, UV + vec2(i * stepX, j * stepY));
        }
    }
    color /= factor * factor;
}
";

        const string postShaderFragAlphaMask = @"#version 330 compatibility
in vec2 UV;
out vec4 color;
uniform sampler2D myTextureSampler;
void main()
{
    color = texture2D( myTextureSampler, UV );
    color.x = color.w;
    color.y = color.w;
    color.z = color.w;
    color.w = 1;
}
";
        const string postShaderFragAlphaMaskColor = @"#version 330 compatibility
in vec2 UV;
out vec4 color;
uniform sampler2D myTextureSampler;
void main()
{
    color = texture2D( myTextureSampler, UV );
    color.x /= color.w;
    color.y /= color.w;
    color.z /= color.w;
    color.w = 1;
}
";
        const string postShaderFragBlackFill = @"#version 330 compatibility
in vec2 UV;
out vec4 color;
uniform sampler2D myTextureSampler;
void main()
{
    color = vec4( 0,0,0,1 );
}
";


        int MakeShader(string vert, string frag)
        {
            int _vertexObj = GL.CreateShader(ShaderType.VertexShader);
            int _fragObj = GL.CreateShader(ShaderType.FragmentShader);
            int statusCode;
            string info;

            GL.ShaderSource(_vertexObj, vert);
            GL.CompileShader(_vertexObj);
            info = GL.GetShaderInfoLog(_vertexObj);
            GL.GetShader(_vertexObj, ShaderParameter.CompileStatus, out statusCode);
            if (statusCode != 1) throw new ApplicationException(info);

            GL.ShaderSource(_fragObj, frag);
            GL.CompileShader(_fragObj);
            info = GL.GetShaderInfoLog(_fragObj);
            GL.GetShader(_fragObj, ShaderParameter.CompileStatus, out statusCode);
            if (statusCode != 1) throw new ApplicationException(info);

            int shader = GL.CreateProgram();
            GL.AttachShader(shader, _fragObj);
            GL.AttachShader(shader, _vertexObj);
            GL.LinkProgram(shader);
            return shader;
        }
        #endregion

        void loadImage(Bitmap image, int texID, bool loop, bool linear)
        {
            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            if (linear)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            }
            if (loop)
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            }
            else
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            }

            image.UnlockBits(data);
        }

        FastList<Note> globalDisplayNotes;
        FastList<Tempo> globalTempoEvents;
        FastList<ColorChange> globalColorEvents;
        FastList<PlaybackEvent> globalPlaybackEvents;
        MidiFile midi;

        RenderSettings settings;

        public double midiTime = 0;
        public long frameStartTime = 0;
        public double tempoFrameStep = 10;

        Process ffmpegvideo = new Process();
        Process ffmpegmask = new Process();
        Task lastRenderPush = null;
        Task lastRenderPushMask = null;

        GLPostbuffer finalCompositeBuff;
        GLPostbuffer downscaleBuff;
        GLPostbuffer ffmpegOutputBuff;

        int postShader;
        int postShaderFlip;
        int postShaderDownscale;
        int postShaderMask;
        int postShaderMaskColor;
        int postShaderBlackFill;

        int uDownscaleRes;
        int uDownscaleFac;

        int screenQuadBuffer;
        int screenQuadIndexBuffer;
        double[] screenQuadArray = new double[] { 0, 0, 0, 1, 1, 1, 1, 0 };
        int[] screenQuadArrayIndex = new int[] { 0, 1, 2, 3 };

        byte[] pixels;
        byte[] pixelsmask;

        Process startNewFF(string path)
        {
            Process ffmpeg = new Process();
            string args = "-hide_banner";
            if (!settings.useFullArguments)
            {
                if (settings.CustomFFmpeg)
                {
                    if (settings.includeAudio)
                    {
                        double fstep = (midi.division / lastTempo) * (1000000 / settings.fps);
                        double offset = -midiTime / fstep / settings.fps;
                        offset = Math.Round(offset * 100) / 100;
                        args = "" +
                            " -f rawvideo -s " + settings.width / settings.downscale + "x" + settings.height / settings.downscale +
                            " -pix_fmt rgb32 -r " + settings.fps + " -i -" +
                            " -itsoffset " + offset.ToString().Replace(",", ".") + " -i \"" + settings.audioPath + "\"" + " -vf vflip -pix_fmt yuv420p ";
                    }
                    else
                    {
                        args = "" +
                            " -f rawvideo -s " + settings.width / settings.downscale + "x" + settings.height / settings.downscale +
                            " -strict -2" +
                            " -pix_fmt rgb32 -r " + settings.fps + " -i -" +
                            " -vf vflip -pix_fmt yuv420p ";
                    }
                    args += settings.ffoption;
                }
                else
                {
                    if (settings.includeAudio)
                    {
                        if (settings.encoder == "png")
                        {
                            MessageBox.Show("Error: PNG Encoder could not render video with audio.", "Invalid Encoder");
                            settings.ffRender = false;
                            return ffmpeg;
                        }
                        double fstep = midi.division / lastTempo * (1000000 / settings.fps);
                        double offset = -midiTime / fstep / settings.fps;
                        offset = Math.Round(offset * 100) / 100;

                        args = "" +
                        " -f rawvideo -s " + settings.width / settings.downscale + "x" + settings.height / settings.downscale +
                        " -pix_fmt rgb32 -r " + settings.fps + " -i -" +
                        " -itsoffset " + offset.ToString().Replace(",", ".") + " -i \"" + settings.audioPath + "\"" +
                        " -vf vflip -vcodec " + settings.encoder + " -pix_fmt yuv" + settings.yuvcode + " -acodec aac";
                        if (settings.useFilterThreads) args += " -filter_threads " + settings.filterThreadsForRender;
                    }
                    else
                    {
                        if (settings.encoder == "png")
                        {
                            args = "" +
                               " -f rawvideo -s " + settings.width / settings.downscale + "x" + settings.height / settings.downscale +
                               " -strict -2" +
                               " -pix_fmt rgb32 -r " + settings.fps + " -i -" +
                               " -vf vflip -vcodec " + settings.encoder;
                            if (settings.useFilterThreads) args += " -filter_threads " + settings.filterThreadsForRender;
                        }
                        else
                        {
                            args = "" +
                               " -f rawvideo -s " + settings.width / settings.downscale + "x" + settings.height / settings.downscale +
                               " -strict -2" +
                               " -pix_fmt rgb32 -r " + settings.fps + " -i -" +
                               " -vf vflip -vcodec " + settings.encoder + " -pix_fmt yuv" + settings.yuvcode;
                            if (settings.useFilterThreads) args += " -filter_threads " + settings.filterThreadsForRender;
                        }
                        if (settings.useBitrate)
                        {
                            args += " -b:v " + settings.bitrate + "k" +
                                " -maxrate " + settings.bitrate + "k" +
                                " -minrate " + settings.bitrate + "k";
                        }
                        else
                        {
                            if (settings.encoder.Contains("nvenc"))
                            {
                                switch (settings.crfPreset)
                                {
                                    case "ultrafast":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'ultrafast'\n硬件编码器不支持ultrafast参数\nWill use argument 'fast'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    case "superfast":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'superfast'\n硬件编码器不支持superfast参数\nWill use argument 'fast'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    case "veryfast":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'veryfast'\n硬件编码器不支持veryfast参数\nWill use argument 'fast'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    case "faster":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'faster'\n硬件编码器不支持faster参数\nWill use argument 'fast'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    case "slower":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'slower'\n硬件编码器不支持slower参数\nWill use argument 'slower'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    case "veryslow":
                                        MessageBox.Show("Error: Hardware Encoder DOES NOT support 'veryslow'\n硬件编码器不支持veryslow参数\nWill use argument 'fast'\n将使用fast参数");
                                        args += " -preset:v fast -crf " + settings.crf;
                                        break;
                                    default:
                                        args += " -preset:v " + settings.crfPreset + " -crf " + settings.crf;
                                        break;
                                }
                            }
                            else
                            {
                                args += " -preset:v " + settings.crfPreset + " -crf " + settings.crf;
                            }
                        }
                    }
                }
                args += " -y \"" + path + "\"";
            }
            else
            {
                args = settings.fullArgs + " -y \"" + path + "\"";
                args += " -s" + settings.width / settings.downscale + "x" + settings.height / settings.downscale;
            }
            ffmpeg.StartInfo = new ProcessStartInfo("ffmpeg", args)
            {
                RedirectStandardInput = true,
                UseShellExecute = false,
                RedirectStandardError = !settings.ffmpegDebug
            };
            try
            {
                ffmpeg.Start();
                // if (ffmpeg.PriorityClass != ProcessPriorityClass.High) ffmpeg.PriorityClass = ProcessPriorityClass.AboveNormal;
                if (!settings.ffmpegDebug)
                {
                    Console.OpenStandardOutput();
                    Regex messageMatch = new Regex("\\[.*@.*\\]");
                    ffmpeg.ErrorDataReceived += (s, e) =>
                    {
                        if (e.Data == null) return;
                        if (e.Data.Contains("frame="))
                        {
                            settings.ffMergeInfo = e.Data;
                            if (!settings.disableInfoWriting) Console.WriteLine(e.Data);
                            // Console.SetCursorPosition(0, Console.CursorTop);
                        }
                        if (e.Data.Contains("Conversion failed!"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("An error occurred in FFMPEG, closing!");
                            Console.ResetColor();
                            settings.running = false;
                        }
                        if (messageMatch.IsMatch(e.Data))
                        {
                            Console.WriteLine(e.Data);
                        }
                    };
                    ffmpeg.BeginErrorReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error starting the ffmpeg process\nNo video will be written\n(Is ffmpeg.exe in the same folder as this program?)\n\n\"" + ex.Message + "\"");
                settings.ffRender = false;
            }
            return ffmpeg;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        CurrentRendererPointer render;
        public RenderWindow(ref CurrentRendererPointer renderer, MidiFile midi, RenderSettings settings) : base(16, 9, new GraphicsMode(new ColorFormat(8, 8, 8, 8)), "Render", GameWindowFlags.Default, DisplayDevice.Default)
        {
            Width = settings.preview_width;
            Height = settings.preview_height;
            Location = new Point((DisplayDevice.Default.Width - Width) / 2, (DisplayDevice.Default.Height - Height) / 2);
            //textEngine = new GLTextEngine();
            render = renderer;
            this.settings = settings;
            lastTempo = midi.zerothTempo;
            lock (render.renderer)
            {
                render.renderer.Tempo = 60000000.0 / midi.zerothTempo;
                midiTime = -render.renderer.NoteScreenTime;
                if (settings.timeBasedNotes) tempoFrameStep = 1000.0 / settings.fps;
                else tempoFrameStep = (midi.division / lastTempo) * (1000000 / settings.fps);
                midiTime -= tempoFrameStep * settings.renderSecondsDelay * settings.fps;
            }

            globalDisplayNotes = midi.globalDisplayNotes;
            globalTempoEvents = midi.globalTempoEvents;
            globalColorEvents = midi.globalColorEvents;
            globalPlaybackEvents = midi.globalPlaybackEvents;
            this.midi = midi;
            if (settings.ffRender)
            {
                // b g r a
                pixels = new byte[settings.width * settings.height * 4 / settings.downscale / settings.downscale];
                ffmpegvideo = startNewFF(settings.ffPath);
                if (settings.ffRenderMask)
                {
                    pixelsmask = new byte[settings.width * settings.height * 4 / settings.downscale / settings.downscale];
                    ffmpegmask = startNewFF(settings.ffMaskPath);
                }
            }

            finalCompositeBuff = new GLPostbuffer(settings.width, settings.height);
            ffmpegOutputBuff = new GLPostbuffer(settings.width / settings.downscale, settings.height / settings.downscale);
            downscaleBuff = new GLPostbuffer(settings.width / settings.downscale, settings.height / settings.downscale);

            GL.GenBuffers(1, out screenQuadBuffer);
            GL.GenBuffers(1, out screenQuadIndexBuffer);

            GL.BindBuffer(BufferTarget.ArrayBuffer, screenQuadBuffer);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                (IntPtr)(screenQuadArray.Length * 8),
                screenQuadArray,
                BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, screenQuadIndexBuffer);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                (IntPtr)(screenQuadArrayIndex.Length * 4),
                screenQuadArrayIndex,
                BufferUsageHint.StaticDraw);

            postShader = MakeShader(postShaderVert, postShaderFrag);
            postShaderFlip = MakeShader(postShaderFlipVert, postShaderFrag);
            postShaderMask = MakeShader(postShaderVert, postShaderFragAlphaMask);
            postShaderMaskColor = MakeShader(postShaderVert, postShaderFragAlphaMaskColor);
            postShaderDownscale = MakeShader(postShaderVert, postShaderFragDownscale);
            // postShaderBlackFill = MakeShader(postShaderVert, postShaderFragBlackFill);

            uDownscaleRes = GL.GetUniformLocation(postShaderDownscale, "res");
            uDownscaleFac = GL.GetUniformLocation(postShaderDownscale, "factor");
        }

        double microsecondsPerTick = 0;
        public double lastDeltaTimeOnScreen;
        bool playbackLoopStarted = false;
        void PlaybackLoop()
        {
            PlaybackEvent pe;
            int timeJump;
            long now;
            playbackLoopStarted = true;
            if (settings.ffRender) return;
            if (settings.Paused || !settings.playbackEnabled)
            {
                SpinWait.SpinUntil(() => !(settings.Paused || !settings.playbackEnabled));
            }
            KDMAPI.ResetKDMAPIStream();
            KDMAPI.SendDirectData(0x0);
            while (settings.running)
            {
                if (settings.Paused || !settings.playbackEnabled)
                {
                    SpinWait.SpinUntil(() => !(settings.Paused || !settings.playbackEnabled));
                }
                try
                {
                    if (globalPlaybackEvents.ZeroLen) continue;
                    pe = globalPlaybackEvents.Pop();
                    now = DateTime.Now.Ticks;
                    if (now - 10000000 > frameStartTime)
                    {
                        SpinWait.SpinUntil(() => now - 10000000 < frameStartTime);
                    }
                    timeJump = (int)(((pe.pos - midiTime) * microsecondsPerTick / settings.tempoMultiplier - now + frameStartTime) / 10000);
                    if (timeJump < -1000)
                        continue;
                    if (timeJump > 0)
                        Thread.Sleep(timeJump);
                    if (settings.playSound && settings.playbackEnabled)
                        try
                        {
                            KDMAPI.SendDirectData((uint)pe.val);
                        }
                        catch { continue; }
                }
                catch { continue; }
            }
            KDMAPI.ResetKDMAPIStream();
            KDMAPI.SendDirectData(0x0);
        }

        double lastTempo;
        // public double lastDeltaTimeOnScreen = 0;
        public double lastMV = 1;

        int bgTexID = -1;
        long lastBGChangeTime = -1;
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            settings.renderedFrames = 0;
            Task.Factory.StartNew(() => PlaybackLoop(), TaskCreationOptions.LongRunning);
            SpinWait.SpinUntil(() => playbackLoopStarted);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            if (!settings.timeBasedNotes) tempoFrameStep = (midi.division / lastTempo) * (1000000.0 / settings.fps);
            lock (render.renderer)
            {
                lastDeltaTimeOnScreen = render.renderer.NoteScreenTime;
                render.renderer.CurrentMidi = midi.info;
            }
            int noNoteFrames = 0;
            long lastNC = 0;
            frameStartTime = DateTime.Now.Ticks;
            if (settings.timeBasedNotes) microsecondsPerTick = 10000;
            else microsecondsPerTick = (long)(lastTempo / midi.division * 10);
            // variables
            var downscaleWidth = settings.width / settings.downscale;
            var downscaleHeight = settings.height / settings.downscale;
            var Vector = new Vector2(downscaleWidth, downscaleHeight);
            int pixelsLength = 0;
            Stream ffStream = null;
            IntPtr unmanagedPointer;
            if (settings.ffRender)
            {
                pixelsLength = pixels.Length;
                ffStream = ffmpegvideo.StandardInput.BaseStream;
            }
            lock (globalDisplayNotes)
            {
                foreach (Note n in globalDisplayNotes)
                {
                    n.meta = null;
                }
            }
            while (settings.running && (noNoteFrames < settings.fps * 5 || midi.unendedTracks != 0))
            {
                if (!settings.Paused || settings.forceReRender)
                {
                    if (settings.lastBGChangeTime != lastBGChangeTime)
                    {
                        if (settings.BGImage == null)
                        {
                            if (bgTexID != -1) GL.DeleteTexture(bgTexID);
                            bgTexID = -1;
                        }
                        else
                        {
                            if (bgTexID == -1) bgTexID = GL.GenTexture();
                            try
                            {
                                loadImage(new Bitmap(settings.BGImage), bgTexID, false, true);
                            }
                            catch
                            {
                                MessageBox.Show("Couldn't load image");
                                if (bgTexID != -1) GL.DeleteTexture(bgTexID);
                                bgTexID = -1;
                            }
                        }
                        lastBGChangeTime = settings.lastBGChangeTime;
                    }

                    lock (render.disposeQueue)
                    {
                        try
                        {
                            if (render.disposeQueue.Count != 0)
                                try
                                {
                                    while (render.disposeQueue.Count != 0)
                                    {
                                        var r = render.disposeQueue.Dequeue();
                                        if (r.Initialized)
                                        {
                                            try
                                            {
                                                r.Dispose();
                                            }
                                            catch { }
                                            //GC.Collect();
                                        }
                                    }
                                }
                                catch (InvalidOperationException) { }
                        }
                        catch
                        {
                            MessageBox.Show("There's a problem while trying to dispose other renderers", "Error");
                        }
                    }
                    lock (render.renderer)
                    {
                        try 
                        {
                            if (!render.renderer.Initialized)
                            {
                                render.renderer.Init();
                                render.renderer.NoteColors = midi.tracks.Select(t => t.trkColors).ToArray();
                                render.renderer.ReloadTrackColors();
                                render.renderer.CurrentMidi = midi.info;
                                render.renderer.NoteColors = midi.tracks.Select(t => t.trkColors).ToArray();
                                render.renderer.ReloadTrackColors();
                                render.renderer.CurrentMidi = midi.info;
                            }
                            render.renderer.Tempo = 60000000.0 / lastTempo;
                            lastDeltaTimeOnScreen = render.renderer.NoteScreenTime;
                            //if (settings.timeBasedNotes)
                            //    SpinWait.SpinUntil(() => (midi.currentFlexSyncTime > midiTime + lastDeltaTimeOnScreen + tempoFrameStep * settings.tempoMultiplier || midi.unendedTracks == 0) || !settings.running);
                            //else
                            //    SpinWait.SpinUntil(() => (midi.currentSyncTime > midiTime + lastDeltaTimeOnScreen + tempoFrameStep * settings.tempoMultiplier || midi.unendedTracks == 0) || !settings.running);
                            //if (!settings.running) break;

                            render.renderer.RenderFrame(globalDisplayNotes, midiTime, finalCompositeBuff.BufferID);
                            settings.renderedFrames++;
                            lastNC = render.renderer.LastNoteCount;
                            if (lastNC == 0 && midi.unendedTracks == 0) noNoteFrames++;
                            else noNoteFrames = 0;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("The renderer has crashed\n" + ex.Message + "\n" + ex.StackTrace);
                            break;
                        }
                    }
                }
                double mv = 1;
                if (settings.realtimePlayback)
                {
                    mv = (DateTime.Now.Ticks - frameStartTime) / microsecondsPerTick / tempoFrameStep;
                    if (mv > settings.fps / 4)
                        mv = settings.fps / 4;
                }
                lastMV = mv;
                if (!settings.Paused)
                {
                    lock (globalTempoEvents)
                    {
                        while (globalTempoEvents.GetFirst() != null && midiTime + (tempoFrameStep * mv * settings.tempoMultiplier) > globalTempoEvents.GetFirst().pos)
                        {
                            var t = globalTempoEvents.Pop();
                            if (t.tempo == 0)
                            {
                                Console.WriteLine("Zero tempo event encountered, ignoring");
                                continue;
                            }
                            // var _t = ((t.pos) - midiTime) / (tempoFrameStep * mv * settings.tempoMultiplier);
                            // mv *= 1 - _t;
                            mv *= 1 - ((t.pos) - midiTime) / (tempoFrameStep * mv * settings.tempoMultiplier);
                            if (!settings.timeBasedNotes) tempoFrameStep = ((double)midi.division / t.tempo) * (1000000.0 / settings.fps);
                            lastTempo = t.tempo;
                            midiTime = t.pos;
                        }
                    }
                    midiTime += mv * tempoFrameStep * settings.tempoMultiplier;
                }
                frameStartTime = DateTime.Now.Ticks;
                if (settings.timeBasedNotes) microsecondsPerTick = 10000;
                else microsecondsPerTick = (long)(lastTempo / midi.division * 10);

                while (globalColorEvents.First != null && globalColorEvents.First.pos < midiTime)
                {
                    var c = globalColorEvents.Pop();
                    var track = c.track;
                    if (!settings.ignoreColorEvents)
                    {
                        if (c.channel == 0x7F)
                        {
                            #region Loop
                            int i = 0;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            ++i;
                            c.track.trkColors[i].left = c.col1;
                            c.track.trkColors[i].right = c.col2;
                            c.track.trkColors[i].isDefault = false;
                            #endregion
                        }
                    }
                        else
                        {
                            c.track.trkColors[c.channel].left = c.col1;
                            c.track.trkColors[c.channel].right = c.col2;
                            c.track.trkColors[c.channel].isDefault = false;
                        }
                }

                downscaleBuff.BindBuffer();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Viewport(0, 0, downscaleWidth, downscaleHeight);
                if (bgTexID != -1)
                {
                    GL.UseProgram(postShaderFlip);
                    GL.BindTexture(TextureTarget.Texture2D, bgTexID);
                    DrawScreenQuad();
                }

                if (settings.downscale > 1)
                {
                    GL.UseProgram(postShaderDownscale);
                    GL.Uniform1(uDownscaleFac, settings.downscale);
                    GL.Uniform2(uDownscaleRes, Vector);
                }
                else
                {
                    GL.UseProgram(postShader);
                }

                finalCompositeBuff.BindTexture();
                DrawScreenQuad();

                if (settings.ffRender)
                {
                    if (ffmpegvideo.HasExited || (settings.ffRenderMask && ffmpegmask.HasExited))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("FFMPEG process closed unexpectedly!");
                        Console.WriteLine("Use 'ffmpeg debug' for more advanced info.");
                        Console.ResetColor();
                        settings.running = false;
                    }

                    //if (!settings.ffRenderMask)
                    //    GL.UseProgram(postShader);
                    //else
                    //    GL.UseProgram(postShaderMaskColor);
                    // finalCompositeBuff.BindTexture();
                    // ffmpegOutputBuff.BindBuffer();
                    // GL.Clear(ClearBufferMask.ColorBufferBit);
                    // GL.Viewport(0, 0, downscaleWidth, downscaleHeight);
                    // downscaleBuff.BindTexture();
                    // DrawScreenQuad();
                    unmanagedPointer = Marshal.AllocHGlobal(pixelsLength);
                    GL.ReadPixels(0, 0, downscaleWidth, downscaleHeight, PixelFormat.Bgra, PixelType.UnsignedByte, unmanagedPointer);
                    Marshal.Copy(unmanagedPointer, pixels, 0, pixelsLength);

                    if (lastRenderPush != null) lastRenderPush.GetAwaiter().GetResult();
                    lastRenderPush = Task.Run(() =>
                    {
                        ffStream.Write(pixels, 0, pixelsLength);
                    });
                    Marshal.FreeHGlobal(unmanagedPointer);

                    if (settings.ffRenderMask)
                    {
                        if (lastRenderPushMask != null) lastRenderPushMask.GetAwaiter().GetResult();
                        GL.UseProgram(postShaderMask);
                        ffmpegOutputBuff.BindBuffer();
                        GL.Clear(ClearBufferMask.ColorBufferBit);
                        GL.Viewport(0, 0, downscaleWidth, downscaleHeight);
                        downscaleBuff.BindTexture();
                        DrawScreenQuad();
                        unmanagedPointer = Marshal.AllocHGlobal(pixelsmask.Length);
                        GL.ReadPixels(0, 0, downscaleWidth, downscaleHeight, PixelFormat.Bgra, PixelType.UnsignedByte, unmanagedPointer);
                        Marshal.Copy(unmanagedPointer, pixelsmask, 0, pixelsmask.Length);

                        if (lastRenderPush != null) lastRenderPush.GetAwaiter().GetResult();
                        lastRenderPush = Task.Run(() =>
                        {
                            ffmpegmask.StandardInput.BaseStream.Write(pixelsmask, 0, pixelsmask.Length);
                        });
                        Marshal.FreeHGlobal(unmanagedPointer);
                    }
                }

                //GLPostbuffer.UnbindBuffers();
                //GL.Clear(ClearBufferMask.ColorBufferBit);
                //GL.UseProgram(postShader);
                //DrawScreenQuad();
                //GL.Viewport(0, 0, Width, Height);
                //downscaleBuff.BindTexture();
                //DrawScreenQuad();
                //GLPostbuffer.UnbindTextures();
                GL.UseProgram(postShader);
                GLPostbuffer.UnbindBuffers();
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Viewport(0, 0, Width, Height);
                downscaleBuff.BindTexture();
                DrawScreenQuad();
                GLPostbuffer.UnbindTextures();
                if (settings.ffRender) VSync = VSyncMode.Off;
                else if (settings.vsync) VSync = VSyncMode.On;
                else VSync = VSyncMode.Off;
                try
                {
                    SwapBuffers();
                }
                catch
                {
                    break;
                }
                ProcessEvents();
                double fr = 10000000.0 / watch.ElapsedTicks;
                settings.liveFps = (settings.liveFps * 2 + fr) / 3;
                watch.Reset();
                watch.Start();
            }
            Console.WriteLine("Left render loop");
            settings.running = false;
            if (settings.ffRender)
            {
                if (lastRenderPush != null) lastRenderPush.GetAwaiter().GetResult();
                ffmpegvideo.StandardInput.Close();
                ffmpegvideo.Close();
                if (settings.ffRenderMask)
                {
                    if (lastRenderPushMask != null) lastRenderPushMask.GetAwaiter().GetResult();
                    ffmpegmask.StandardInput.Close();
                    ffmpegmask.Close();
                }
            }
            Console.WriteLine("Disposing current renderer");
            try
            {
                render.renderer.Dispose();
            }
            catch { }
            try
            {
                Console.WriteLine("Disposing of other renderers");
                while (render.disposeQueue.Count != 0)
                {
                    var r = render.disposeQueue.Dequeue();
                    try
                    {
                        if (r.Initialized) r.Dispose();
                    }
                    catch { }
                }
            }
            catch (InvalidOperationException) { }
            Console.WriteLine("Disposed of renderers");

            globalDisplayNotes = null;
            globalTempoEvents = null;
            globalColorEvents = null;
            pixels = null;
            pixelsmask = null;
            if (settings.ffRender)
            {
                ffmpegvideo.Dispose();
                if (settings.ffRenderMask) ffmpegmask.Dispose();
            }
            ffmpegvideo = null;
            ffmpegmask = null;


            finalCompositeBuff.Dispose();
            ffmpegOutputBuff.Dispose();
            downscaleBuff.Dispose();
            finalCompositeBuff = null;
            ffmpegOutputBuff = null;
            downscaleBuff = null;

            GL.DeleteBuffers(2, new int[] { screenQuadBuffer, screenQuadIndexBuffer });

            GL.DeleteProgram(postShader);
            GL.DeleteProgram(postShaderMask);
            GL.DeleteProgram(postShaderMaskColor);
            GL.DeleteProgram(postShaderDownscale);

            midi = null;
            render.renderer = null;
            render.disposeQueue = null;
            Console.WriteLine("Closing window");

            this.Close();
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Space && !settings.ffRender) settings.Paused = !settings.Paused;
            if (e.Key == Key.Right && !settings.ffRender)
            {
                int skip = settings.skip;
                if (e.Modifiers == KeyModifiers.Control) skip += 20000;
                if (e.Modifiers == KeyModifiers.Shift) skip += 60000;
                if (settings.timeBasedNotes) midiTime += skip;
                else
                {
                    lock (midi)
                    {
                        double timeSkipped = 0;
                        for (; timeSkipped < skip; ++midiTime)
                        {
                            midi.ParseUpTo(midiTime);
                            timeSkipped += 1 / midi.tempoTickMultiplier;
                        }
                    }
                    Task.Run(() =>
                    {
                        Console.WriteLine("Resetting KDMAPI Stream...");
                        try
                        {
                            KDMAPI.ResetKDMAPIStream();
                            Console.WriteLine("Done");
                        }
                        catch
                        {
                            Console.WriteLine("Failed to reset KDMAPI Stream.");
                        }
                    });
                }
            }
            if (e.Key == Key.Enter)
            {
                if (WindowState != WindowState.Fullscreen)
                    WindowState = WindowState.Fullscreen;
                else
                    WindowState = WindowState.Normal;
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            settings.running = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            settings.running = false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        void DrawScreenQuad()
        {
            GL.Enable(EnableCap.Blend);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.BindBuffer(BufferTarget.ArrayBuffer, screenQuadBuffer);
            GL.VertexPointer(2, VertexPointerType.Double, 16, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, screenQuadIndexBuffer);
            GL.IndexPointer(IndexPointerType.Int, 1, 0);
            GL.DrawElements(PrimitiveType.Quads, 4, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Disable(EnableCap.Blend);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.Disable(EnableCap.Texture2D);
        }
    }
}