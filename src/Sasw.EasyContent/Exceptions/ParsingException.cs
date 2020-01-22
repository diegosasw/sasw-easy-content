namespace Sasw.EasyContent.Exceptions
{
    using System;

    public class ParsingException
        : Exception
    {
        public ParsingException(string message)
            : base(message)
        {
        }
    }
}
