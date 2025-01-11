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

            //Title.Origin = Title.GetGlobalBounds().Size / 2 + Title.GetLocalBounds().Position;
            Title.Position = new Vector2f(-Title.GetGlobalBounds().Size.X - 10, -SliderHeight / 2) + Axis.Position;
        }


        void Logic(RenderWindow window)
        {
            if (slider.GetGlobalBounds().Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y) && Mouse.IsButtonPressed(Mouse.Button.Left) || IsPressed)
            {
                IsPressed = true;
                //if (Mouse.GetPosition(window).X >= Position.X && Mouse.GetPosition(window).X <= Position.X + AxisWidth)
                {
                    slider.Position = new Vector2f(Math.Clamp(Mouse.GetPosition(window).X, Axis.Position.X, Axis.Position.X+MaxValue), Position.Y);
                    Value = (MinValue + (((slider.Position.X - Axis.Position.X) / AxisWidth) * (MaxValue - MinValue)));
                }
            }

            if (!Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                IsPressed = false;
            }
        }

        public void SetSliderValue(float newValue)
        {
            if (newValue >= MinValue && newValue <= MaxValue)
            {
                Value = newValue;
                float diff = MaxValue - MinValue;
                float precentage = AxisWidth / diff;
                float posX = precentage * -diff;
                posX += Position.X;
                slider.Position = new Vector2f(posX, Position.Y);
            }
        }

        public void SetSliderPercentValue(float newPercentValue)
        {
            if (newPercentValue >= 0 && newPercentValue <= 100)
            {
                Value = newPercentValue / 100 * MaxValue;
                slider.Position = new Vector2f(Position.X + (AxisWidth * newPercentValue / 100), Position.Y);
            }
        }

        private Vector2f GetSliderPosition()
        {
            float precentage = Math.Clamp(Value, MinValue, MaxValue) / MaxValue;
            //Vector2f sliderMinPos = Axis.Position;
            Vector2f sliderMaxPos = new Vector2f(Axis.Position.X + (AxisWidth * precentage), Position.Y);
            return sliderMaxPos;
        }

        public void Draw(RenderWindow window)
        {
            Logic(window);
            
            ValueDisplay.CharacterSize = 20;
            ValueDisplay.Position = new Vector2f(Position.X - 10, Position.Y + 20);
            ValueDisplay.DisplayedString = Value.ToString();
            window.Draw(ValueDisplay);
            window.Draw(Axis);
            //window.Draw(returnText(Position.X + AxisWidth - 10, Position.Y + 5, MaxValue.ToString(), 20));
            window.Draw(slider);
            window.Draw(Title);
            //window.Draw(returnText(slider.getPosition().x - sliderWidth, slider.getPosition().y - sliderHeight,
            //    ((int)sliderValue), 15));
        }

    }
}
