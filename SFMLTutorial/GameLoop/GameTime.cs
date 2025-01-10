using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLTutorial
{
    public class GameTime
    {

        private float _deltatTime = 0f;
        private float _timeScale = 1f;

        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        public float DeltaTime
        {
            get { return _deltatTime * _timeScale; }
            set { _deltatTime = value; }
        }

        public float DletaTimeUnscaled
        {
            get { return _deltatTime; }
        }

        public float TotalTimeElapsed
        {
            get;
            private set;
        }
        public GameTime()
        {

        }

        public void Update(float delatTime, float totalTimeElapsed)
        {
            _deltatTime = delatTime;
            TotalTimeElapsed = totalTimeElapsed;
        }

    }
}
