using System;
using System.Runtime.InteropServices;

namespace Glib.Natives
{
    /// <summary>
    /// Nativní funkce z WinAPI.
    /// </summary>
    public static class Win32
    {
        #region Konstanty

        // typ zdroje pro LoadImage() jako "uType"
        public const uint IMAGE_BITMAP = 0;
        public const uint IMAGE_ICON = 1;
        public const uint IMAGE_CURSOR = 2;

        // typy načtení pro LoadImage() jako "fuLoad"
        public const uint LR_LOADFROMFILE = 0x00000010;

        #endregion Konstanty

        #region Funkce
        
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadImage(
            IntPtr hinst,
            string lpszName,
            uint uType,
            int cxDesired,
            int cyDesired,
            uint fuLoad);

        #endregion Funkce
    }
}
