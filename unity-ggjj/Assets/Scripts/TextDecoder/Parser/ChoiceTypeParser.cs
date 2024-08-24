using System;

namespace TextDecoder.Parser
{
    public class ChoiceTypeParser : Parser<InvestigationState.ChoiceType>
    {
        public override string Parse(string input, out InvestigationState.ChoiceType output)
        {
            if (!Enum.TryParse(input, out output))
            {
                return $"Cannot convert '{input}' into an {typeof(InvestigationState.ChoiceType)} (valid values include: '{string.Join(", ", Enum.GetValues(typeof(InvestigationState.ChoiceType)))}')";
            }
            return null;
        }
    }
}