using System;

namespace Work32
{
    class Bird
    {
        public int[] FlySpeed = {5, 15, 25, 35 };
        private int _normalSpeed { get; set; }
        private string _nick { get; set; }
        private bool _birdFlewAway;

        public Bird() { }
        public Bird(string name,int speed)
        {
            _nick = name;
            _normalSpeed = speed;
            if (_normalSpeed > 0)
                _birdFlewAway = true;
            else
            { 
                Console.WriteLine($"{_nick} will not fly away, enter a speed greater than 0");
                _birdFlewAway = false;
            }
        }
        public void FlyAway(int incrmnt)
        {
            int CurrentSpeed = _normalSpeed;
            if (_birdFlewAway)
            {
                CurrentSpeed += incrmnt;
                if (CurrentSpeed < FlySpeed[3])
                {
                    Console.WriteLine($"{_nick} flies with speed {CurrentSpeed}...");
                }
                else
                {
                    _birdFlewAway = false;
                    BirdFlewAwayException myExeptions = new BirdFlewAwayException(string.Format("{0} flew with incredible speed!", _nick), "Oh! Startle.", DateTime.Now);
                    myExeptions.HelpLink = "http://en.wikipedia.org/wiki/Tufted_titmouse";
                    throw myExeptions;
                }
            }
        }
    }
}
