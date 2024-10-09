using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    //シーン名をインスペクタから設定できるように
    [SerializeField] string sceneName;

    [SerializeField] Button restartButton;

    void Start()
    {
        restartButton.onClick.AddListener(ChangeScene);
    }
    // ボタンが押されたときに呼ばれるメソッド
    public void ChangeScene()
    {
        GM.Instance.score = 0;
        //指定されたシーンに切り替え
        SceneManager.LoadScene(sceneName);
    }
}
