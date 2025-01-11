using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTutorial.UISFML
{
    public abstract class UIElement
    {
        public Text Title { get; }
        public static Font font;
        public dynamic Value { get; protected set; }
        protected abstract void Logic(RenderWindow window);
        public abstract void Draw(RenderWindow window);
        
    }
}
