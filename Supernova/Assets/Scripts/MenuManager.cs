using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用するための名前空間

public class MenuManager : MonoBehaviour
{
    public GameObject firstSelectedButton; // 最初に選択するボタン
    public GameObject[] otherButton; // 他のボタン

    private void Start()
    {
        // 最初のボタンを選択状態にする
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        // 各ボタンにEventTriggerを設定する
        for (int i = 0; i < otherButton.Length; i++)
        {
            AddEventTriggers(otherButton[i]);
        }
        // 最初のボタンにも設定する
        AddEventTriggers(firstSelectedButton);
    }

    private void Update()
    {
        // 現在選択されているオブジェクトがInputFieldかどうかをチェック
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton != null && selectedButton.GetComponent<TMP_InputField>() != null)
        {
            // InputFieldが選択されている場合はスペースキーで音を鳴らさない
            return;
        }

        // スペースキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedButton != null)
            {
                // ボタンのアクションを呼び出す
                selectedButton.GetComponent<Button>().onClick.Invoke();
                AudioManager.instance.pressSound(); // 押された音を再生
            }
        }
    }

    // ボタンにイベントトリガーを追加するメソッド
    private void AddEventTriggers(GameObject button)
    {
        EventTrigger eventTrigger = button.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = button.AddComponent<EventTrigger>();
        }

        // 選択されたときのイベント
        EventTrigger.Entry selectEntry = new EventTrigger.Entry();
        selectEntry.eventID = EventTriggerType.Select;
        selectEntry.callback.AddListener((eventData) => 
        { 
            AudioManager.instance.SelectSound(); // 選択音を再生
        });
        eventTrigger.triggers.Add(selectEntry);

        // ボタンが押されたときのイベント
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((eventData) => 
        { 
            AudioManager.instance.pressSound(); // 押された音を再生
        });
        eventTrigger.triggers.Add(pointerDownEntry);
    }
}
