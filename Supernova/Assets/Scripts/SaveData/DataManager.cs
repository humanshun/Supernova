using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [HideInInspector] public SaveData data;
    string filepath;
    string fileName = "Data.json";

    //----------------------------------------------------------------
    //開始時にファイルをチェック、読み込み
    void Awake()
    {
        //パス名を取得
        filepath = Application.dataPath + "/" + fileName;

        // ファイルがない時、新しい SaveData オブジェクトを初期化して保存
        if (!File.Exists(filepath))
        {
            data = new SaveData();
            Save(data);
        }
        else
        {
            // ファイルを読み込んで data に格納
            data = Load(filepath);
        }
    }

    //----------------------------------------------------------------
    //jsonとしてデータを保存
    public void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        StreamWriter writer = new StreamWriter(filepath);
        writer.Write(json);
        writer.Close();
    }

    //----------------------------------------------------------------
    //jsonファイル読み込み
    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);
        string json = rd.ReadToEnd();
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json);
    }

    //----------------------------------------------------------------
    //ゲーム終了時に保存
    void OnDestroy()
    {
        Save(data);
    }

    //----------------------------------------------------------------
    //プレイヤーの名前を設定
    public void SetPlayerName(string name)
    {
        // 現在のプレイヤー名を設定
        if (data.userNames.Length > 0)
        {
            data.userNames[0] = name;
        }
        Save(data);
    }
}
