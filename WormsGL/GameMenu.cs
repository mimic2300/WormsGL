using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System.Collections.Generic;
using System.Drawing;

namespace WormsGL
{
    struct MenuItem 
    {
        public MenuState State;
        public string Text;

        public MenuItem(string text, MenuState state)
        {
            Text = text;
            State = state;
        }
    }

    class GameMenu : IRenderObject
    {
        private WormsGame game;
        private QFont font;
        private List<MenuItem> items;
        private MenuState actualState = MenuState.Play;

        public GameMenu(WormsGame game)
        {
            this.game = game;

            items = new List<MenuItem>();
            font = new QFont(new Font(game.DefaultFontFamily, 28f));

            AddMenuItem(
                new MenuItem("Play", MenuState.Play),
                new MenuItem("Options", MenuState.Options),
                new MenuItem("Score", MenuState.Score),
                new MenuItem("Info", MenuState.Info),
                new MenuItem("Exit", MenuState.Exit)
                );
        }

        public void AddMenuItem(params MenuItem[] items)
        {
            this.items.AddRange(items);
        }

        public void Clear()
        {
            items.Clear();
        }

        private void UpdateControls()
        {
            if (game.KeyInput.IsKeyPress(Key.Down))
            {
                actualState++;

                if (actualState >= MenuState.COUNT)
                    actualState = MenuState.Play;
            }
            else if (game.KeyInput.IsKeyPress(Key.Up))
            {
                actualState--;

                if (actualState < 0)
                    actualState = (MenuState)(items.Count - 1);
            }
            else if (game.KeyInput.IsKeyPress(Key.Enter))
            {
                switch (actualState)
                {
                    case MenuState.Exit:
                        game.Exit();
                        break;
                }
            }
        }

        private void DrawMenuItems()
        {
            int y = game.Height - 50;

            for (int i = items.Count - 1; i >= 0; i--)
            {
                MenuItem item = items[i];

                font.Options.Colour = Color4.White;
                font.Options.DropShadowActive = false;

                if (item.State == actualState)
                {
                    font.Options.Colour = Color4.Red;
                    font.Options.DropShadowActive = true;
                }

                font.Print(item.Text, new Vector2(20, y));
                y -= 35;
            }
        }

        public void Render(FrameEventArgs e)
        {
            // nastavuje barvu pozadí
            GL.Disable(EnableCap.Texture2D);
            Draw.FilledRectangle(0, 0, game.Width, game.Height, new Color4(36, 36, 36, 255));

            UpdateControls(); // umožňuje ovládání menu
            DrawMenuItems();  // vykreslí položky v menu
        }
    }
}
