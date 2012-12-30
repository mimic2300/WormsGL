using OpenTK.Graphics;

namespace Glib
{
    /// <summary>
    /// Pracuje s barvama.
    /// </summary>
    public static class GColor
    {
        /// <summary>
        /// Náhodná barva.
        /// </summary>
        /// <param name="alpha">Alfa barva (průhlednost).</param>
        public static Color4 RandomColor(byte alpha = 255)
        {
            return new Color4(Util.RandomFloat, Util.RandomFloat, Util.RandomFloat, alpha);
        }

        /// <summary>
        /// Upraví alfa kánál pro existující barvu.
        /// </summary>
        /// <param name="color">Barva.</param>
        /// <param name="a">Alfa kanál (0 = 100% průhlednost).</param>
        /// <returns>Upravená barva.</returns>
        public static Color4 Alpha(this Color4 color, byte a)
        {
            color.A = a;
            return color;
        }
    }
}
