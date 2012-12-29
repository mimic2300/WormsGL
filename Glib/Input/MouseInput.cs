using OpenTK.Input;
using System;
using System.Drawing;

namespace Glib.Input
{
    /// <summary>
    /// Obsluhuje ovládání myši.
    /// </summary>
    public class MouseInput : GlibInput
    {
        private bool containsMouse = false;
        private int deltaX = 0;
        private int deltaY = 0;
        private Point clickedPosition = Point.Empty;
        private bool clicked = false;
        private MouseButton? button = null;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="window">Herní okno.</param>
        public MouseInput(GlibWindow window)
            : base(window)
        {
            window.Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>((o, e) => { MouseDown(e); });
            window.Mouse.ButtonUp += new EventHandler<MouseButtonEventArgs>((o, e) => { MouseUp(e); });
            window.Mouse.Move += new EventHandler<MouseMoveEventArgs>((o, e) => { MouseMove(e); });

            window.MouseEnter += new EventHandler<EventArgs>((o, e) => { containsMouse = true; });
            window.MouseLeave += new EventHandler<EventArgs>((o, e) => { containsMouse = false; });
        }

        /// <summary>
        /// Pozice X.
        /// </summary>
        public int X
        {
            get { return Window.Mouse.X; }
        }

        /// <summary>
        /// Pozice Y.
        /// </summary>
        public int Y
        {
            get { return Window.Mouse.Y; }
        }

        /// <summary>
        /// Pozice Z (Wheel).
        /// </summary>
        public int Z
        {
            get { return Window.Mouse.Wheel; }
        }

        /// <summary>
        /// Delta pozice X.
        /// </summary>
        public int DeltaX
        {
            get { return deltaX; }
        }

        /// <summary>
        /// Delta pozice Y.
        /// </summary>
        public int DeltaY
        {
            get { return deltaY; }
        }

        /// <summary>
        /// Pokud herní okno obsahuje myš.
        /// </summary>
        public bool ContainsMouse
        {
            get { return containsMouse; }
        }

        /// <summary>
        /// Pozice kliknutí myši.
        /// </summary>
        public Point ClickedPosition
        {
            get { return clickedPosition; }
        }

        /// <summary>
        /// Tlačítko myši, které je stisknuto.
        /// </summary>
        public MouseButton? Button
        {
            get { return button; }
        }

        /// <summary>
        /// Je tlačítko myši stisknuto.
        /// </summary>
        public bool IsClicked
        {
            get { return clicked; }
        }

        /// <summary>
        /// Pokud je tlačítko myši stisknuto.
        /// </summary>
        /// <param name="button">Tlačítko myši.</param>
        /// <returns>Vrací true, pokud je tlačítko stisknuto.</returns>
        public bool IsMouseDown(MouseButton button)
        {
            return (this.button == button);
        }

        /// <summary>
        /// Pokud je tlačítko myši uvolněno.
        /// </summary>
        /// <param name="button">Tlačítko myši.</param>
        /// <returns>Vrací true, pokud je tlačítko uvolněno.</returns>
        public bool IsMouseUp(MouseButton button)
        {
            return (this.button != button);
        }

        /// <summary>
        /// Tlačítko myši se stisklo.
        /// </summary>
        /// <param name="e"></param>
        private void MouseDown(MouseButtonEventArgs e)
        {
            clicked = true;
            clickedPosition = e.Position;
            button = e.Button;
        }

        /// <summary>
        /// Tlačítko myši se uvolnilo.
        /// </summary>
        /// <param name="e"></param>
        private void MouseUp(MouseButtonEventArgs e)
        {
            clicked = false;
            button = null;
        }

        /// <summary>
        /// Pohym myši po herním okně.
        /// </summary>
        /// <param name="e"></param>
        private void MouseMove(MouseMoveEventArgs e)
        {
            deltaX = e.XDelta;
            deltaY = e.YDelta;
        }

        /// <summary>
        /// Vypíše informace o objektu myši.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[x={0}, y={1}, z={2}] {3} [dx={4}, dy={5}] {6}",
                X, Y, Z, containsMouse, deltaX, deltaY, button);
        }
    }
}
