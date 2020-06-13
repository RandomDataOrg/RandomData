using System;

namespace RandomData.Api.Services.StringServices.Exceptions
{
    public class WordsDictionaryLocationUnspecifiedException : Exception
    {
        public WordsDictionaryLocationUnspecifiedException()
            : base("Words dictionary location was unspecified in config. Please specify it under StringGenerationOptions:WordsDictionaryLocation")
        {
            
        }
    }
}