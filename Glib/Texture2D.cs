using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Glib
{
    /// <summary>
    /// Obsahuje 2D textůru.
    /// </summary>
    public class Texture2D
    {
        private int id;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="textureID">ID textůry.</param>
        public Texture2D(int textureID)
        {
            id = textureID;
        }

        /// <summary>
        /// ID textůry.
        /// </summary>
        public int ID
        {
            get { return id; }
        }

        /// <summary>
        /// Načte textůru.
        /// </summary>
        /// <param name="filename">Cesta k textůře.</param>
        /// <returns>Nová 2D textůra.</returns>
        public static Texture2D Load(string filename)
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

            return new Texture2D(id);
        }
    }
}
