public class SpawnerCountInfo
{
    public SpawnerCountInfo(int totalСreated, int numberNewOnes, int countActive)
    {
        TotalСreated = totalСreated;
        NumberNewOnes = numberNewOnes;
        CountActive = countActive;
    }

    public int TotalСreated { get; private set; }
    public int NumberNewOnes { get; private set; }
    public int CountActive { get; private set; }
}
