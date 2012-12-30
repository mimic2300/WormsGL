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
        /// Segmentace pro všechny základní objekty jako kruh, kružnice, elipsa apod.
        /// </summary>
        public const int Segmentation = 24;

        #region Point

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

        #endregion Point

        #region Line

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

        #endregion Line

        #region Rectangle

        /// <summary>
        /// Vykreslí obdelník.
        /// </summary>
        /// <param name="mode">Mód pro vykreslení.</param>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        private static void BaseRectangle(BeginMode mode, float x, float y, float width, float heigth, Color4 color)
        {
            GL.Begin(mode);
            GL.Color4(color);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y + heigth);
            GL.Vertex2(x + width, y + heigth);
            GL.Vertex2(x + width, y);
            GL.End();
        }

        /// <summary>
        /// Vykreslí drátěný model obdelníku.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void WiredRectangle(float x, float y, float width, float heigth, Color4 color)
        {
            BaseRectangle(BeginMode.LineLoop, x, y, width, heigth, color);

            GL.Begin(BeginMode.Lines);
            GL.Color4(color);
            // čára z horního levého rohu dolů do pravého dolního
            GL.Vertex2(x, y);
            GL.Vertex2(x + width, y + heigth);
            // čára z dolního levého rohu do pravého horního
            GL.Vertex2(x, y + heigth);
            GL.Vertex2(x + width, y);
            GL.End();
        }

        /// <summary>
        /// Vykreslí obdelník.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void Rectangle(float x, float y, float width, float heigth, Color4 color)
        {
            BaseRectangle(BeginMode.LineLoop, x, y, width, heigth, color);
        }

        /// <summary>
        /// Vykreslí vyplněný obdelník.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void FilledRectangle(float x, float y, float width, float heigth, Color4 color)
        {
            BaseRectangle(BeginMode.Polygon, x, y, width, heigth, color);
        }

        #endregion Rectangle

        #region Circle

        /// <summary>
        /// Vykreslí kruh nebo kružnici.
        /// </summary>
        /// <param name="mode">Mód pro vykreslení.</param>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        private static void BaseCircle(BeginMode mode, float x, float y, float radius, int segmentation, Color4 color)
        {
            GL.Begin(mode);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < segmentation; i++)
            {
                angle = i * GMath.TwoPI / segmentation;
                GL.Vertex2(x + (Math.Cos(angle) * radius), y + (Math.Sin(angle) * radius));
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí kružnici.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void Circle(float x, float y, float radius, int segmentation, Color4 color)
        {
            BaseCircle(BeginMode.LineLoop, x, y, radius, segmentation, color);
        }

        /// <summary>
        /// Vykreslí kružnici.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="color">Barva.</param>
        /// <remarks>Výchozí počet bodů je 36.</remarks>
        public static void Circle(float x, float y, float radius, Color4 color)
        {
            BaseCircle(BeginMode.LineLoop, x, y, radius, Segmentation, color);
        }

        /// <summary>
        /// Vykreslí vyplněný kruh.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void FilledCircle(float x, float y, float radius, int segmentation, Color4 color)
        {
            BaseCircle(BeginMode.Polygon, x, y, radius, segmentation, color);
        }

        /// <summary>
        /// Vykreslí vyplněný kruh.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="color">Barva.</param>
        /// <remarks>Výchozí počet bodů je 36.</remarks>
        public static void FilledCircle(float x, float y, float radius, Color4 color)
        {
            BaseCircle(BeginMode.Polygon, x, y, radius, Segmentation, color);
        }

        #endregion Circle

        #region Ellipse

        /// <summary>
        /// Vykreslí elipsu.
        /// </summary>
        /// <param name="mode">Mód pro vykreslení.</param>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        private static void BaseEllipse(BeginMode mode, float x, float y, float radiusX, float radiusY, int segmentation, Color4 color)
        {
            GL.Begin(mode);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < segmentation; i++)
            {
                angle = i * GMath.TwoPI / segmentation;
                GL.Vertex2(x + (Math.Cos(angle) * radiusX), y + (Math.Sin(angle) * radiusY));
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void Ellipse(float x, float y, float radiusX, float radiusY, int segmentation, Color4 color)
        {
            BaseEllipse(BeginMode.LineLoop, x, y, radiusX, radiusY, segmentation, color);
        }

        /// <summary>
        /// Vykreslí elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="color">Barva.</param>
        public static void Ellipse(float x, float y, float radiusX, float radiusY, Color4 color)
        {
            BaseEllipse(BeginMode.LineLoop, x, y, radiusX, radiusY, Segmentation, color);
        }

        /// <summary>
        /// Vykreslí vyplněnou elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="segmentation">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void FilledEllipse(float x, float y, float radiusX, float radiusY, int segmentation, Color4 color)
        {
            BaseEllipse(BeginMode.Polygon, x, y, radiusX, radiusY, segmentation, color);
        }

        /// <summary>
        /// Vykreslí vyplněnou elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="color">Barva.</param>
        public static void FilledEllipse(float x, float y, float radiusX, float radiusY, Color4 color)
        {
            BaseEllipse(BeginMode.Polygon, x, y, radiusX, radiusY, Segmentation, color);
        }

        #endregion Ellipse

        #region Triangle

        /// <summary>
        /// Vykreslí trojúhelník.
        /// </summary>
        /// <param name="mode">Mód pro vykreslení.</param>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="z">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        private static void BaseTriangle(BeginMode mode, float cx, float cy, float z, float v, Color4 color)
        {
            GL.Begin(mode);
            GL.Color4(color);
            GL.Vertex2(cx, cy);
            GL.Vertex2(cx - z / 2, cy + v);
            GL.Vertex2(cx + z / 2, cy + v);
            GL.End();
        }

        /// <summary>
        /// Vykreslí drátěný model trojúhelníku.
        /// </summary>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="z">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void WiredTriangle(float cx, float cy, float z, float v, Color4 color)
        {
            BaseTriangle(BeginMode.LineLoop, cx, cy, z, v, color);
            Line(cx, cy, cx, cy + v, color);
        }

        /// <summary>
        /// Vykreslí trojúhelník.
        /// </summary>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="z">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void Triangle(float cx, float cy, float z, float v, Color4 color)
        {
            BaseTriangle(BeginMode.LineLoop, cx, cy, z, v, color);
        }

        /// <summary>
        /// Vykreslí vyplněný trojúhelník.
        /// </summary>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="z">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void FilledTriangle(float cx, float cy, float z, float v, Color4 color)
        {
            BaseTriangle(BeginMode.Polygon, cx, cy, z, v, color);
        }

        #endregion Triangle

        #region Pie

        /// <summary>
        /// Vykreslí koláčový graf.
        /// </summary>
        /// <param name="mode">Mód pro vykreslení.</param>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="height">Výška.</param>
        /// <param name="angle">Úhel (jak velká část se má vykreslit).</param>
        /// <param name="color">Barva.</param>
        private static void BasePie(BeginMode mode, float x, float y, float width, float height, int angle, Color4 color)
        {
            if (angle > 360)
                angle = 360;

            GL.Color4(color);
            GL.Begin(mode);
            GL.Vertex2(x, y);

            for (int i = 0; i <= angle; i += 2)
            {
                GL.Vertex2(
                    x + (float)Math.Cos(i * GMath.PI_180) * width,
                    y + (float)Math.Sin(i * GMath.PI_180) * height);
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí koláčový graf.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="height">Výška.</param>
        /// <param name="angle">Úhel (jak velká část se má vykreslit).</param>
        /// <param name="color">Barva.</param>
        public static void Pie(float x, float y, float width, float height, int angle, Color4 color)
        {
            BasePie(BeginMode.LineLoop, x, y, width, height, angle, color);
        }

        /// <summary>
        /// Vykreslí vyplněný koláčový graf.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="height">Výška.</param>
        /// <param name="angle">Úhel (jak velká část se má vykreslit).</param>
        /// <param name="color">Barva.</param>
        public static void FilledPie(float x, float y, float width, float height, int angle, Color4 color)
        {
            BasePie(BeginMode.Polygon, x, y, width, height, angle, color);
        }

        #endregion Pie

        #region Spiral

        /// <summary>
        /// Vykreslí spirálu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="rotations">Počet rotací kolem středu.</param>
        /// <param name="smooth">Úroveň vyhlazení (doporučuji 0.25f).</param>
        /// <param name="color">Barva.</param>
        public static void Spiral(float x, float y, int rotations, float smooth, Color4 color)
        {
            GL.Begin(BeginMode.LineStrip);
            GL.Color4(color);

            for (float i = 0; i < rotations * GMath.TwoPI; i += smooth)
            {
                GL.Vertex2(
                    x + (float)Math.Sin(i) * i,
                    y + (float)Math.Cos(i) * i);
            }
            GL.End();
        }

        #endregion Spiral

        #region Letters

        /// <summary>
        /// Vykreslí písmeno H.
        /// </summary>
        /// <param name="x">Pozice horního levého rohu na ose X.</param>
        /// <param name="y">Pozice horního levého rohu na ose Y.</param>
        /// <param name="color">Barva.</param>
        public static void H(float x, float y, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            GL.Vertex2(x, y);
            GL.Vertex2(x + 8, y);

            GL.Vertex2(x + 8, y + 9);
            GL.Vertex2(x + 16, y + 9);

            GL.Vertex2(x + 16, y);
            GL.Vertex2(x + 24, y);

            GL.Vertex2(x + 24, y + 28);
            GL.Vertex2(x + 16, y + 28);

            GL.Vertex2(x + 16, y + 17);
            GL.Vertex2(x + 8, y + 17);

            GL.Vertex2(x + 8, y + 28);
            GL.Vertex2(x, y + 28);

            GL.End();
        }

        /// <summary>
        /// Vykreslí písmeno I.
        /// </summary>
        /// <param name="x">Pozice horního levého rohu na ose X.</param>
        /// <param name="y">Pozice horního levého rohu na ose Y.</param>
        /// <param name="color">Barva.</param>
        public static void I(float x, float y, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            GL.Vertex2(x, y);
            GL.Vertex2(x + 18, y);

            GL.Vertex2(x + 18, y + 6);
            GL.Vertex2(x + 12, y + 6);

            GL.Vertex2(x + 12, y + 22);
            GL.Vertex2(x + 18, y + 22);

            GL.Vertex2(x + 18, y + 28);
            GL.Vertex2(x, y + 28);

            GL.Vertex2(x, y + 22);
            GL.Vertex2(x + 6, y + 22);

            GL.Vertex2(x + 6, y + 6);
            GL.Vertex2(x, y + 6);

            GL.End();
        }

        #endregion Letters
    }
}
