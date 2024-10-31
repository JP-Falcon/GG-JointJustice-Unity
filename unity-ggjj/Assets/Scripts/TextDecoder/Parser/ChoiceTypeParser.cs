using System;

namespace TextDecoder.Parser
{
    public class ChoiceTypeParser : Parser<InvestigationChoiceType>
    {
        public override string Parse(string input, out InvestigationChoiceType output)
        {
            if (!Enum.TryParse(input, out output))
            {
                return $"Cannot convert '{input}' into an {typeof(InvestigationChoiceType)} (valid values include: '{string.Join(", ", Enum.GetValues(typeof(InvestigationChoiceType)))}')";
            }
            return null;
        }
    }
}