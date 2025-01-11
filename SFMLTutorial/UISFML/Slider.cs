using SFML.Window;
using SFML.System;
using SFML.Graphics;
using System.Diagnostics;

namespace SFMLTutorial.UISFML
{
    public class Slider : UIElement
    {
        //private Vector2f Position;
        //private float Height;
        //private float Width;
        private float sliderWidth;
        private float SliderHeight;
        private RectangleShape Axis = new RectangleShape();
        private RectangleShape slider = new RectangleShape();
        //public float Value { get; private set; }
        private float MinValue { get; }
        private float MaxValue { get; }
        bool IsPressed = false;

        //public static Font font = new Font("./Fonts/Arial.ttf");
        public Text ValueDisplay = new Text();
        //public Text Title = new Text();

        public Slider(float width, float height, float value, float min, float max) : base(width, height, 0, 0)
        {
	        this.Position = new Vector2f(0,0);
	        sliderWidth = 20;
	        SliderHeight = height;
            Value = value;
            MinValue = min;
            MaxValue = max;


            ValueDisplay.Font = font;



            Axis.Position = new Vector2f(0,0);
	        Axis.Origin = new Vector2f(0, Height / 2);
	        Axis.Size = new Vector2f(Width, Height);
	        Axis.FillColor = new Color(63,63,63);
            slider.Position = GetSliderPosition();
	        slider.Origin = new Vector2f(sliderWidth / 2, SliderHeight / 2);
	        slider.Size = new Vector2f(sliderWidth, SliderHeight);
	        slider.FillColor = new Color(192,192,192);
        }

        public Slider(float width, float height, float value, float min, float max, string title) : base(width, height, 0 ,0)
        {
            this.Position = new Vector2f(0,0);
            sliderWidth = 20;
            SliderHeight = height;
            Value = value;
            MinValue = min;
            MaxValue = max;


            ValueDisplay.Font = font;
            Title.Font = font;
            
            Title.DisplayedString = title;

            Axis.Position = new Vector2f(Title.GetGlobalBounds().Size.X, Position.Y);
            Axis.Origin = new Vector2f(0, Height / 2);
            Axis.Size = new Vector2f(Width, Height);
            Axis.FillColor = new Color(63, 63, 63);
            slider.Position = GetSliderPosition();
            slider.Origin = new Vector2f(sliderWidth / 2, SliderHeight / 2);
            slider.Size = new Vector2f(sliderWidth, SliderHeight);
            slider.FillColor = new Color(192, 192, 192);

            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X - 10, -SliderHeight / 2) + Axis.Position;
        }
        
        public Slider(float width, float height, float paddidngHorizontal, float paddingVertical, float value, float min, float max, string title) : base(width, height, paddidngHorizontal, paddingVertical)
        {
            this.Position = new Vector2f(0,0);
            sliderWidth = 20;
            SliderHeight = height;
            Value = value;
            MinValue = min;
            MaxValue = max;


            ValueDisplay.Font = font;
            Title.Font = font;
            
            Title.DisplayedString = title;

            Axis.Position = new Vector2f(Title.GetGlobalBounds().Size.X, Position.Y);
            Axis.Origin = new Vector2f(0, Height / 2);
            Axis.Size = new Vector2f(Width, Height);
            Axis.FillColor = new Color(63, 63, 63);
            slider.Position = GetSliderPosition();
            slider.Origin = new Vector2f(sliderWidth / 2, SliderHeight / 2);
            slider.Size = new Vector2f(sliderWidth, SliderHeight);
            slider.FillColor = new Color(192, 192, 192);

            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X - 10, -SliderHeight / 2) + Axis.Position;
        }


        protected override void Logic(RenderWindow window)
        {
            if (slider.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y) && Mouse.IsButtonPressed(Mouse.Button.Left) || IsPressed)
            {
                IsPressed = true;
                //if (Mouse.GetPosition(window).X >= Position.X && Mouse.GetPosition(window).X <= Position.X + AxisWidth)
                {
                    slider.Position = new Vector2f(Math.Clamp(Mouse.GetPosition(window).X, Axis.Position.X, Axis.Position.X+Width), Axis.Position.Y);
                    Value = GetValueFromAxisPosition();
                }
            }

            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                IsPressed = false;
            }
        }

        public void SetSliderValue(float newValue)
        {

            Value = Math.Clamp(newValue, MinValue, MaxValue);
            slider.Position = GetSliderPosition();
            
        }





        public void SetSliderPercentValue(float newPercentValue)
        {
            float clampedPrecenet = Math.Clamp(newPercentValue, 0, 100);
            Value = clampedPrecenet / 100 * MaxValue;
            slider.Position = GetSliderPosition();
            
        }

        private Vector2f GetSliderPosition()
        {
            float precentage = Math.Clamp(Value, MinValue, MaxValue) / MaxValue;
            //Vector2f sliderMinPos = Axis.Position;
            Vector2f sliderMaxPos = new Vector2f(Axis.Position.X + (Axis.Size.X * precentage), Axis.Position.Y);
            return sliderMaxPos;
        }

        public float GetValueFromAxisPosition()
        {
            return (MinValue + (((slider.Position.X - Axis.Position.X) / Width) * (MaxValue - MinValue)));
        }

        public override void Draw(RenderWindow window)
        {
            Logic(window);
            
            ValueDisplay.CharacterSize = 20; // TODO: FIX WEIRD JUMPING WITH TEXT
            ValueDisplay.Position = new Vector2f(Width + ValueDisplay.GetGlobalBounds().Size.X / 2, -Height / 2 + ValueDisplay.GetGlobalBounds().Size.Y / 2) + Axis.Position;
            ValueDisplay.DisplayedString = Value.ToString();

            window.Draw(ValueDisplay);
            window.Draw(Axis);
            window.Draw(slider);
            window.Draw(Title);

        }
        public override void SetPosition(Vector2f position)
        {

            Axis.Position = new Vector2f(Title.GetGlobalBounds().Size.X + slider.Size.X, 0) + position;
            slider.Position = GetSliderPosition();
            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X - slider.Size.X, -SliderHeight / 2) + Axis.Position;
            Position = position;

        }
        public override float GetWidth()
        {
            return Width + (ValueDisplay.GetGlobalBounds().Size.X + Title.GetGlobalBounds().Size.X);
        }
        public override float GetHeight()
        {
            return Height;
        }

        public override uint GetPointCount()
        {
            return 8;
        }

        public override Vector2f GetPoint(uint index)
        {
            return Axis.Size / 2 + Axis.Position;
        }
    }
}
