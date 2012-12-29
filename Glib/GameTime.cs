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
        public GameTime()
        {
            gameTimeWatch = new Stopwatch();
        }

        /// <summary>
        /// Konstruktor, zapne se na eventu Load.
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
            if (!gameTimeWatch.IsRunning)
                gameTimeWatch.Start();
        }
    
        /// <summary>
        /// Zastaví stopky.
        /// </summary>
        /// <param name="reset">Pokud bude true, tak se čas vyresetuje na 0.</param>
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
