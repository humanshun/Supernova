using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    /* 値 */
    string[] rankNames = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }; //ランキング名
    const int rankCnt = SaveData.rankCnt; //ランキング数

    /* コンポーネント取得用 */
    Text[] rankTexts = new Text[rankCnt]; //ランキングのテキスト
    SaveData data; //参照するセーブデータ

    //-------------------------------------------------------------
    void Start()
    {
        data = GetComponent<DataManager>().data; //セーブデータをDataManagerから参照

        for (int i = 0; i < rankCnt; i++)
        {
            Transform rankChilds = GameObject.Find("RankTexts").transform.GetChild(i); //子オブジェクト取得
            rankTexts[i] = rankChilds.GetComponent<Text>();
        }
    }

    //-------------------------------------------------------------
    void FixedUpdate() // Typo修正: FixdUpdata -> FixedUpdate
    {
        DispRank();
    }

    //-------------------------------------------------------------
    //ランキング表示
    void DispRank()
    {
        for (int i = 0; i < rankCnt; i++)
        {
            // 各ランクごとの名前とスコアを表示
            rankTexts[i].text = (rankNames[i] + " : " + data.userNames[i] + " - " + data.rank[i]);
        }
    }

    //-------------------------------------------------------------
    //ランキング保存
    public void SetRank()
    {
        InputField scoreInputField = GameObject.Find("ScoreInputField").GetComponent<InputField>();
        int score = int.Parse(scoreInputField.text); // スコア入力フィールドの値を取得

        // 現在のプレイヤー名を取得
        string currentName = GetComponent<DataManager>().data.userNames[0];

        // スコアがランキング内の値よりも大きいときは入れ替え
        for (int i = 0; i < rankCnt; i++)
        {
            if (score > data.rank[i])
            {
                // スコアと名前を入れ替え
                int tempScore = data.rank[i];
                string tempName = data.userNames[i];

                data.rank[i] = score;
                data.userNames[i] = currentName;

                score = tempScore;
                currentName = tempName;
            }
        }
        // データを保存
        GetComponent<DataManager>().Save(data);
    }

    //--------------------------------------------------------------
    //ランクデータの削除
    public void DelRank()
    {
        for (int i = 0; i < rankCnt; i++)
        {
            data.rank[i] = 0;
            data.userNames[i] = "unknown";
        }
    }
}
