﻿using System;

namespace RandomData.Api.WordGeneration.Exceptions
{
    public class InvalidWordsDictionaryException : Exception
    {
        public InvalidWordsDictionaryException() : base(
            "Words dictionary file was empty, couldn't be loaded, wasn't a valid json array of strings" +
            " or no valid string with given length and characters was found.")
        {
        }
    }
}