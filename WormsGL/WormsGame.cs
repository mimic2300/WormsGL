using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
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
            font.Options.Colour = Color4.PaleGreen;

            VSync = VSyncMode.Off;
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

            if (KeyInput.IsKeyDown(Key.Escape))
                Exit();

            // čtverec se otočí o 90° za sekundu
            rotation += (float)e.Time * 90;

            if (rotation > 360)
                rotation = 0;
        }

        protected override void OnRenderBegin(FrameEventArgs e)
        {
            base.OnRenderBegin(e);

            // vypne vykreslení textůr kolem fontu (aby se zobrazilo vše ostatní)
            GL.Disable(EnableCap.Texture2D);
        }

        protected override void OnRender(FrameEventArgs e)
        {
            /*
            GL.Rotate(rotation, Vector3.UnitZ);
            GL.Begin(BeginMode.Quads);
            GL.Color3(1f, 0f, 0f); GL.Vertex3(-Width / 4, Height / 4, 0);
            GL.Color3(0f, 1f, 0f); GL.Vertex3(-Width / 4, -Height / 4, 0);
            GL.Color3(0f, 0f, 1f); GL.Vertex3(Width / 4, -Height / 4, 0);
            GL.Color3(1f, 0f, 1f); GL.Vertex3(Width / 4, Height / 4, 0);
            GL.End();*/

            // vykreslení čar myši
            Draw.Line(Mouse.X, 0, Mouse.X, Height, Color4.DarkGray);
            Draw.Line(0, Mouse.Y, Width, Mouse.Y, Color4.DarkGray);

            // testy
            Draw.Line(200, 200, 400, 200, Color4.Red);
            Draw.Point(300, 300, Color4.Red);
            Draw.Rectangle(400, 400, 25, 25, Color4.Yellow);
            Draw.Circle(300, 50, 25, Color4.Orchid);
            Draw.Triangle(200, 200, 50, 50, Color4.PaleTurquoise);
            // testy
            Draw.FilledRectangle(400, 400, 25, 25, Color4.Red);
            Draw.FilledCircle(500, 500, 50, Color4.Green);
            Draw.FilledTriangle(300, 200, 50, 50, Color4.PapayaWhip);
            Draw.FilledEllipse(500, 100, 50, 80, Color4.Green);

            font.Print(string.Format("Time: {0:hh}:{0:mm}:{0:ss}.{0:FFF}", TimeElapsed), new Vector2(10, 10));
            font.Print(string.Format("FPS: {0}", FPS.ToString("#")), new Vector2(10, 30));
            font.Print(string.Format("Mouse: {0}", MouseInput), new Vector2(10, 50));
            font.Print(string.Format("Keyboard: {0}", KeyInput), new Vector2(10, 70));
            font.Print(string.Format("Delta: {0}", e.Time.ToString("F3")), new Vector2(10, 90));
            font.Print(string.Format("Rotation: {0}", rotation.ToString("#")), new Vector2(10, 110));
            font.Print(string.Format("{0}x{1}", Mouse.X, Mouse.Y), new Vector2(Mouse.X + 5, Mouse.Y - 22));
        }
    }
}
