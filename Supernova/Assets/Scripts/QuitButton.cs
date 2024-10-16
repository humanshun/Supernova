using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] Button quitButton;
    void Start()
    {
        quitButton.onClick.AddListener(QuitApplication);
    }
    public void QuitApplication()
    {
        // エディターで動作確認するためのコード（実際のビルド時には動作しません）
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // アプリケーションを終了する
        Application.Quit();
        #endif
    }
}
