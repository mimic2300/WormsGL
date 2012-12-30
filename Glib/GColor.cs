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
    }
}
