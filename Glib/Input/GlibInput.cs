using System;

namespace Glib.Input
{
    /// <summary>
    /// Input herního okna.
    /// </summary>
    public abstract class GlibInput
    {
        private GlibWindow window;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public GlibInput(GlibWindow window)
        {
            if (window == null)
                throw new ArgumentNullException("window", "Instance pro GlibWindow je NULL.");

            this.window = window;
        }

        /// <summary>
        /// Herní okno.
        /// </summary>
        protected GlibWindow Window
        {
            get { return window; }
        }
    }
}
