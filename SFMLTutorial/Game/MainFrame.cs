using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLTutorial
{


    class MainFrame : GameLoop
    {
        public const uint DEFAULT_WINDOW_WIDTH = 1920;
        public const uint DEFAULT_WINDOW_HEIGHT = 1080;
        public MainFrame() : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, "MAIN FRAM", Color.Black)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            CircleShape rect = new CircleShape(55f);
            rect.Origin = new Vector2f(rect.Radius, rect.Radius);
            rect.Position = new Vector2f(DEFAULT_WINDOW_WIDTH / 2, DEFAULT_WINDOW_HEIGHT / 2);
            rect.FillColor = Color.White;
            Window.Draw(rect);
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
