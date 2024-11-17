using System;

public class MissingBackgroundTagException : Exception
{
    public MissingBackgroundTagException(string message) : base(message) { }
}