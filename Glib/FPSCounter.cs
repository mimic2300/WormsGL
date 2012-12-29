using OpenTK;
using System.Diagnostics;

namespace Glib
{
    /// <summary>
    /// Výpočet FPS.
    /// </summary>
    public class FpsCounter : Stopwatch
    {
        private double frames = 0;
        private double fps = 0;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public FpsCounter(GlibWindow window)
        {
            window.UpdateFrame += UpdateFrame;
            window.Load += (sender, e) => { Start(); };
        }

        /// <summary>
        /// Snímky za sekundu.
        /// </summary>
        public double FPS
        {
            get { return fps; }
        }

        /// <summary>
        /// Aktualizace herního okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFrame(object sender, FrameEventArgs e)
        {
            frames++;

            if (ElapsedMilliseconds > 1000)
            {
                fps = frames * 1000 / ElapsedMilliseconds;
                frames = 0;
                Restart();
            }
        }
    }
}
