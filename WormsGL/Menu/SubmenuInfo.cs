using Glib;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using System.Drawing;

namespace WormsGL.Menu
{
    class SubmenuInfo : IRenderObject
    {
        private WormsGame game;
        private GameMenu menu;
        private QFont font;

        public SubmenuInfo(WormsGame game, GameMenu menu)
        {
            this.game = game;
            this.menu = menu;

            font = new QFont(new Font(game.DefaultFontFamily, 13f));
        }

        private void UpdateControl()
        {
            if (game.KeyInput.IsKeyPress(Key.Enter) || game.KeyInput.IsKeyPress(Key.Left))
                menu.BackToMainMenu();
        }

        public void Render(FrameEventArgs e)
        {
            UpdateControl();

            int x = 100;
            int y = 110;

            GL.Disable(EnableCap.Texture2D);
            Draw.FilledRectangle(x, y, game.Width - x * 2, game.Height - y * 2, new Color4(32, 32, 32, 255));
            Draw.Rectangle(x, y, game.Width - x * 2, game.Height - y * 2, new Color4(96, 96, 96, 255));

            font.Options.Colour = Color4.LightGray;
            font.Print(@"On the other hand, we denounce with righteous indignation and
dislike men who are so beguiled and demoralized by the
charms of pleasure of the moment, so blinded by desire,
that they cannot foresee the pain and trouble that are
bound to ensue; and equal blame belongs to those who fail
in their duty through weakness of will, which is the same as saying
through shrinking from toil and pain. These cases are perfectly
simple and easy to distinguish. In a free hour, when our
power of choice isuntrammelled and when nothing prevents our
being able to do what we like best, every pleasure is to be
welcomed and every pain avoided. But in certain circumstances
and owing to the claims of duty or the obligations of business
it will frequently occur that pleasures have to be repudiated
and annoyances accepted. The wise man therefore always holds in
these matters to this principle of selection: he rejects
pleasures to secure other greater pleasures, or else he
endures pains to avoid worse pains.", new Vector2(x + 10, y + 10));

            font.Options.Colour = Color4.Orange;
            font.Print("<< Back", new Vector2(x + 10, 450));
        }
    }
}
