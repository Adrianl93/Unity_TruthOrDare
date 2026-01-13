[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score { get; private set; }

    public PlayerData(string name)
    {
        Name = name;
        Score = 0;
    }

    public void AddScore(int value)
    {
        Score += value;
    }
}
