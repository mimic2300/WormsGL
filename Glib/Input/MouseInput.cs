using OpenTK;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Glib.Input
{
    /// <summary>
    /// Abstrakce nad myší.
    /// </summary>
    public class MouseInput
    {
        private GlibWindow window;
        private MouseState state;

        /// <summary>
        /// Vytvoří nový objekt typu MouseInput.
        /// </summary>
        /// <param name="window"></param>
        public MouseInput(GlibWindow window)
        {
            this.window = window;

            window.UpdateFrame += UpdateFrame;
        }

        private void UpdateFrame(object sender, FrameEventArgs e)
        {
            // Aktualizuje state
            state = Mouse.GetState();
            
            System.Console.WriteLine(this);
        }

        /// <summary>
        /// Vrací nebo nastaví pozici myši.
        /// </summary>
        public Point Position
        {
            get 
            {
                return new Point(state.X, state.Y);
            }
            set
            {
                Mouse.SetPosition(value.X, value.Y);
            }
        }

        /// <summary>
        /// Vrací, jestli button stisknutý.
        /// </summary>
        /// <param name="button">Button</param>
        public bool IsButtonDown(MouseButton button)
        {
            return state.IsButtonDown(button);
        }

        /// <summary>
        /// Vrací, jestli je button uvolněný.
        /// </summary>
        /// <param name="button">Button</param>
        public bool IsButtonUp(MouseButton button)
        {
            return state.IsButtonUp(button);
        }

        /// <summary>
        /// Překryje metodu ToString().
        /// </summary>
        public override string ToString()
        {
            return String.Format("Mouse position: [{0}, {1}]", Position.X, Position.Y);
        }
    }
}
