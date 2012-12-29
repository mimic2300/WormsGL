using OpenTK.Input;
using System;
using System.Collections.Generic;

namespace Glib.Input
{
    /// <summary>
    /// Obsluhuje ovládání klávesnice.
    /// </summary>
    public class KeyboardInput
    {
        private List<Key> keys;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public KeyboardInput(GlibWindow window)
        {
            keys = new List<OpenTK.Input.Key>();

            window.Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>((o, e) => { KeyDown(e); });
            window.Keyboard.KeyUp += new EventHandler<KeyboardKeyEventArgs>((o, e) => { KeyUp(e); });
        }

        /// <summary>
        /// Poslední stisknutá klávesa.
        /// </summary>
        public Key LastKey
        {
            get { return keys[keys.Count - 1]; }
        }

        /// <summary>
        /// Všechny stisknuté klávesy.
        /// </summary>
        public Key[] AllPressedKeys
        {
            get { return keys.ToArray(); }
        }

        /// <summary>
        /// Pokud je stisknuta klávesa.
        /// </summary>
        /// <param name="key">Klávesa.</param>
        /// <returns>Vrací true, pokud je stisknuta.</returns>
        public bool IsKeyDown(Key key)
        {
            return keys.Contains(key);
        }

        /// <summary>
        /// Pokud je uvolněná klávesa.
        /// </summary>
        /// <param name="key">Klávesa.</param>
        /// <returns>Vrací true, pokud je uvolněná.</returns>
        public bool IsKeyUp(Key key)
        {
            return !keys.Contains(key);
        }

        /// <summary>
        /// Klávesa se stiskla.
        /// </summary>
        /// <param name="e"></param>
        private void KeyDown(KeyboardKeyEventArgs e)
        {
            if (!keys.Contains(e.Key))
                keys.Add(e.Key);
        }

        /// <summary>
        /// Klávesa se uvolnila.
        /// </summary>
        /// <param name="e"></param>
        private void KeyUp(KeyboardKeyEventArgs e)
        {
            keys.RemoveAll(key => { return (key == e.Key); });
        }

        /// <summary>
        /// Vypíše objekt klávesnice.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = null;

            for (int i = 0; i < keys.Count; i++)
            {
                result += keys[i].ToString();

                if (i < keys.Count - 1)
                    result += " + ";
            }
            return (result == null) ? "None" : result;
        }
    }
}
