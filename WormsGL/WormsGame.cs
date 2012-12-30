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
        private Color4 color = Color4.Orange;
        private float rotation = 0;
        private int textureId = 0;
        private bool pressed = false;

        public WormsGame()
            : base("Worms", 1000, 600, GraphicsMode.Default)
        {
            // adresář pro herní obsah
            Content = "Content";

            font = new QFont(Path.Combine(Content, "Comfortaa-Regular.ttf"), 16f);
            font.Options.Colour = Color4.PaleGreen;

            VSync = VSyncMode.Off;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            textureId = Util.Texture(Path.Combine(Content, "image.png"));
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

            // ukončí okno
            if (KeyInput.IsKeyDown(Key.Escape))
                Exit();

            // zrychlí rotaci
            if (KeyInput.IsKeyDown(Key.Space))
                rotation += 0.08f;

            // test KeyPress na písmeno "A"
            if (KeyInput.IsKeyPress(Key.A))
                pressed = !pressed;

            // čtverec se otočí o 90° za sekundu
            rotation += (float)e.Time * 90;

            if (rotation > 360)
            {
                rotation = 0;
            }
            else if ((int)rotation % 100 == 0)
            {
                color = GColor.RandomColor();
            }
        }

        protected override void OnRenderBegin(FrameEventArgs e)
        {
            base.OnRenderBegin(e);

            // vypne vykreslení textůr kolem fontu (aby se zobrazilo vše ostatní)
            GL.Disable(EnableCap.Texture2D);
        }

        protected override void OnRender(FrameEventArgs e)
        {
            GL.PushMatrix();
            GL.Translate(Width / 1.3f, Height / 2, 0);
            GL.Rotate(rotation, Vector3.UnitZ);
            Draw.Spiral(0, 0, 36, 1.8f, color);
            GL.PopMatrix();

            // vykreslení čar myši
            Draw.Line(Mouse.X, 0, Mouse.X, Height, Color4.DarkGray);
            Draw.Line(0, Mouse.Y, Width, Mouse.Y, Color4.DarkGray);

            // testy - normal
            Draw.Line(200, 200, 400, 200, Color4.Red);
            Draw.Point(300, 300, Color4.Red);
            Draw.Rectangle(400, 400, 25, 25, Color4.Yellow);
            Draw.Circle(300, 50, 25, Color4.Orchid);
            Draw.Circle(380, 50, 25, 6, Color4.Orchid);
            Draw.Ellipse(200, 500, 100, 50, Color4.Pink);
            Draw.Ellipse(220, 500, 60, 25, 6, Color4.Pink);
            Draw.Triangle(200, 200, 50, 50, Color4.PaleTurquoise);
            Draw.Pie(150, 150, 50, 50, 90, Color4.Lime);
            Draw.Spiral(250, 400, 8, GMath._14, Color4.Violet);
            // testy - filled
            Draw.FilledRectangle(400, 400, 25, 25, Color4.Red);
            Draw.FilledCircle(500, 500, 50, Color4.Green);
            Draw.FilledTriangle(300, 200, 25, 25, Color4.PapayaWhip);
            Draw.FilledEllipse(500, 100, 50, 80, Color4.Green);
            Draw.FilledPie(250, 150, 50, 50, 90, Color4.LimeGreen);
            // testy - wired
            Draw.WiredRectangle(10, 450, 50, 50, Color4.OrangeRed);
            Draw.WiredTriangle(40, 150, 50, 80, Color4.PaleGreen);
            // testy - letters
            Draw.H(600, 30, Color4.White);
            Draw.I(635, 30, Color4.White);

            //=========================================================
            GL.Enable(EnableCap.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(new Vector2(0, 0)); GL.Vertex3(0, 0, 0);
            GL.TexCoord2(new Vector2(0, 1)); GL.Vertex3(0, 100, 0);
            GL.TexCoord2(new Vector2(1, 1)); GL.Vertex3(100, 100, 0);
            GL.TexCoord2(new Vector2(1, 0)); GL.Vertex3(100, 0, 0);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.End();
            GL.Disable(EnableCap.Texture2D); // opet se vypne kvuli kresleni
            //=========================================================

            int offset = 270;
            font.Print(string.Format("Time: {0:hh}:{0:mm}:{0:ss}.{0:FFF}", TimeElapsed), new Vector2(10, 10 + offset));
            font.Print(string.Format("FPS: {0}", FPS.ToString("#")), new Vector2(10, 30 + offset));
            font.Print(string.Format("Mouse: {0}", MouseInput), new Vector2(10, 50 + offset));
            font.Print(string.Format("Keyboard: {0}", KeyInput), new Vector2(10, 70 + offset));
            font.Print(string.Format("Delta: {0}", e.Time.ToString("F6")), new Vector2(10, 90 + offset));
            font.Print(string.Format("Rotation: {0}", rotation.ToString("#")), new Vector2(10, 110 + offset));
            font.Print(string.Format("Pressed A: {0}", pressed), new Vector2(10, 130 + offset));
            font.Print(string.Format("{0}x{1}", Mouse.X, Mouse.Y), new Vector2(Mouse.X + 5, Mouse.Y - 22));
        }
    }
}
