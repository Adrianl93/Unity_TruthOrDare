[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;

    public PlayerData(string name)
    {
        Name = name;
        Score = 0;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
