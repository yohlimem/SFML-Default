using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLTutorial
{
    public abstract class GameLoop
    {

        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        public RenderWindow Window
        {
            get;
            protected set;
        }

        public GameTime GameTime
        {
            get;
            protected set;
        }

        public Color BackgroundColor{ get; set; }

        protected GameLoop(uint width, uint height, string windowTitle, Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
            Window = new RenderWindow(new VideoMode(width, height), windowTitle);
        }

    }
}
