using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject energyPrefab; // Energyプレハブを追加

    // PlayerHealthの参照（リフレクションモード状態の取得用）
    private PlayerHealth playerHealth;

    void Start()
    {
        // PlayerHealthを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Playerに当たった場合、爆発はなし
            Destroy(gameObject); // 敵オブジェクトを削除
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            // Shieldに当たった場合、エナジーだけ
            Destroy(gameObject); // 敵オブジェクトを削除

            // リフレクションモード中はエナジーを出さない
            if (playerHealth != null && !playerHealth.isInReflectionMode)
            {
                // 5回に1回エナジーを出す（確率20%）
                if (Random.Range(0f, 1f) <= 0.2f)  // 0から1の間でランダムな値を取得
                {
                    if (energyPrefab != null)
                    {
                        Instantiate(energyPrefab, transform.position, Quaternion.identity); // エナジー生成
                    }
                }
            }
        }
    }
}
