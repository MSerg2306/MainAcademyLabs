using System;

namespace Work32
{
    class Bird
    {
        //Create fields and properties
        public int[] FlySpeed = {5, 15, 25, 35 };
        public int NormalSpeed { get; set; }
        public string Nick { get; set; }
        private bool BirdFlewAway;
        //Create constructors
        public Bird() { }
        public Bird(string name,int speed)
        {
            Nick = name;
            NormalSpeed = speed;
            if (NormalSpeed>0)
                BirdFlewAway = true;
            else
            { 
                Console.WriteLine($"{Nick} will not fly away, enter a speed greater than 0");
                BirdFlewAway = false;
            }
        }
        //Implement Method public void FlyAway( int incrmnt ) which check Bird state by reading field  BirdFlewAway
        public void FlyAway(int incrmnt)
        {
            int CurrentSpeed = NormalSpeed;
            // check BirdFlewAway
            if (BirdFlewAway)
            {
                // increment the Bird speed by method argument
                CurrentSpeed += incrmnt;
                if (CurrentSpeed < FlySpeed[3])
                {
                    Console.WriteLine($"{Nick} flies with speed {CurrentSpeed}...");
                }
                else
                { 
                    BirdFlewAway = false;
                    BirdFlewAwayException MyExeptions = new BirdFlewAwayException(string.Format("{0} flew with incredible speed!", Nick), "Oh! Startle.", DateTime.Now);
                    MyExeptions.HelpLink = "http://en.wikipedia.org/wiki/Tufted_titmouse";
                    throw MyExeptions;
                }
            }
        }
    }
}
