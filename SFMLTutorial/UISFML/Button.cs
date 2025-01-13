using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTutorial.UISFML
{
    public class Button : UIElement
    {

        private RectangleShape body = new RectangleShape();
        private bool IsPressed = false;

        public delegate void OnButtonClicked();
        public delegate void OnButtonUp();
        public event OnButtonClicked OnButtonClickedEvent;
        public event OnButtonClicked OnButtonUpEvent;

        public float MarginHorizontal;
        public float MarginVertical;
        public Button(float width, float height, float paddingHorizontal, float paddingVertical) : base(width, height, paddingHorizontal, paddingVertical)
        {

            body.Size = new Vector2f(width, height);

        }
        public Button() : base(0,0)
        {
            Width = 0;
            Height = 0;
        }
        public Button(float paddingHorizontal, float paddingVertical, string text) : base(paddingHorizontal, paddingVertical)
        {

            Title.DisplayedString = text;
            Title.FillColor = Color.Blue;
            Title.Origin = Title.GetGlobalBounds().Size / 2 + Title.Position;

            body.Size = Title.GetGlobalBounds().Size;

            Width = body.Size.X;
            Height = body.Size.Y;
           
        }
        public Button(float marginHorizontal, float marginVertical, float paddingHorizontal, float paddingVertical, string text) : base(paddingHorizontal, paddingVertical)
        {

            Title.DisplayedString = text;
            Title.FillColor = Color.Blue;
            Title.Origin = Title.GetGlobalBounds().Size / 2 + Title.Position;



            MarginHorizontal = marginHorizontal;
            MarginVertical = marginVertical;
            
            Width = body.Size.X + marginHorizontal;
            Height = body.Size.Y + marginVertical;
           
            body.Size = Title.GetGlobalBounds().Size + new Vector2f(MarginVertical, MarginHorizontal);
        }


        public override void Draw(RenderWindow window)
        {
            Logic(window);
            //body.Position = Position;
            window.Draw(body);
            window.Draw(Title);
        }

        public override void SetPosition(Vector2f position)
        {
            body.Position = position;
            Title.Position = new Vector2f(body.Size.X / 2, body.Size.Y / 2 - 5) + position;
            Position = position;
        }

        public override Vector2f GetPoint(uint index)
        {
            return new Vector2f(Width / 2, Height / 2);
        }

        public override uint GetPointCount()
        {
            return 8;
        }

        bool doOnce = false;
        public override bool OnClicked()
        {
            if (IsDown() && !doOnce)
            {
                OnButtonClickedEvent?.Invoke();


                doOnce = true;
                return true;
            }
            if (doOnce && IsUp())
            {
                OnButtonUpEvent?.Invoke();

                doOnce = false;
                return false;
            }
            return false;
        }

        public override bool IsDown()
        {
            return IsPressed;
        }
        public override bool IsUp()
        {
            return !IsPressed;
        }

        protected override void Logic(RenderWindow window)
        {
            IsPressed = body.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y) && Mouse.IsButtonPressed(Mouse.Button.Left);
            Value = OnClicked();
        }

        public override float GetWidth()
        {
            return Width;
        }

        public override float GetHeight()
        {
            return Height;
        }
    }
}
