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
        }

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
        public int FPS
        {
            get { return (int)Math.Round(RenderFrequency); }
        }

        /// <summary>
        /// Pozice okna na ose X.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public int X
        {
            get { return ClientRectangle.X; }
        }

        /// <summary>
        /// Pozice okna na ose Y.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public int Y
        {
            get { return ClientRectangle.Y; }
        }

        /// <summary>
        /// Šířka okna.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
        public int Width
        {
            get { return ClientRectangle.Width; }
        }

        /// <summary>
        /// Výška okna.
        /// </summary>
        /// <remarks>Překrývá původní vlastnost.</remarks>
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

            // DEBUG
            double fps = Fps.GetFps(e.Time);
            fps *= 0.99f + RenderFrequency * 0.01; 
            System.Console.WriteLine(fps);

            OnPreRender();
            OnRender(e);

            SwapBuffers(); // prohodí backBuffer a frontBuffer
        }

        /// <summary>
        /// Initializace openGL před vykreslováním (provádí se vždy).
        /// </summary>
        protected virtual void OnPreRender()
        {
            // vyčistí barevný buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        /// <summary>
        /// Vykreslení do okna.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void OnRender(FrameEventArgs e);
    }
}
