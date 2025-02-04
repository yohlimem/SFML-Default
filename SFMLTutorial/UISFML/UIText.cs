using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
namespace SFMLTutorial.UISFML
{
    public class UIText : UIElement
    {
        public string Text
        {
            get =>  Title.DisplayedString;
            set => Title.DisplayedString = value;
        }
        public UIText(string text, float paddingHorizontal, float paddingVertical) : base(paddingHorizontal, paddingVertical)
        {
            Text = text;
            Height = Title.GetGlobalBounds().Size.Y;
            Width = Title.GetGlobalBounds().Size.X;
        }
        public UIText(string text) : base(0,0)
        {
            Text = text;
            Debug.Print(Title.GetGlobalBounds().Size.ToString());
            Height = Title.GetGlobalBounds().Size.Y;
            Width = Title.GetGlobalBounds().Size.X;
        }

        protected override void Logic(RenderWindow window) { }

        public override void Draw(RenderWindow window)
        {
            window.Draw(Title);
            RectangleShape rect = new RectangleShape(Title.GetGlobalBounds().Size);
            window.Draw(rect);
        }

        public override bool OnClicked()
        {
            return false;
        }

        public override bool IsDown()
        {
            return false;
        }

        public override bool IsUp()
        {
            return false;
        }

        public override uint GetPointCount()
        {
            return 0;
        }

        public override Vector2f GetPoint(uint index)
        {
            return Title.Position;
        }

        public override void SetPosition(Vector2f position)
        {
            Title.Position = position;
            Position = position;
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
