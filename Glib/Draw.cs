﻿using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;

namespace Glib
{
    /// <summary>
    /// Kreslení.
    /// </summary>
    public static class Draw
    {
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
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="width">Šířka.</param>
        /// <param name="heigth">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void Rectangle(float x, float y, float width, float heigth, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y + heigth);
            GL.Vertex2(x + width, y + heigth);
            GL.Vertex2(x + width, y);
            GL.End();
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
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);
            GL.Vertex2(x, y);
            GL.Vertex2(x, y + heigth);
            GL.Vertex2(x + width, y + heigth);
            GL.Vertex2(x + width, y);
            GL.End();
        }

        #endregion Rectangle

        #region Circle

        /// <summary>
        /// Vykreslí kružnici.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="count">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void Circle(float x, float y, float radius, int count, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
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
        /// <param name="color">Barva.</param>
        /// <remarks>Výchozí počet bodů je 36.</remarks>
        public static void Circle(float x, float y, float radius, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            double angle = 0;
            const int count = 24;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radius), y + (Math.Sin(angle) * radius));
            }
            GL.End();
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
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);

            double angle = 0;
            const int count = 24;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radius), y + (Math.Sin(angle) * radius));
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí vyplněný kruh.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radius">Poloměr.</param>
        /// <param name="count">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void FilledCircle(float x, float y, float radius, int count, Color4 color)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radius), y + (Math.Sin(angle) * radius));
            }
            GL.End();
        }

        #endregion Circle

        #region Ellipse

        /// <summary>
        /// Vykreslí elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="count">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void Ellipse(float x, float y, float radiusX, float radiusY, int count, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
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
        /// <param name="color">Barva.</param>
        /// <remarks>Výchozí počet bodů je 36.</remarks>
        public static void Ellipse(float x, float y, float radiusX, float radiusY, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);

            double angle = 0;
            const int count = 24;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radiusX), y + (Math.Sin(angle) * radiusY));
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí vyplněnou elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="color">Barva.</param>
        /// <remarks>Výchozí počet bodů je 36.</remarks>
        public static void FilledEllipse(float x, float y, float radiusX, float radiusY, Color4 color)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);

            double angle = 0;
            const int count = 24;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radiusX), y + (Math.Sin(angle) * radiusY));
            }
            GL.End();
        }

        /// <summary>
        /// Vykreslí vyplněnou elipsu.
        /// </summary>
        /// <param name="x">Pozice X.</param>
        /// <param name="y">Pozice Y.</param>
        /// <param name="radiusX">Poloměr X.</param>
        /// <param name="radiusY">Poloměr Y.</param>
        /// <param name="count">Počet bodů.</param>
        /// <param name="color">Barva.</param>
        public static void FilledEllipse(float x, float y, float radiusX, float radiusY, int count, Color4 color)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);

            double angle = 0;

            for (int i = 0; i < count; i++)
            {
                angle = i * GMath.TwoPI / count;
                GL.Vertex2(x + (Math.Cos(angle) * radiusX), y + (Math.Sin(angle) * radiusY));
            }
            GL.End();
        }

        #endregion Ellipse

        #region Triangle

        /// <summary>
        /// Vykreslí trojúhelník.
        /// </summary>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="baseSize">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void Triangle(float cx, float cy, float baseSize, float v, Color4 color)
        {
            GL.Begin(BeginMode.LineLoop);
            GL.Color4(color);
            GL.Vertex2(cx, cy);
            GL.Vertex2(cx - baseSize / 2, cy + v);
            GL.Vertex2(cx + baseSize / 2, cy + v);
            GL.End();
        }

        /// <summary>
        /// Vykreslí vyplněný trojúhelník.
        /// </summary>
        /// <param name="cx">Pozice bodu C (vrcholu) na ose X.</param>
        /// <param name="cy">Pozice bodu C (vrcholu) na ose Y.</param>
        /// <param name="baseSize">Délka podstavce (od vrcholu A do B).</param>
        /// <param name="v">Výška.</param>
        /// <param name="color">Barva.</param>
        public static void FilledTriangle(float cx, float cy, float baseSize, float v, Color4 color)
        {
            GL.Begin(BeginMode.Polygon);
            GL.Color4(color);
            GL.Vertex2(cx, cy);
            GL.Vertex2(cx - baseSize / 2, cy + v);
            GL.Vertex2(cx + baseSize / 2, cy + v);
            GL.End();
        }

        #endregion Triangle

        #region Pie

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
            if (angle > 360)
                angle = 360;

            GL.Color4(color);
            GL.Begin(BeginMode.LineLoop);
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
            if (angle > 360)
                angle = 360;

            GL.Color4(color);
            GL.Begin(BeginMode.Polygon);
            GL.Vertex2(x, y);

            for (int i = 0; i <= angle; i += 2)
            {
                GL.Vertex2(
                    x + (float)Math.Cos(i * GMath.PI_180) * width,
                    y + (float)Math.Sin(i * GMath.PI_180) * height);
            }
            GL.End();
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
    }
}
