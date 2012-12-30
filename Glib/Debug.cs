using OpenTK;
using OpenTK.Graphics;
using QuickFont;
using System.Collections.Generic;
using System.Drawing;

namespace Glib
{
    /// <summary>
    /// Vypisuje na okno základní informace.
    /// </summary>
    public class Debug
    {
        private LinkedList<string> texts = new LinkedList<string>();
        private QFont font;

        private Vector2 startPosition { get; set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public Debug(GlibWindow window)
        {
            font = new QFont(new Font(window.GlibFont, 13f));

            startPosition = new Vector2(10, 10);

            window.RenderEnd += RenderEnd;
        }

        /// <summary>
        /// Získá nebo nastaví barvu textu.
        /// </summary>
        public Color4 FontColor
        {
            get { return font.Options.Colour; }
            set { font.Options.Colour = value; }
        }

        /// <summary>
        /// Přidá texty do listu.
        /// </summary>
        /// <param name="texts">Texty.</param>
        public void Add(params string[] texts)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                this.texts.AddLast(texts[i]);
            }
        }

        /// <summary>
        /// Odebere všechny texty.
        /// </summary>
        public void Clear()
        {
            texts.Clear();
        }

        /// <summary>
        /// Vykreslí texty na okno jako poslední.
        /// </summary>
        private void RenderEnd()
        {
            float y = startPosition.Y;

            foreach (string str in texts)
            {
                font.Print(str, new Vector2(startPosition.X, y));
                y += 20;
            }
        }
    }
}
