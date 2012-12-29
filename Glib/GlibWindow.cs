using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Glib
{
    /// <summary>
    /// Herní okno.
    /// </summary>
    public abstract class GlibWindow : GameWindow
    {
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
        }

        /// <summary>
        /// Pozice okna na ose X.
        /// </summary>
        public int X
        {
            get { return ClientRectangle.X; }
        }

        /// <summary>
        /// Pozice okna na ose Y.
        /// </summary>
        public int Y
        {
            get { return ClientRectangle.Y; }
        }

        /// <summary>
        /// Šířka okna.
        /// </summary>
        public int Width
        {
            get { return ClientRectangle.Width; }
        }

        /// <summary>
        /// Výška okna.
        /// </summary>
        public int Height
        {
            get { return ClientRectangle.Height; }
        }

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

            GL.Viewport(X, Y, Width, Height);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        /// <summary>
        /// Vykreslování do okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            Initialize();
            Update(e);
            Render(e);

            SwapBuffers(); // prohodí backBuffer a frontBuffer
        }

        /// <summary>
        /// Initializace openGL.
        /// </summary>
        protected virtual void Initialize()
        {
            // vyčistí barevný buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        /// <summary>
        /// Aktualizace okna.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void Update(FrameEventArgs e)
        {
        }

        /// <summary>
        /// Vykreslení do okna.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void Render(FrameEventArgs e);
    }
}
