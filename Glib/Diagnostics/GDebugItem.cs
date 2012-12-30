using System;

namespace Glib.Diagnostics
{
	/// <summary>
	/// Obsahuje jednu ladící informaci.
	/// </summary>
	public class GDebugItem
	{
		/// <summary>
		/// String, který je poslán jako první parametr metodě string.Format, např. "{0}".
		/// </summary>
		public string FormatedText;

		/// <summary>
		/// Pole funkcí, které vrací jakoukoliv hodnotu.
		/// </summary>
		public Func<object>[] ValueFunction;

		/// <summary>
		/// Konstruktor.
		/// </summary>
		/// <param name="formatedText">String, který je poslán jako první parametr metodě string.Format, např. "{0}".</param>
		/// <param name="valueFunction">Pole funkcí, které vrací jakoukoliv hodnotu.</param>
		public GDebugItem(string formatedText, params Func<object>[] valueFunction)
		{
			FormatedText = formatedText;
			ValueFunction = valueFunction;
		}
	}
}
