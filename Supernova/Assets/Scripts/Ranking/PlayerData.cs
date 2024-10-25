[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int score;

    public PlayerData(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}