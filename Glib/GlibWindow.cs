using Glib.Diagnostics;
using Glib.Input;
using Glib.Properties;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.IO;

namespace Glib
{
    /// <summary>
    /// Herní okno.
    /// </summary>
    public abstract class GlibWindow : GameWindow, IDisposable
    {
        #region Události

        /// <summary>
        /// Vykresluje vše až nakonec.
        /// </summary>
        public event Action RenderEnd;

        /// <summary>
        /// Vykresluje vše až nakonec.
        /// </summary>
        private void Do_RenderEnd()
        {
            if (RenderEnd != null)
                RenderEnd();
        }

        #endregion Události

        private string contentDirectory = Environment.CurrentDirectory;
        private KeyboardInput keyboard;
        private MouseInput mouse;
        private FpsCounter fpsCounter;
        private GameTime gameTime;
        private FontFamily defaultFontFamily;
        private GDebug debug;
        private bool begin = false;
        private float deltaTime = 0;

        #region Konstruktory

        /// <summary>
        /// Initializuje všechno potřebné pro herní okno.
        /// </summary>
        private void Init()
        {
            defaultFontFamily = Util.LoadFontFamily(Resources.SVBasicManual);
            Content = "Content";

            keyboard = new KeyboardInput(this);
            mouse = new MouseInput(this);
            fpsCounter = new FpsCounter(this);
            gameTime = new GameTime(this);
            debug = new GDebug(this);

            debug.FontColor = Color4.PaleGreen;
            debug.Add(
                new GDebugItem("Time: {0:hh}:{0:mm}:{0:ss}.{0:FFF}", () => TimeElapsed),
                new GDebugItem("FPS: {0}", () => FPS.ToString("#")),
                new GDebugItem("Delta: {0}", () => deltaTime.ToString("F6")),
                new GDebugItem("Mouse: {0}", () => MouseInput),
                new GDebugItem("Keyboard: {0}", () => KeyInput));
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public GlibWindow()
        {
            Init();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="width">Šířka okna.</param>
        /// <param name="height">Výška okna.</param>
        public GlibWindow(int width, int height)
            : base(width, height)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="title">Titulek okna.</param>
        /// <param name="width">Šířka okna.</param>
        /// <param name="height">Výška okna.</param>
        public GlibWindow(string title, int width, int height)
            : base(width, height, GraphicsMode.Default, title)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="title">Titulek okna.</param>
        /// <param name="width">Šířka okna.</param>
        /// <param name="height">Výška okna.</param>
        /// <param name="mode">Zobrazovací mód.</param>
        public GlibWindow(string title, int width, int height, GraphicsMode mode)
            : base(width, height, mode, title)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="title">Titulek okna.</param>
        /// <param name="width">Šířka okna.</param>
        /// <param name="height">Výška okna.</param>
        /// <param name="mode">Zobrazovací mód.</param>
        /// <param name="flags">Další flagy okna.</param>
        public GlibWindow(string title, int width, int height, GraphicsMode mode, GameWindowFlags flags)
            : base(width, height, mode, title, flags)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="title">Titulek okna.</param>
        /// <param name="width">Šířka okna.</param>
        /// <param name="height">Výška okna.</param>
        /// <param name="mode">Zobrazovací mód.</param>
        /// <param name="flags">Další flagy okna.</param>
        /// <param name="device">Vykreslovací ovladač.</param>
        public GlibWindow(string title, int width, int height, GraphicsMode mode, GameWindowFlags flags, DisplayDevice device)
            : base(width, height, mode, title, flags, device)
        {
            Init();
        }

        #endregion Konstruktory

        #region Vlastnosti

        /// <summary>
        /// Adresář pro herní obsah.
        /// </summary>
        public string Content
        {
            get { return contentDirectory; }
            set { contentDirectory = Path.Combine(Environment.CurrentDirectory, value); }
        }

        /// <summary>
        /// Vrací ovládání klávesnice.
        /// </summary>
        public KeyboardInput KeyInput
        {
            get { return keyboard; }
        }

        /// <summary>
        /// Vrací ovládání myši.
        /// </summary>
        public MouseInput MouseInput
        {
            get { return mouse; }
        }

        /// <summary>
        /// Snímky za sekundu.
        /// </summary>
        public double FPS
        {
            get { return fpsCounter.FPS; }
        }

        /// <summary>
        /// Čas delta.
        /// </summary>
        public float DeltaTime
        {
            get { return deltaTime; }
        }

        /// <summary>
        /// Čas, který uběhnul od zapnutí hry.
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get { return gameTime.Elapsed; }
        }

        /// <summary>
        /// Pozice okna na ose X.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public new int X
        {
            get { return ClientRectangle.X; }
        }

        /// <summary>
        /// Pozice okna na ose Y.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public new int Y
        {
            get { return ClientRectangle.Y; }
        }

        /// <summary>
        /// Šířka okna.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public new int Width
        {
            get { return ClientRectangle.Width; }
        }

        /// <summary>
        /// Výška okna.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public new int Height
        {
            get { return ClientRectangle.Height; }
        }

        /// <summary>
        /// Šířka plochy.
        /// </summary>
        public int DesktopWidth
        {
            get { return DisplayDevice.GetDisplay(0).Width; }
        }

        /// <summary>
        /// Výška plochy.
        /// </summary>
        public int DesktopHeight
        {
            get { return DisplayDevice.GetDisplay(0).Height; }
        }

        /// <summary>
        /// Nativní FontFamily z Glib API.
        /// </summary>
        public FontFamily DefaultFontFamily
        {
            get { return defaultFontFamily; }
        }

        /// <summary>
        /// Debug výpisy na okno.
        /// </summary>
        public GDebug Debug
        {
            get { return debug; }
        }

        /// <summary>
        /// Získá nebo nastaví Fullscreen.
        /// </summary>
        public bool Fullscreen
        {
            get { return (WindowState == WindowState.Fullscreen); }
            set
            {
                if (WindowState != WindowState.Fullscreen && value)
                    WindowState = WindowState.Fullscreen;
            }
        }

        #endregion Vlastnosti

        #region Funkce

        /// <summary>
        /// Zahájí souřadnicový systém podle pixelů.
        /// </summary>
        public void BeginPixelSystem()
        {
            GraphicsContext.Assert();

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();

            GL.Ortho(X, Width, Height, Y, -1.0, 1.0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();

            begin = true;
        }

        /// <summary>
        /// Ukončí souřadnicový systém podle pixelů.
        /// </summary>
        public void EndPixelSystem()
        {
            if (begin)
            {
                GraphicsContext.Assert();

                GL.MatrixMode(MatrixMode.Modelview);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();

                GL.MatrixMode(MatrixMode.Modelview);
            }
            begin = false;
        }

        /// <summary>
        /// Načte textůru z herního obsahu.
        /// </summary>
        /// <param name="filename">Název textůry.</param>
        /// <returns>Vrací 2D textůru.</returns>
        public Texture2D LoadTexture(string filename)
        {
            return Texture2D.Load(Path.Combine(Content, filename));
        }

        /// <summary>
        /// Nastaví vlastní zobrazení.
        /// </summary>
        /// <param name="fullscreen">Fullscreen.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="height">Výška.</param>
        public void SetScreen(bool fullscreen, int width, int height)
        {
            Size = new Size(width, height);
            Fullscreen = fullscreen;
        }

        #endregion Funkce

        #region Virtuální a abstraktní funkce

        /// <summary>
        /// Načtení okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color4.Black);
        }

        /// <summary>
        /// Změna rozlišení okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(X, Y, Width, Height);
        }

        /// <summary>
        /// Vykreslování do okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            deltaTime = (float)e.Time;

            OnRenderBegin(e);
            OnRender(e);
            OnRenderEnd(e);

            SwapBuffers();
        }

        /// <summary>
        /// Před-vykreslení.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderBegin(FrameEventArgs e)
        {
            // vyčistí barevný buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);

            BeginPixelSystem();
        }

        /// <summary>
        /// Po-vykreslení.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderEnd(FrameEventArgs e)
        {
            Do_RenderEnd();
            EndPixelSystem();
        }

        /// <summary>
        /// Vykreslení do okna.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void OnRender(FrameEventArgs e);

        /// <summary>
        /// Uvolní prostředky.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            fpsCounter.Stop();
            gameTime.Stop();
        }

        #endregion Virtuální a abstraktní funkce
    }
}
