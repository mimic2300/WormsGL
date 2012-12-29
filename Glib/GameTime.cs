using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
