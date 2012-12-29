﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
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
        private bool containsWindowMouse = false;

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
            Mouse.Move += new EventHandler<MouseMoveEventArgs>((o, e) => { OnMouseMove(e); });
            Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>((o, e) => { OnMouseButtonDown(e); });
            Mouse.ButtonUp += new EventHandler<MouseButtonEventArgs>((o, e) => { OnMouseButtonUp(e); });
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
        /// Pokud se myš bude nacházet v okně.
        /// </summary>
        public bool ContainsWindowMouse
        {
            get { return containsWindowMouse; }
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
        }

        /// <summary>
        /// Vykreslování do okna.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

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

        /// <summary>
        /// Pohyb myši.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseMove(MouseMoveEventArgs e)
        {
        }

        /// <summary>
        /// Stiskne se tlačítko myši.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseButtonDown(MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Uvolní se tlačítko myši.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseButtonUp(MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// Myš se bude nacházet v okně.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            containsWindowMouse = true;
        }

        /// <summary>
        /// Myš se bude nacházet mimo okno.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            containsWindowMouse = false;
        }
    }
}
