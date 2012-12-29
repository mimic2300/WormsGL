using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Glib
{
    /// <summary>
    /// Kreslení.
    /// </summary>
    public static class Draw
    {
        /// <summary>
        /// Vykreslí bod.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="color">Barva.</param>
        public static void Point(float x, float y, Color4 color)
        {
            GL.Begin(BeginMode.Points);
            GL.Color4(color);
            GL.Vertex2(x, y);
            GL.End();
        }

        /// <summary>
        /// Vykreslí čáru.
        /// </summary>
        /// <param name="x1">Počáteční bod X.</param>
        /// <param name="y1">Počáteční bod Y.</param>
        /// <param name="x2">Konečný bod X.</param>
        /// <param name="y2">Konečný bod Y.</param>
        /// <param name="color">Barva.</param>
        public static void Line(float x1, float y1, float x2, float y2, Color4 color)
        {
            GL.Begin(BeginMode.Lines);
            GL.Color4(color);
            GL.Vertex2(x1, y1);
            GL.Vertex2(x2, y2);
            GL.End();
        }

        /// <summary>
        /// Vykreslí čtverec.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void Rectangle(float x, float y, float width, float heigth, Color4 color)
        {
            GL.Begin(BeginMode.Quads);
            GL.Color4(color);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y + heigth);
            GL.Vertex2(x + width, y + heigth);
            GL.Vertex2(x + width, y);
            GL.End();
        }

        /// <summary>
        /// Vykreslí kružnici.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="color">Barva.</param>
        public static void Circle(float x, float y, float radius, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            double angle = 0;
            for (int i = 0; i < 36; i++)
            {
                angle = i * 2 * Math.PI / 36;
                GL.Vertex2(x + (Math.Cos(angle) * radius), y + (Math.Sin(angle) * radius));
            }
            
            GL.End();
        }
    }
}
