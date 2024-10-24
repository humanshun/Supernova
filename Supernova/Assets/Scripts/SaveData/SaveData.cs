[System.Serializable]
public class SaveData
{
    public const int rankCnt = 10;
    public string[] userNames = new string[rankCnt]; // 修正: userName -> userNames
    public int[] rank = new int[rankCnt];
}
