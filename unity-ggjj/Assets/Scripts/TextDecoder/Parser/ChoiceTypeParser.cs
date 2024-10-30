using System;

namespace TextDecoder.Parser
{
    public class ChoiceTypeParser : Parser<IInvestigationState.ChoiceType>
    {
        public override string Parse(string input, out IInvestigationState.ChoiceType output)
        {
            if (!Enum.TryParse(input, out output))
            {
                return $"Cannot convert '{input}' into an {typeof(IInvestigationState.ChoiceType)} (valid values include: '{string.Join(", ", Enum.GetValues(typeof(IInvestigationState.ChoiceType)))}')";
            }
            return null;
        }
    }
}