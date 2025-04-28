using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;  // 移動速度
    public int maxHealth = 50;    // ボスの最大HP
    private int currentHealth;    // 現在のHP

    private Vector2 targetPosition;
    private Camera mainCamera;

    public GameObject backGroundCollar; // 背景オブジェクト（BackGroundCollar）

    private float backgroundMoveSpeed = 0.1f; // 背景移動の速度（調整可能）

    void Start()
    {
        mainCamera = Camera.main;  // メインカメラ取得
        SetNewTargetPosition();

        currentHealth = maxHealth;  // HPを最大にセット

        // ボスのCollider2DをTriggerに設定
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;  // Triggerに設定
        }

        if (backGroundCollar == null)
        {
            Debug.LogError("BackGroundCollarが設定されていません！");
        }
    }

    void Update()
    {
        // 現在地から目的地に向かって移動
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 目的地に到達したら次の目標地点を設定
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        float randX = Random.Range(0.1f, 0.9f);
        float randY = Random.Range(0.1f, 0.9f);

        // ビューポートの座標をワールド座標に変換
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(new Vector3(randX, randY, transform.position.z - mainCamera.transform.position.z));
        targetPosition = new Vector2(worldPos.x, worldPos.y);
    }

    // ダメージを受ける関数
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Bossにダメージ！残りHP: " + currentHealth);

        // 背景のX座標を移動
        MoveBackground();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 背景のX座標を移動させる
    void MoveBackground()
    {
        if (backGroundCollar != null)
        {
            // 背景のX座標を少しずつ移動させる（体力が減るたびに）
            backGroundCollar.transform.position -= new Vector3(backgroundMoveSpeed, 0f, 0f);
        }
    }

    // 倒れたときの処理
    void Die()
    {
        Debug.Log("Boss撃破！");
        Destroy(gameObject);
    }

    // トリガーイベントでリフレクションエネミーとの衝突を検出
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ReflectionEnemy"))
        {
            // リフレクションエネミーに当たったらダメージを受ける
            TakeDamage(1);  // ダメージ量は適宜設定

            // エフェクトを生成する場合は、ここで処理
            // effectManager.CreateBossHitEffect(transform.position);

            // リフレクションエネミーを削除
            Destroy(other.gameObject);
        }
    }
}
