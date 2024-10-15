using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject firstSelectedButton; // 最初に選択するボタン

    private void Start()
    {
        // 最初のボタンを選択状態にする
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
