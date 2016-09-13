using System;

namespace Knapcode.PolyGeocoder
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}