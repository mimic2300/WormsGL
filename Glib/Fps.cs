namespace Glib
{
    public static class Fps
    {
        private static double time = 0;
        private static double frames = 0;
        private static int fps = 0;

        public static int GetFps(double time)
        {
            time += time;

            if (time < 1.0)
            {
                frames++;
                return fps;
            }
            else
            {
                fps = (int)frames;
                time = 0;
                frames = 0;
                return fps;
            }
        }
    }
}
