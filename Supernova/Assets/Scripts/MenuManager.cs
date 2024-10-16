using UnityEngine;

public class ToggleObjectVisibility : MonoBehaviour
{
    // 切り替えたいオブジェクトをInspectorから指定できるようにします
    public GameObject MenuObject;

    void Start()
    {
        MenuObject.SetActive(false);
    }
    void Update()
    {
        // ESCキーが押された場合
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // オブジェクトのアクティブ状態を反転させる
            if (MenuObject != null)
            {
                MenuObject.SetActive(!MenuObject.activeSelf);
            }
        }
    }
}
