using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using QuickFont;
using System.Collections.Generic;
using System.Drawing;

namespace WormsGL
{
    struct MenuItem
    {
        public short ID;
        public string Text;

        public MenuItem(string text, short id)
        {
            Text = text;
            ID = id;
        }
    }

    /// <summary>
    /// Herní menu.
    /// </summary>
    class GameMenu
    {
        private GlibWindow window;
        private QFont font;
        private List<MenuItem> items;
        private short activeID = 0;

        public GameMenu(GlibWindow window)
        {
            this.window = window;

            items = new List<MenuItem>();
            font = new QFont(new Font(window.GlibFont, 18f));

            AddMenuItem(
                new MenuItem("Play", 0),
                new MenuItem("Options", 1),
                new MenuItem("Score", 2),
                new MenuItem("Info", 3),
                new MenuItem("Exit", 4)
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

        public void Update()
        {
            if (window.KeyInput.IsKeyPress(Key.Down))
            {
                activeID++;

                if (activeID >= items.Count)
                    activeID = 0;
            }
            else if (window.KeyInput.IsKeyPress(Key.Up))
            {
                activeID--;

                if (activeID < 0)
                    activeID = (short)(items.Count - 1);
            }
            else if (window.KeyInput.IsKeyPress(Key.Enter))
            {
                switch (activeID)
                {
                    case 4:
                        window.Exit();
                        break;
                }
            }
        }

        public void Render()
        {
            Draw.FilledRectangle(50, 50, window.Width - 100, window.Height - 100, Color4.DeepSkyBlue);

            int y = 100;

            foreach (MenuItem item in items)
            {
                font.Options.Colour = Color4.White;
                font.Options.DropShadowActive = false;

                if (item.ID == activeID)
                {
                    font.Options.Colour = Color4.Red;
                    font.Options.DropShadowActive = true;
                }
                font.Print(item.Text, new Vector2(window.Width / 2, y));
                y += 25;
            }
        }
    }
}
