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
            this.GameTime = new GameTime();
        }

        public void Run()
        {
            LoadContent();
            Initialize();

            float totalTimeBeforeUpdate = 0d;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock;

            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if (totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    GameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());

                    totalTimeBeforeUpdate = 0;

                    Update(GameTime);

                    Window.Clear(BackgroundColor);

                    Draw(GameTime);
                    Window.Display();
                }
            }
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

    }
}
