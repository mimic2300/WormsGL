using OpenTK;
using System;
using System.Diagnostics;

namespace Glib
{
    /// <summary>
    /// Výpočet FPS.
    /// </summary>
    public class FpsCounter
    {
        private Stopwatch fpsClock;
        private GlibWindow window;
        private double value;
        private double valuesCount;
        private double fps = 0;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public FpsCounter(GlibWindow window)
        {
            fpsClock = new Stopwatch();

            this.window = window;
            window.UpdateFrame += UpdateFrame;
            window.Load += new EventHandler<EventArgs>((o, e) => { Start(); });
        }

        /// <summary>
        /// Snímky za sekundu.
        /// </summary>
        public double FPS
        {
            get { return fps; }
        }

        /// <summary>
        /// Stav stopek.
        /// </summary>
        public bool IsRunning
        {
            get { return fpsClock.IsRunning; }
        }

        /// <summary>
        /// Spustí stopky.
        /// </summary>
        public void Start()
        {
            fpsClock.Start();
        }

        /// <summary>
        /// Zastaví stopky.
        /// </summary>
        public void Stop()
        {
            fpsClock.Stop();
        }

        /// <summary>
        /// Aktualizace herního okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFrame(object sender, FrameEventArgs e)
        {
            value += window.RenderFrequency;
            valuesCount++;

            if (fpsClock.ElapsedMilliseconds > 1000)
            {
                fps = value / valuesCount;
                value = 0;
                valuesCount = 0;
                fpsClock.Restart();
            }
        }
    }
}
