using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SFML.Window.Joystick;

namespace SFMLTutorial.UISFML
{
    public abstract class UIElement : Shape
    {
        public Vector2f Position { get; set; }
        public Text Title { get; } = new Text();
        public static Font font = new Font("./Fonts/Arial.ttf");
        public dynamic Value { get; protected set; }
        protected float Width { get; set; }
        protected float Height { get; set; }
        public float PaddingHorizontal;
        public float PaddingVertical;

        //private bool doOnce;


        protected UIElement(float width, float height, float paddingHorizontal, float paddingVertical)
        {
            Width = width;
            Height = height;
            PaddingHorizontal = paddingHorizontal;
            PaddingVertical = paddingVertical;
            font = new Font("./Fonts/Arial.ttf");
            Title.Font = font;
        }
        protected UIElement(float paddingHorizontal, float paddingVertical)
        {
            PaddingHorizontal = paddingHorizontal;
            PaddingVertical = paddingVertical;
            font = new Font("./Fonts/Arial.ttf");
            Title.Font = font;
        }

        
        
        protected abstract void Logic(RenderWindow window);
        public abstract void Draw(RenderWindow window);
        public abstract bool OnClicked();
        public abstract bool IsDown();
        public abstract bool IsUp();
        public virtual float GetWidth()
        {
            return Width + Title.GetGlobalBounds().Size.X / 2;
        }

        public virtual float GetHeight()
        {
            return Height;
        }

        public virtual void SetPosition(Vector2f position)
        {
            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X, -Height / 2) + Position;
            Position = position;
        }
        
    }
}
