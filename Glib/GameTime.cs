using System.Diagnostics;

namespace Glib
{
    /// <summary>
    /// Herní doba.
    /// </summary>
    public class GameTime
    {
        private Stopwatch watch;

        public GameTime()
        {
            watch = new Stopwatch();
        }
    }
}
