using System;

namespace RandomData.Api.Services.StringServices.Exceptions
{
    public class InvalidWordsDictionaryException : Exception
    {
        public InvalidWordsDictionaryException() : base(
            "Words dictionary file was empty, couldn't be loaded or wasn't a valid json array of strings.")
        {
        }
    }
}