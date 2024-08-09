using System;

namespace TextDecoder.Parser
{
    public class ScriptParsingException : Exception
    {
        public ScriptParsingException(string message, string line, Exception innerException = null) : base($"Line: {line}{Environment.NewLine}{message}", innerException)
        {
        }
    }
}