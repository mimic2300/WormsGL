using OpenTK;
using OpenTK.Graphics;
using QuickFont;
using System.Collections.Generic;
using System.Drawing;

namespace Glib.Diagnostics
{
    /// <summary>
    /// Vypisuje na okno základní informace.
    /// </summary>
    public class GDebug
    {
        private List<GDebugItem> items = new List<GDebugItem>();
        private GlibWindow window;
        private QFont qFont;
        private Vector2 position;
        private int offsetY;
        private Font font;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public GDebug(GlibWindow window, Font font)
        {
            this.window = window;

            this.font = font;
            qFont = new QFont(font);

            position = new Vector2(10, 10);
            offsetY = 20;

            window.RenderEnd += RenderEnd;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public GDebug(GlibWindow window)
            : this(window, new Font(window.GlibFont, 13f))
        {
        }

        /// <summary>
        /// Nastaví pozici, ze které začíná vykreslování informací.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Nastaví nebo získá mezeru na ose Y mezi dvěma informacemi.
        /// </summary>
        public int OffsetY
        {
            get { return offsetY; }
            set { offsetY = value; }
        }

        /// <summary>
        /// Nastaví nebo získá aktuální font.
        /// </summary>
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                qFont = new QFont(font);
            }
        }

        /// <summary>
        /// Získá nebo nastaví barvu textu.
        /// </summary>
        public Color4 FontColor
        {
            get { return qFont.Options.Colour; }
            set { qFont.Options.Colour = value; }
        }

        /// <summary>
        /// Přidá položky do listu.
        /// </summary>
        /// <param name="items">Položky.</param>
        public void Add(params GDebugItem[] items)
        {
            this.items.AddRange(items);
        }

        /// <summary>
        /// Odebere všechny položky.
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Vykreslí položky na okno jako poslední.
        /// </summary>
        private void RenderEnd()
        {
            float y = Position.Y;

            foreach (GDebugItem item in items)
            {
                object[] args = new object[item.Value.Length];

                for (int i = 0; i < item.Value.Length; i++)
                {
                    args[i] = item.Value[i]();
                }

                qFont.Print(string.Format(item.FormatedText, args), new Vector2(Position.X, y));
                y += offsetY;
            }
        }
    }
}
