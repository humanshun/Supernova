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

    //----------------------------------------------
    //開始時にファイルをチェック、読み込み
    void Awake()
    {
        //パス名を取得
        filepath = Application.dataPath + "/" + fileName;

        //ファイルがない時、ファイルを作成
        if (!File.Exists(filepath))
        {
            Save(data);
        }

        //ファイルを読み込んでdataに格納
        data = Load(filepath);
    }

    //-----------------------------------------------
    //jsonとしてデータを保存
    void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);

    }
    //-----------------------------------------------
    //jsonファイル読み込み
    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);
        string json = rd.ReadToEnd();
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json);
    }
    //-----------------------------------------------
    //ゲーム終了時に保存
    void OnDestroy()
    {
        Save(data);
    }
}
