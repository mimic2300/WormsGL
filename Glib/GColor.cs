using OpenTK.Graphics;

namespace Glib
{
    /// <summary>
    /// Pracuje s barvama.
    /// </summary>
    public static class GColor
    {
        /// <summary>
        /// Náhodná barva i náhodný alfa kanál.
        /// </summary>
        /// <returns>Vrací novou barvu.</returns>
        public static Color4 RandomColorAlpha()
        {
            return new Color4(Util.RandomFloat, Util.RandomFloat, Util.RandomFloat, Util.RandomFloat);
        }

        /// <summary>
        /// Náhodná barva s vlastním alfa kanálem.
        /// </summary>
        /// <param name="alpha">Alfa barva (průhlednost).</param>
        /// <returns>Vrací novou barvu.</returns>
        public static Color4 RandomColor(byte alpha = 255)
        {
            return new Color4(Util.RandomFloat, Util.RandomFloat, Util.RandomFloat, alpha);
        }

        /// <summary>
        /// Upraví alfa kánál pro existující barvu.
        /// </summary>
        /// <param name="color">Barva.</param>
        /// <param name="alpha">Alfa kanál (0 = 100% průhlednost).</param>
        /// <returns>Upravená barva.</returns>
        public static Color4 Alpha(this Color4 color, byte alpha)
        {
            color.A = alpha;
            return color;
        }
    }
}
