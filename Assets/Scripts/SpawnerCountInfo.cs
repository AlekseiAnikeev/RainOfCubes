using UnityEngine;

public class SpawnerCountInfo : MonoBehaviour
{
    public int TotalСreated { get; private set; }
    public int NumberNewOnes { get; private set; }
    public int CountActive { get; private set; }

    public void SetTotalСreated()
    {
        TotalСreated++;
    }
    
    public void SetNumberNewOnes()
    {
        NumberNewOnes++;
    }

    public void SetCountActive(int countActive)
    {
        CountActive = countActive;
    }
}
