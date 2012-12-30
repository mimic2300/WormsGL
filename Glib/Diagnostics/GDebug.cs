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
        private Font font;
        private bool enabled = false;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        /// <param name="font">Font.</param>
        public GDebug(GlibWindow window, Font font)
        {
            this.window = window;

            Font = font;
            qFont = new QFont(Font);

            Position = new Vector2(10, 10);
            OffsetY = 20;
            Enabled = true;
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
            get;
            set;
        }

        /// <summary>
        /// Nastaví nebo získá mezeru na ose Y mezi dvěma informacemi.
        /// </summary>
        public int OffsetY
        {
            get;
            set;
        }

        /// <summary>
        /// Zapne nebo vypne zobrazování Debug informací.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value == enabled)
                    return;

                enabled = value;

                if (enabled)
                {
                    window.RenderEnd += RenderEnd;
                }
                else
                {
                    window.RenderEnd -= RenderEnd;
                }
            }
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
                y += OffsetY;
            }
        }
    }
}
