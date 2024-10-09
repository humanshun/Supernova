using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // プレイヤーの移動速度
    [SerializeField] NextPlanet nextPlanet;
    private Rigidbody2D rb;       // Rigidbody2Dコンポーネントへの参照
    private Vector2 moveDirection; // 移動方向を保存するための変数
    private int planetNumber;

    // 移動範囲の制限
    public float minX = -8.5f;
    public float maxX = 8.5f;

    // FruitsのPrefabを参照
    public GameObject[] planetPrefabs; // 複数のFruitを保持する配列

    // スポーン位置の設定
    public Transform spawnPoint; // スポーンする位置

    private bool canSpawn = true; // スポーン可能かどうかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpawnRandomPlanet();
    }

    void Update()
    {
        // 水平方向の入力のみ取得
        float moveX = Input.GetAxisRaw("Horizontal");  // 左右方向の入力（A/Dキー、または矢印キー）

        // Y軸方向の動きを制限するため、Yは0に固定
        moveDirection = new Vector2(moveX, 0).normalized;

        // スペースキーを押したらフルーツをスポーン
        if (Input.GetKeyDown(KeyCode.Space) && canSpawn)
        {
            planetNumber = nextPlanet.nextPlanetNumber;
            nextPlanet.NextPlanetInstance();
            StartCoroutine(SpawnPlanetWithDelay()); // コルーチンを開始
        }
    }

    void FixedUpdate()
    {
        // プレイヤーを左右にのみ移動させる
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // 現在のプレイヤー位置を取得
        Vector2 currentPosition = rb.position;

        // プレイヤーのX軸の位置を制限
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        // 位置を更新
        rb.position = currentPosition;
    }

    IEnumerator SpawnPlanetWithDelay()
    {
        canSpawn = false; // スポーンを一時停止
        SpawnRandomPlanet(); // 惑星をスポーン
        yield return new WaitForSeconds(1f); // 1秒待機
        canSpawn = true; // スポーンを再開
    }

    void SpawnRandomPlanet()
    {
        // 惑星をスポーン
        Instantiate(planetPrefabs[planetNumber], spawnPoint.position, Quaternion.identity);
    }
}
