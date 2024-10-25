using System.Collections.Generic;

[System.Serializable]
public class RankingData
{
    public List<PlayerData> players = new List<PlayerData>();

    public void AddPlayer(string playerName, int score)
    {
        players.Add(new PlayerData(playerName, score));
    }
}
