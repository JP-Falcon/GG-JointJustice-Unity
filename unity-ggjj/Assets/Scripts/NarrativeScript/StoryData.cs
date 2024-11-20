using System.Collections.Generic;

public struct StoryData
{
    public StoryData(IReadOnlyList<string> lines, IReadOnlyList<string> moveTags)
    {
        DistinctActions = lines;
        DistinctMoveTags = moveTags;
    }

    public IReadOnlyList<string> DistinctActions { get; private set; }

    public IReadOnlyList<string> DistinctMoveTags { get; private set; }
}