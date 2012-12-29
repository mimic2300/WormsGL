using Glib;
using Glib.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        private float rotation = 0;
        private MouseInput mouse;

        public WormsGame()
            : base("Worms", 800, 600, GraphicsMode.Default)
        {
            mouse = new MouseInput(this);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            // vytvoří projekční matici, která zarovná šířku a výšku na střed okna
            Matrix4 projection = Matrix4.CreateOrthographic(Width, Height, -1f, 1f);
            // nastaví mód pro projekti
            GL.MatrixMode(MatrixMode.Projection);
            // použije naš vzor projekce
            GL.LoadMatrix(ref projection);
            // nastaví OpenGL na modelview, který umožní použití 2D transformací bez konfliktu na projekční matici
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // čtverec se otočí o 90° za sekundu
            rotation += (float)e.Time * 90;
        }

        protected override void OnPreRender()
        {
            // není povinný, ale musí se vytvořit vlastní nastavení OpenGL
            base.OnPreRender();

            // nastaví modilview na jednotkovou matici
            GL.LoadIdentity();
        }

        protected override void OnRender(FrameEventArgs e)
        {
            // rotace viewmodel matixe podle osy Z
            GL.Rotate(rotation, Vector3.UnitZ);

            GL.Begin(BeginMode.Quads);
            GL.Color3(1f, 0f, 0f); GL.Vertex3(-Width / 4, Height / 4, 0);
            GL.Color3(0f, 1f, 0f); GL.Vertex3(-Width / 4, -Height / 4, 0);
            GL.Color3(0f, 0f, 1f); GL.Vertex3(Width / 4, -Height / 4, 0);
            GL.Color3(1f, 0f, 1f); GL.Vertex3(Width / 4, Height / 4, 0);
            GL.End();

            
        }
    }
}
