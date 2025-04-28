using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 5f; // 弾の速度

    // EffectManagerへの参照
    private EffectManager effectManager;

    // ReflectionEnemy プレハブ
    [SerializeField] GameObject reflectionEnemyPrefab;

    // PlayerHealthへの参照
    private PlayerHealth playerHealth;

    void Start()
    {
        // EffectManagerを探して取得
        effectManager = FindFirstObjectByType<EffectManager>();

        // PlayerHealthを取得（リフレクションモード用）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        // ローカルの「上」方向に進む
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        // 画面外に出たら削除
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    // プレイヤーと衝突した場合
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーに当たったら → プレイヤー用の爆発を出す
            effectManager.CreatePlayerExplosion(transform.position);

            // 自分自身を削除
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            // リフレクションモード中 → ReflectionEnemyを召喚
            if (playerHealth != null && playerHealth.isInReflectionMode)
            {
                if (reflectionEnemyPrefab != null)
                {
                    Instantiate(reflectionEnemyPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                // 通常モード → 通常の爆発を出す
                effectManager.CreateExplosion(transform.position);
            }

            // 自分自身を削除
            Destroy(gameObject);
        }
    }
}
