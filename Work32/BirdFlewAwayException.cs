using System;

namespace Work32
{
    //Create the BirdFlewAwayException class, derived from ApplicationException  with two properties  
    public class BirdFlewAwayException : ApplicationException
    {
        //When
        //Why
        public DateTime When { get; set; }
        public string Why { get; set; }
        //private string Messege;
        //Create constructors
        public BirdFlewAwayException() { }
        public BirdFlewAwayException(string messege, string info, DateTime when) : base(messege)
        {
            Why = info;
            When = when;
        }
    }
}
