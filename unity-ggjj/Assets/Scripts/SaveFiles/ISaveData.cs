namespace SaveFiles
{
    public interface ISaveData
    {
        int Version { get; set;}

        string Key { get; }
    }
}