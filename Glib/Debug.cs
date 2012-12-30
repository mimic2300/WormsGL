using OpenTK;
using OpenTK.Graphics;
using QuickFont;
using System.Collections.Generic;
using System.Drawing;

namespace Glib
{
    public enum DebugType : byte
    {
        None = 0,
        Fps,
        DeltaTime
    }

    public class DebugItem
    {
        public string FormatedText;
        public object Value;
        public DebugType Type;

        public DebugItem(string formatedText, object value, DebugType type)
        {
            FormatedText = formatedText;
            Value = value;
            Type = type;
        }

        public DebugItem(string formatedText, object value)
        {
            FormatedText = formatedText;
            Value = value;
            Type = DebugType.None;
        }
    }

    /// <summary>
    /// Vypisuje na okno základní informace.
    /// </summary>
    public class Debug
    {
        private List<DebugItem> items = new List<DebugItem>();
        private GlibWindow window;
        private QFont font;

        private Vector2 startPosition { get; set; }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public Debug(GlibWindow window)
        {
            this.window = window;

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
        /// Přidá položky do listu.
        /// </summary>
        /// <param name="items">Položky.</param>
        public void Add(params DebugItem[] items)
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
            float y = startPosition.Y;

            foreach (DebugItem item in items)
            {
                switch (item.Type)
                {
                    case DebugType.Fps:
                        item.Value = window.FPS.ToString("#");
                        break;

                    case DebugType.DeltaTime:
                        item.Value = window.DeltaTime.ToString("F6");
                        break;
                }

                font.Print(string.Format(item.FormatedText, item.Value), new Vector2(startPosition.X, y));
                y += 20;
            }
        }
    }
}
