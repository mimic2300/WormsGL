using System;
using System.Diagnostics;

namespace Glib
{
    public class FPSCounter
    {
        private Stopwatch fpsClock;
        private GlibWindow window;
        private double value;
        private double valuesCount;

        public double FPS
        {
            get;
            private set;
        }

        public FPSCounter(GlibWindow window)
        {
            fpsClock = new Stopwatch();
            this.window = window;

            window.UpdateFrame += UpdateFrame;
            window.Load += Load;
        }

        private void Load(object sender, EventArgs e)
        {
            fpsClock.Start();
        }

        private void UpdateFrame(object sender, OpenTK.FrameEventArgs e)
        {
            value += window.RenderFrequency;
            valuesCount++;

            if (fpsClock.ElapsedMilliseconds > 1000)
            {
                FPS = (value / valuesCount);
                value = valuesCount = 0;
                fpsClock.Restart();
            }
        }
    }
}
