using System;
using System.Diagnostics;
using System.Security.AccessControl;
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
        public Color backgroundColor = new Color(30, 30, 30);
        

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
            Size.Y += element.GetHeight() + element.PaddingVertical * 1.1f;

            Debug.Print(Size.ToString());


            MaxWidth = Math.Max(MaxWidth, element.GetWidth());
            Size.X = MaxWidth + element.PaddingHorizontal;

            if (UIElements.Count == 0) {
                element.SetPosition(new Vector2f(0, element.PaddingVertical));
                UIElements.Add(element);
                return;
            }

            int lastIndex = UIElements.Count - 1;
            
            element.SetPosition(new Vector2f(0, UIElements[lastIndex].GetHeight() + UIElements[lastIndex].Position.Y + element.PaddingVertical));

            UIElements.Add(element);
            
        }

        public void Draw(RenderWindow window)
        {
            RectangleShape rect = new RectangleShape();
            rect.Size = Size;
            rect.FillColor = backgroundColor;
            
            window.Draw(rect);

            foreach (UIElement element in UIElements)
            {
                element.Draw(window);
            }
        }

    }
}
