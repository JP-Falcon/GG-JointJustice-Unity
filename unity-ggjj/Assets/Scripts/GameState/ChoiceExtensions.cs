using System.Linq;
using Ink.Runtime;

public static class ChoiceExtensions
{
    public static string GetTagValue(this Choice choice, string key)
    {
        return choice.tags.First(choiceTags => choiceTags.StartsWith(key+":")).Split(':')[1];
    }
}