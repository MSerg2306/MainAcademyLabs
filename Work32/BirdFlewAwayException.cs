using System;

namespace Work32
{
    public class BirdFlewAwayException : ApplicationException
    {
        public DateTime When { get; set; }
        public string Why { get; set; }

        public BirdFlewAwayException() { }
        public BirdFlewAwayException(string messege, string info, DateTime when) : base(messege)
        {
            Why = info;
            When = when;
        }
    }
}
