using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject explosionPiecePrefab;      // 通常の爆発パーツのプレハブ
    public GameObject playerExplosionPrefab;     // プレイヤーに当たったとき用の爆発エフェクト
    public GameObject energyPrefab;              // エナジー用
    public GameObject bossHitEffectPrefab;       // ボスに当たったときのエフェクト

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

    // 衝突時に呼ばれるメソッド（2D用）
    public void HandleCollision(GameObject collidedObject, Vector3 position)
    {
        if (collidedObject.CompareTag("Player"))
        {
            // プレイヤーに当たったとき → プレイヤー用爆発を出して消える
            CreatePlayerExplosion(position);
        }
        else if (collidedObject.CompareTag("Shield"))
        {
            // リフレクションモード中はエナジーを出さない
            if (playerHealth != null && !playerHealth.isInReflectionMode)
            {
                // シールドに当たったとき → 20%の確率でエナジーを出して消える
                if (Random.Range(0f, 1f) <= 0.2f)  // 0.0fから1.0fのランダムな値で確率を判定
                {
                    CreateEnergy(position);  // エナジーを出す
                }
            }
        }

        // 敵オブジェクトを削除
        Destroy(collidedObject);
    }

    // 通常の爆発を処理するメソッド
    public void CreateExplosion(Vector3 position)
    {
        int count = 6; // 発射するパーツの数

        // 6方向に飛ばす
        for (int i = 0; i < count; i++)
        {
            // 方向を計算
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // 爆発パーツを生成
            GameObject piece = Instantiate(explosionPiecePrefab, position, Quaternion.identity);

            // 爆発パーツの移動方向を設定
            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            ep.moveDirection = dir;  // 移動方向
        }
    }

    // プレイヤーに当たったときの爆発を処理するメソッド
    public void CreatePlayerExplosion(Vector3 position)
    {
        int count = 6; // パーツの数（ここも6個にする）

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject piece = Instantiate(playerExplosionPrefab, position, Quaternion.identity);

            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            ep.moveDirection = dir;
        }
    }

    // エナジーを出すメソッド
    public void CreateEnergy(Vector3 position)
    {
        // エナジーを生成
        if (energyPrefab != null)
        {
            Instantiate(energyPrefab, position, Quaternion.identity);
        }
    }

    // ボスに当たった時のエフェクトを生成するメソッド
    public void CreateBossHitEffect(Vector3 position)
    {
        int count = 6; // パーツの数（6個に設定）

        // 6方向に飛ばす
        for (int i = 0; i < count; i++)
        {
            // 方向を計算
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // ボスに当たった時のエフェクトを生成
            GameObject piece = Instantiate(bossHitEffectPrefab, position, Quaternion.identity);

            // ボスヒットエフェクトの移動方向を設定
            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            if (ep != null)
            {
                ep.moveDirection = dir; // 移動方向
            }
        }
    }
}
