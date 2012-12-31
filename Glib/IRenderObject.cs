using OpenTK;

namespace Glib
{
    /// <summary>
    /// Objekt, který se zobrazí v okně.
    /// </summary>
    public interface IRenderObject
    {
        /// <summary>
        /// Vykreslení objektu.
        /// </summary>
        /// <param name="e"></param>
        void Render(FrameEventArgs e);
    }
}
