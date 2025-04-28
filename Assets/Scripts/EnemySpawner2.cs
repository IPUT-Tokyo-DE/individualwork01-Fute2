using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;  // 弾（Enemy2）のプレハブ
    public float fireInterval = 3f; // 発射間隔
    public float bulletSpeed = 5f;  // 弾のスピード
    public int bulletCount = 8;    // 円周上に配置する弾の数
    public float radius = 1f;       // 弾を発生させる円の半径

    void Start()
    {
        InvokeRepeating(nameof(SpawnBulletCircle), 0f, fireInterval);
    }

    void SpawnBulletCircle()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // 角度計算（0?360度を等間隔に分ける）
            float angle = i * (360f / bulletCount);
            float rad = angle * Mathf.Deg2Rad;

            // 円周上の位置
            Vector2 spawnPos = new Vector2(
                transform.position.x + Mathf.Cos(rad) * radius,
                transform.position.y + Mathf.Sin(rad) * radius
            );

            // 弾を生成（回転も合わせる）
            GameObject bullet = Instantiate(
                enemyPrefab,
                spawnPos,
                Quaternion.Euler(0f, 0f, angle - 90f) // 上方向に向ける
            );

            // Rigidbody2D で前方向に発射
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
