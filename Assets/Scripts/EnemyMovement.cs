using UnityEngine;

public class Homing : MonoBehaviour
{
    Transform playerTr; // プレイヤーのTransform
    [SerializeField] float speed = 2f;
    [SerializeField] float rotateSpeed = 180f;

    EffectManager effectManager;
    private PlayerHealth playerHealth;

    [SerializeField] GameObject reflectionEnemyPrefab; // ★追加：ReflectionEnemyプレハブ

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        effectManager = FindFirstObjectByType<EffectManager>();

        // ★PlayerHealthを取ってくる（リフレクションモード確認用）
        playerHealth = playerTr.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            playerTr.position,
            speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            effectManager.CreatePlayerExplosion(transform.position);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            if (playerHealth != null && playerHealth.isInReflectionMode)
            {
                // ★ リフレクションモード中 → ReflectionEnemyを召喚！
                if (reflectionEnemyPrefab != null)
                {
                    Instantiate(reflectionEnemyPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                // 通常モード → 通常の爆発
                effectManager.CreateExplosion(transform.position);
            }

            Destroy(gameObject);
        }
    }
}
