using Glib;
using OpenTK;
using OpenTK.Graphics;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        public WormsGame()
            : base("Worms", 800, 600, GraphicsMode.Default)
        {
        }

        protected override void Initialize()
        {
            // není povinný, ale musí se vytvořit vlastní nastavení OpenGL
            base.Initialize();
        }

        protected override void Render(FrameEventArgs e)
        {
        }

        protected override void Update(FrameEventArgs e)
        {
            base.Update(e);
        }
    }
}
