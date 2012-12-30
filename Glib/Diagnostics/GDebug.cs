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
		private QFont font;

		private Vector2 startPosition { get; set; }

		/// <summary>
		/// Konstruktor.
		/// </summary>
		/// <param name="window">Herní okno.</param>
		public GDebug(GlibWindow window)
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

			startPosition = new Vector2(10, 10);
		}

		/// <summary>
		/// Vykreslí položky na okno jako poslední.
		/// </summary>
		private void RenderEnd()
		{
			float y = startPosition.Y;

			foreach (GDebugItem item in items)
			{
				object[] args = new object[item.ValueFunction.Length];

				for (int i = 0; i < item.ValueFunction.Length; i++)
				{
					args[i] = item.ValueFunction[i].Invoke();
				}

				font.Print(string.Format(item.FormatedText, args), new Vector2(startPosition.X, y));
				y += 20;
			}
		}
	}
}
