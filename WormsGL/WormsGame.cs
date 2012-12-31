﻿using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System.Drawing;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        private GameState gameState = GameState.Menu;
        private GameMenu menu;
        private QFont font;

        public WormsGame()
            : base("Worms", 800, 600, GraphicsMode.Default)
        {
            // nastavení okna
            VSync = VSyncMode.Off;
            Fullscreen = false;
            CursorVisible = true;
            WindowBorder = WindowBorder.Fixed;

            // initializace
            font = new QFont(new Font(DefaultFontFamily, 16f));
            menu = new GameMenu(this);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // ukončí okno
            if (KeyInput.IsKeyDown(Key.Escape))
                Exit();

            // zobrazí nebo skyje výchozí debug
            if (KeyInput.IsKeyPress(Key.F3))
                Debug.Enabled = !Debug.Enabled;

            // nastaví fullscreen v rozlišení plochy
            if (KeyInput.IsKeyPress(Key.F12))
                SetScreen(true, DesktopWidth, DesktopHeight);
        }

        protected override void OnRender(FrameEventArgs e)
        {
            switch (gameState)
            {
                // hráč vstoupí do herního menu (výchozí po spuštění app)
                case GameState.Menu:
                    menu.Render(e);
                    break;
            }

            // vykreslí vodítka myší (dočasně)
            GL.Disable(EnableCap.Texture2D);
            Draw.MouseGuides(this, Color4.Red);
        }
    }
}
