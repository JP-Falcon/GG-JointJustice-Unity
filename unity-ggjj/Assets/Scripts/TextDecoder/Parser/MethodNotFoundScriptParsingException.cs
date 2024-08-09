namespace TextDecoder.Parser
{
    public class MethodNotFoundScriptParsingException : ScriptParsingException
    {
        public MethodNotFoundScriptParsingException(string className, string methodName, string line) : base($"Class '{className}' contains no non-public method '{methodName}()'", line)
        {
        }
    }
}