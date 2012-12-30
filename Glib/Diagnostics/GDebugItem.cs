﻿using System;

namespace Glib.Diagnostics
{
    /// <summary>
    /// Obsahuje jednu ladící informaci.
    /// </summary>
    public struct GDebugItem
    {
        /// <summary>
        /// String, který je poslán jako první parametr metodě string.Format, např. "{0}".
        /// </summary>
        public string FormatedText;

        /// <summary>
        /// Pole funkcí, které vrací jakoukoliv hodnotu.
        /// </summary>
        public Func<object>[] Value;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="formatedText">String, který je poslán jako první parametr metodě string.Format, např. "{0}".</param>
        /// <param name="value">Pole funkcí, které vrací jakoukoliv hodnotu.</param>
        public GDebugItem(string formatedText, params Func<object>[] value)
        {
            FormatedText = formatedText;
            Value = value;
        }
    }
}
