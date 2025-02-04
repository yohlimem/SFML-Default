using System;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Xml.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFMLTutorial.UISFML
{
    public class UIContainer
    {
        public List<UIElement> UIElements = new List<UIElement>();
        public float MaxWidth = 0;
        private Vector2f Size;
        public Color BackgroundColor = new Color(30, 30, 30);
        public Vector2f Position = new Vector2f(30,100);
        public Vector2f Padding = new Vector2f(0,0);
        

        //public UIContainer(Vector2f size)
        //{
        //    Size = size;
        //}
        //public UIContainer(float width, float height)
        //{
        //    Size = new Vector2f(width, height);
        //}
        public UIContainer()
        {
        }

        public void AddElement(UIElement element)
        {

            if (UIElements.Count == 0) {
                element.SetPosition(new Vector2f(0, element.GetHeight() / 2 + element.PaddingVertical / 2) + Position);
                UIElements.Add(element);

                AddSize(element);

                return;
            }

            AddSize(element);

            

            Size.Y += UIElements[^1].PaddingVertical;


            float nextPositionY = UIElements[^1].GetHeight() + UIElements[^1].Position.Y;
            float paddingY = UIElements[^1].PaddingVertical / 2 + element.PaddingVertical / 2;


            element.SetPosition(new Vector2f(Position.X, nextPositionY + paddingY));

            Debug.Print(element.GetType() + " " + element.GetHeight());

            UIElements.Add(element);
            
        }

        private void AddSize(UIElement element)
        {
            Size.Y += element.GetHeight() + element.PaddingVertical;

            MaxWidth = Math.Max(MaxWidth, element.GetWidth() + element.PaddingHorizontal);
            Size.X = MaxWidth;
        }

        public void UpdateSize()
        {
            Size.X = 0;
            MaxWidth = 0;
            foreach (UIElement element in UIElements)
            {
                MaxWidth = Math.Max(MaxWidth, element.GetWidth() + element.PaddingHorizontal);
                Size.X = MaxWidth;

            }

        }

        public void Draw(RenderWindow window)
        {
            RectangleShape rect = new RectangleShape();
            rect.Size = Size + Padding + new Vector2f(0,UIElements[^1].PaddingVertical);
            rect.FillColor = BackgroundColor;
            rect.Position = Position - (Padding / 2);
            
            window.Draw(rect);
            UpdateSize();

            foreach (UIElement element in UIElements)
            {
                element.Draw(window);
            }
        }

    }
}
