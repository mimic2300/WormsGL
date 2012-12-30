using System;
using System.Drawing;
using System.Drawing.Text;

namespace Glib
{
    /// <summary>
    /// Pomocné funkce.
    /// </summary>
    public static class Util
    {
        private static readonly Random random = new Random(Environment.TickCount);

        /// <summary>
        /// Generátor čísel.
        /// </summary>
        public static Random Rand
        {
            get { return random; }
        }

        /// <summary>
        /// Vrátí náhodné číslo typu float od 0.000 do 1.000.
        /// </summary>
        public static float RandomFloat
        {
            get { return (float)random.Next(0, 1001) / 1000; }
        }

        /// <summary>
        /// Načte font ze zdrojů.
        /// </summary>
        /// <param name="fontResourceData">Dara fontu ze zdrojů.</param>
        /// <param name="fontCollection">Výstupní kolekce fontů s novým fontem.</param>
        /// <returns>Vrací FontFamily.</returns>
        public static unsafe FontFamily LoadFontFamily(byte[] fontResourceData, out PrivateFontCollection fontCollection)
        {
            fixed (byte* ptr = fontResourceData)
            {
                fontCollection = new PrivateFontCollection();
                fontCollection.AddMemoryFont(new IntPtr(ptr), fontResourceData.Length);
                return fontCollection.Families[0];
            }
        }

        /// <summary>
        /// Načte font ze zdrojů.
        /// </summary>
        /// <param name="fontResourceData">Dara fontu ze zdrojů.</param>
        /// <returns>Vrací FontFamily.</returns>
        public static FontFamily LoadFontFamily(byte[] fontResourceData)
        {
            PrivateFontCollection fontCollection;
            FontFamily family = LoadFontFamily(fontResourceData, out fontCollection);
            return family;
        }
    }
}
