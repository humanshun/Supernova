using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // プレイヤーの移動速度
    [SerializeField] NextPlanet nextPlanet;
    [SerializeField] private float nextPlanetSeconds = 0;
    private Rigidbody2D rb;       // Rigidbody2Dコンポーネントへの参照
    private Vector2 moveDirection; // 移動方向を保存するための変数
    private int planetNumber;
    private GameObject currentPlanet; // 現在プレイヤーにくっついている惑星

    // 移動範囲の制限
    public float minX = -8.5f;
    public float maxX = 8.5f;

    // PlanetのPrefabを参照
    public GameObject[] planetPrefabs; // 複数のPlanetを保持する配列

    // スポーン位置の設定
    public Transform spawnPoint; // スポーンする位置

    private bool canSpawn = true; // スポーン可能かどうかのフラグ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextPlanet.NextPlanetInstance(); // ゲーム開始時に次の惑星を表示
        planetNumber = nextPlanet.nextPlanetNumber; // 次の惑星番号を取得
        SpawnRandomPlanet(); // 最初の惑星をスポーン
    }

    void Update()
    {
        // 水平方向の入力のみ取得
        float moveX = Input.GetAxisRaw("Horizontal");  // 左右方向の入力（A/Dキー、または矢印キー）

        // Y軸方向の動きを制限するため、Yは0に固定
        moveDirection = new Vector2(moveX, 0).normalized;

        // スペースキーを押したら惑星をスポーンまたは落下させる
        if (Input.GetKeyDown(KeyCode.Space) && canSpawn)
        {
            if (currentPlanet != null)
            {
                DropCurrentPlanet();
                StartCoroutine(SpawnPlanetWithDelay());
            }
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

        // 惑星がプレイヤーにくっついている場合、惑星もプレイヤーの位置に追従
        if (currentPlanet != null)
        {
            currentPlanet.transform.position = spawnPoint.position;
        }
    }

    IEnumerator SpawnPlanetWithDelay()
    {
        yield return new WaitForSeconds(nextPlanetSeconds); // 指定時間待機
        planetNumber = nextPlanet.nextPlanetNumber;
        nextPlanet.NextPlanetInstance(); // 次の惑星を更新して表示
        SpawnRandomPlanet(); // 惑星をスポーン
    }

    void SpawnRandomPlanet()
    {
        // 惑星をスポーンしてプレイヤーにくっつける
        currentPlanet = Instantiate(planetPrefabs[planetNumber], spawnPoint.position, Quaternion.identity);

        // Kinematicモードにして物理挙動を一時的に無効化
        Rigidbody2D planetRb = currentPlanet.GetComponent<Rigidbody2D>();
        Collider2D planetCollider = currentPlanet.GetComponent<Collider2D>();
        if (planetRb != null)
        {
            planetRb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (planetCollider != null)
        {
            planetCollider.enabled = false;
        }
    }

    void DropCurrentPlanet()
    {
        if (currentPlanet != null)
        {
            // KinematicモードからDynamicモードに戻して重力で落下させる
            Rigidbody2D planetRb = currentPlanet.GetComponent<Rigidbody2D>();
            Collider2D planetCollider = currentPlanet.GetComponent<Collider2D>();

            if (planetRb != null)
            {
                planetRb.bodyType = RigidbodyType2D.Dynamic; // 重力を有効化して落下させる
            }
            if (planetCollider != null)
            {
                planetCollider.enabled = true;
            }

            currentPlanet = null; // currentPlanetをリセット
        }
    }
}
