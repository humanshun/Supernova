using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        // 2秒後にこのゲームオブジェクトを削除する
        Destroy(gameObject, 2f);
        AudioManager.instance.explosionSound();
    }
}
