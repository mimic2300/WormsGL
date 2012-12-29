using Glib.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace Glib
{
    /// <summary>
    /// Herní okno.
    /// </summary>
    public abstract class GlibWindow : GameWindow
    {
        private string contentDirectory = Environment.CurrentDirectory;
        private KeyboardInput keyboard;
        private MouseInput mouse;
        private FpsCounter fpsCounter;
        private bool begin = false;

        #region Konstruktory

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
            keyboard = new KeyboardInput(this);
            mouse = new MouseInput(this);
            fpsCounter = new FpsCounter(this);
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

        #endregion Funkce

        #region Virtuální a abstraktní funkce

        /// <summary>
        /// Načtení okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // barva pozadí - černá
            GL.ClearColor(0, 0, 0, 1);
        }

        /// <summary>
        /// Změna rozlišení okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // výchozí vieport
            GL.Viewport(X, Y, Width, Height);
        }

        /// <summary>
        /// Vykreslování do okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            OnRenderBegin(e);
            OnRender(e);
            OnRenderEnd(e);

            SwapBuffers(); // prohodí backBuffer a frontBuffer
        }

        /// <summary>
        /// Před-vykreslení.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderBegin(FrameEventArgs e)
        {
            // vyčistí barevný buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        /// <summary>
        /// Po-vykreslení.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRenderEnd(FrameEventArgs e)
        {
        }

        /// <summary>
        /// Vykreslení do okna.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void OnRender(FrameEventArgs e);

        #endregion Virtuální a abstraktní funkce
    }
}
