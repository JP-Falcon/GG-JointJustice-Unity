namespace SaveFiles
{
    public interface ISaveData
    {
        int Version { get; set;}
        
        int LatestVersion { get; }

        string Key { get; }
    }
}