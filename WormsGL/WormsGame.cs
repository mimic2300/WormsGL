using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System;
using System.Drawing;
using WormsGL.Properties;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        private GameState gameState = GameState.Menu;
        private Rectangle prevWindowRect;
        private GameMenu menu;
        private QFont font;

        public WormsGame()
            : base("Worms", 800, 600)
        {
            // nastavení okna
            Icon = Resources.worms;
            VSync = VSyncMode.Off;
            Fullscreen = false;
            CursorVisible = true;
            WindowBorder = WindowBorder.Fixed;

            // initializace
            font = new QFont(new Font(DefaultFontFamily, 16f));
            menu = new GameMenu(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            prevWindowRect = new Rectangle(Location.X, Location.Y, Width, Height);
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
            {
                if (!Fullscreen)
                {
                    SetScreen(true, DesktopWidth, DesktopHeight);
                }
                else
                {
                    WindowState = WindowState.Normal;
                    SetScreen(false, prevWindowRect.Width, prevWindowRect.Height);
                    Location = new Point(prevWindowRect.X, prevWindowRect.Y);
                }
            }
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
