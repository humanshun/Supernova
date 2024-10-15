using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton;

    // 最大文字数を設定
    private const int maxCharacterLimit = 10;

    void Start()
    {
        // TMP_InputFieldに文字数制限を設定
        nameInputField.characterLimit = maxCharacterLimit;

        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        string playerName = nameInputField.text;

        // 名前の長さを確認
        if (!string.IsNullOrEmpty(playerName))
        {
            if (playerName.Length <= maxCharacterLimit)
            {
                GM.Instance.PlayerName = playerName;
                SceneManager.LoadScene("InGame");
            }
            else
            {
                Debug.LogWarning($"プレイヤー名は{maxCharacterLimit}文字以内で入力してください。");
            }
        }
        else
        {
            Debug.LogWarning("プレイヤー名を入力してください。");
        }
    }
}
