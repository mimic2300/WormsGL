using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System;
using System.Drawing;
using System.Windows.Forms;
using WormsGL.Properties;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        private GameState gameState = GameState.Menu;
        private Rectangle prevWindowRect;
        private GameMenu menu;
        private QFont font;
        private Cursor cursor;

        public WormsGame()
            : base("Worms", 800, 600)
        {
            // nastavení okna
            Icon = Resources.worms;
            VSync = VSyncMode.Off;
            Fullscreen = false;
            CursorVisible = true;
            WindowBorder = WindowBorder.Fixed;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // základní prvky samotné okno hry
            font = new QFont(new Font(DefaultFontFamily, 16f));
            cursor = LoadCursor("cursor_normal.cur");

            // přídavky
            menu = new GameMenu(this);

            // získá poslední velikost a pozici okna
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

            // aktualizace kurzoru
            Cursor.Current = cursor;
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
            Draw.MouseGuides(this, new Color4(128, 128, 128, 128));
        }
    }
}
