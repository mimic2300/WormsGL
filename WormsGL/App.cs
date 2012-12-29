using System;

namespace WormsGL
{
    internal static class App
    {
        [STAThread]
        static void Main()
        {
            using (WormsGame game = new WormsGame())
            {
                game.Run();
            }
        }
    }
}
