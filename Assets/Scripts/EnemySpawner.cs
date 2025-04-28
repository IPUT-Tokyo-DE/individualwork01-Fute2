using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // 生成するEnemyプレハブ
    public Transform player;           // PlayerのTransform
    public float spawnRadius = 13f;    // 出現半径

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 0.5f); // 1秒後から3秒おきに出現
    }

    void SpawnEnemy()
    {
        // ランダムな角度（ラジアン）を取得
        float angle = Random.Range(0f, Mathf.PI * 2);

        // 円周上の座標を計算
        Vector3 spawnPos = new Vector3(
            player.position.x + Mathf.Cos(angle) * spawnRadius,
            player.position.y + Mathf.Sin(angle) * spawnRadius,
            0f // 2DなのでZは0
        );

        // Enemyを生成
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
