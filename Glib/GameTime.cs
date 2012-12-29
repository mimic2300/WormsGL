using System;
using System.Diagnostics;

namespace Glib
{
    /// <summary>
    /// Herní doba.
    /// </summary>
    public class GameTime
    {
        private Stopwatch gameTimeWatch;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public GameTime(GlibWindow window)
        {
            gameTimeWatch = new Stopwatch();

            window.Load += (sender, e) => { Start(); };
        }

        /// <summary>
        /// Vrací dobu, která uběhla od zapnutí hry.
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get { return gameTimeWatch.Elapsed; }
        }

        /// <summary>
        /// Stav stopek.
        /// </summary>
        public bool IsRunning
        {
            get { return gameTimeWatch.IsRunning; }
        }

        /// <summary>
        /// Spustí stopky.
        /// </summary>
        public void Start()
        {
            gameTimeWatch.Start();
        }
    
        /// <summary>
        /// Zastaví stopky.
        /// </summary>
        /// <param name="reset">Pokud true, tak se cas vyresetuje na nulu.</param>
        public void Stop(bool reset = false)
        {
            if (reset)
            {
                gameTimeWatch.Reset();
            }
            else
            {
                gameTimeWatch.Stop();
            }
        }
    }
}
