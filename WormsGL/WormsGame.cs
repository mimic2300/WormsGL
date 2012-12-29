using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using System;
using System.IO;

namespace WormsGL
{
    internal class WormsGame : GlibWindow
    {
        private QFont font;
        private float rotation = 0;

        public WormsGame()
            : base("Worms", 800, 600, GraphicsMode.Default)
        {
            // adresář pro herní obsah
            Content = "Content";
            font = new QFont(Path.Combine(Content, "Comfortaa-Regular.ttf"), 16f);
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
            // vypne vykreslení textůr, aby se zobrazil font (textůry přesune jakoby na pozadí)
            GL.Disable(EnableCap.Texture2D);
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

            QFont.Begin();
            font.Print(string.Format("Mouse: x={0}, y={1}, z={2}", Mouse.X, Mouse.Y, Mouse.Wheel), new Vector2(10, 10));
            QFont.End();
        }
    }
}
