using SFML.Window;
using SFML.System;
using SFML.Graphics;

namespace SFMLTutorial.UISFML
{
    public class Slider
    {
        private Vector2f Position;
        private float AxisHeight;
        private float AxisWidth;
        private float sliderWidth;
        private float SliderHeight;
        private RectangleShape Axis = new RectangleShape();
        private RectangleShape slider = new RectangleShape();
        public float Value { get; private set; }
        private float MinValue { get; }
        private float MaxValue { get; }
        bool IsPressed = false;

        public Font font = new Font("./Fonts/Arial.ttf");
        public Text ValueDisplay = new Text();
        public Text Title = new Text();

        public Slider(float x, float y, float width, float height, float value, float min, float max)
        {
	        this.Position = new Vector2f(x,y);
	        AxisHeight = height;
	        AxisWidth = width;
	        sliderWidth = 20;
	        SliderHeight = height;
            Value = value;
            MinValue = min;
            MaxValue = max;


            ValueDisplay.Font = font;



            Axis.Position = new Vector2f(x, y);
	        Axis.Origin = new Vector2f(0, AxisHeight / 2);
	        Axis.Size = new Vector2f(AxisWidth, AxisHeight);
	        Axis.FillColor = new Color(63,63,63);
            slider.Position = GetSliderPosition();
	        slider.Origin = new Vector2f(sliderWidth / 2, SliderHeight / 2);
	        slider.Size = new Vector2f(sliderWidth, SliderHeight);
	        slider.FillColor = new Color(192,192,192);
        }

        public Slider(float x, float y, float width, float height, float value, float min, float max, string title)
        {
            this.Position = new Vector2f(x, y);
            AxisHeight = height;
            AxisWidth = width;
            sliderWidth = 20;
            SliderHeight = height;
            Value = value;
            MinValue = min;
            MaxValue = max;


            ValueDisplay.Font = font;
            Title.Font = font;
            
            Title.DisplayedString = title;

            Axis.Position = new Vector2f(x + Title.GetGlobalBounds().Size.X - 10, y);
            Axis.Origin = new Vector2f(0, AxisHeight / 2);
            Axis.Size = new Vector2f(AxisWidth, AxisHeight);
            Axis.FillColor = new Color(63, 63, 63);
            slider.Position = GetSliderPosition();
            slider.Origin = new Vector2f(sliderWidth / 2, SliderHeight / 2);
            slider.Size = new Vector2f(sliderWidth, SliderHeight);
            slider.FillColor = new Color(192, 192, 192);

            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X - 10, -SliderHeight / 2) + Axis.Position;
        }


        void Logic(RenderWindow window)
        {
            if (slider.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y) && Mouse.IsButtonPressed(Mouse.Button.Left) || IsPressed)
            {
                IsPressed = true;
                //if (Mouse.GetPosition(window).X >= Position.X && Mouse.GetPosition(window).X <= Position.X + AxisWidth)
                {
                    slider.Position = new Vector2f(Math.Clamp(Mouse.GetPosition(window).X, Axis.Position.X, Axis.Position.X+AxisWidth), Position.Y);
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
            Vector2f sliderMaxPos = new Vector2f(Axis.Position.X + (AxisWidth * precentage), Position.Y);
            return sliderMaxPos;
        }

        public float GetValueFromAxisPosition()
        {
            return (MinValue + (((slider.Position.X - Axis.Position.X) / AxisWidth) * (MaxValue - MinValue)));
        }

        public void Draw(RenderWindow window)
        {
            Logic(window);
            
            ValueDisplay.CharacterSize = 20;
            ValueDisplay.Position = new Vector2f(AxisWidth + ValueDisplay.GetGlobalBounds().Size.X / 2, -AxisHeight / 2 + ValueDisplay.GetGlobalBounds().Size.Y / 2) + Axis.Position;
            ValueDisplay.DisplayedString = Value.ToString();

            window.Draw(ValueDisplay);
            window.Draw(Axis);
            window.Draw(slider);
            window.Draw(Title);

        }

    }
}
