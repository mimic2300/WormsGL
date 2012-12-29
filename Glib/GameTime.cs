using System.Diagnostics;

namespace Glib
{
    /// <summary>
    /// Herní doba.
    /// </summary>
    public class GameTime : Stopwatch
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public GameTime()
        {
        }

        /// <summary>
        /// Konstruktor, přes který se stopky spustí hned při načtení okna.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public GameTime(GlibWindow window)
        {
            window.Load += (sender, e) => { Start(); };
        }
    }
}
