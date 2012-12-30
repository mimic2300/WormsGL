using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Glib
{
    /// <summary>
    /// Načítá herní obsah.
    /// </summary>
    public static class Util
    {
        private static readonly Random random = new Random(Environment.TickCount);

        /// <summary>
        /// Vrátí náhodné číslo typu float od 0.0 do 1.0.
        /// </summary>
        public static float RandomFloat
        {
            get { return (float)random.NextDouble(); }
        }

        /// <summary>
        /// Vykreslí textůru.
        /// </summary>
        /// <param name="filename">Cesta k textůře.</param>
        /// <returns>ID textůry.</returns>
        public static int Texture(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);
            bmp.UnlockBits(bmpData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);

            return id;
        }
    }
}
