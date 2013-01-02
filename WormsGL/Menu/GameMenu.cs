using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace WormsGL.Menu
{
    struct MenuItem 
    {
        public string Text;
        public Action Action;

        public MenuItem(string text, Action action)
        {
            Text = text;
            Action = action;
        }
    }

    class GameMenu : IRenderObject
    {
        private WormsGame game;
        private QFont font;
        private QFont fontSelected;
        private List<MenuItem> items;
        private sbyte actualItem = 0; // první položka v menu
        private int submenu = 0; // hlavní menu

        private SubmenuInfo subInfo;

        public GameMenu(WormsGame game)
        {
            this.game = game;

            subInfo = new SubmenuInfo(game, this);

            items = new List<MenuItem>();
            font = new QFont(new Font(game.DefaultFontFamily, 28f));
            fontSelected = new QFont(new Font(game.DefaultFontFamily, 32f));

            AddMenuItem(
                new MenuItem("Play", null),
                new MenuItem("Options", null),
                new MenuItem("Score", null),
                new MenuItem("Info", null),
                new MenuItem("Exit", () => { game.Exit(); })
                );
        }

        public void BackToMainMenu()
        {
            submenu = 0;
        }

        public void AddMenuItem(params MenuItem[] items)
        {
            this.items.AddRange(items);
        }

        public void Clear()
        {
            items.Clear();
        }

        protected virtual void UpdateControls()
        {
            if (game.KeyInput.IsKeyPress(Key.Down))
            {
                actualItem++;

                if (actualItem >= items.Count)
                    actualItem = 0; // první položka v menu
            }
            else if (game.KeyInput.IsKeyPress(Key.Up))
            {
                actualItem--;

                if (actualItem < 0)
                    actualItem = (sbyte)(items.Count - 1); // poslední položka v menu
            }
            else if (game.KeyInput.IsKeyPress(Key.Enter) || game.KeyInput.IsKeyPress(Key.Right))
            {
                switch (items[actualItem].Text)
                {
                    case "Exit":
                        game.Exit();
                        break;

                    default:
                        submenu = actualItem;
                        break;
                }
            }

            int y = game.Height - 190;

            foreach (MenuItem item in items)
            {
                Rectangle rect = new Rectangle(20, y, 140, 35);

                if (rect.IntersectsWith(new Rectangle(game.Mouse.X, game.Mouse.Y, 1, 1)))
                {
                    actualItem = (sbyte)items.IndexOf(item);

                    if (game.MouseInput.IsMouseDown(MouseButton.Left))
                    {
                        submenu = actualItem;
                        break;
                    }
                }
                y += 35;
            }
        }

        private void DrawMenuItems(FrameEventArgs e)
        {
            int y = game.Height - 190;

            foreach (MenuItem item in items)
            {
                int x = 20;

                // aktuálně vybraná položka v menu
                if (items.IndexOf(item) == actualItem)
                {
                    GL.Disable(EnableCap.Texture2D);
                    Draw.FilledRectangle(8, y, 5, 40, Color4.OrangeRed);

                    font.Options.Colour = Color4.Orange.Alpha(0.8f);
                    font.Options.DropShadowActive = true;
                    x += 5;

                    // nastaví tučnější font
                    for (int i = 0; i < 2; i++)
                    {
                        font.Print(item.Text, new Vector2(x, y));
                        x += 1;
                    }
                }
                else
                {
                    font.Options.Colour = Color4.White;
                    font.Options.DropShadowActive = false;
                    font.Print(item.Text, new Vector2(x, y));
                }
                y += 35;
            }
        }

        public void Render(FrameEventArgs e)
        {
            // hlavní menu
            if (submenu == 0)
            {
                // nastavuje barvu pozadí
                GL.Disable(EnableCap.Texture2D);
                Draw.FilledRectangle(0, 0, game.Width, game.Height, new Color4(36, 36, 36, 255));

                UpdateControls(); // umožňuje ovládání menu
                DrawMenuItems(e);  // vykreslí položky v menu
            }
            // exit
            else if (submenu == items.Count - 1)
            {
                game.Exit();
            }
            else
            {
                switch (items[submenu].Text)
                {
                    case "Info":
                        subInfo.Render(e);
                        break;

                    default:
                        submenu = 0;
                        break;
                }
            }
        }
    }
}
