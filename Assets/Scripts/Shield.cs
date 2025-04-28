using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject Player;
    public float rotateSpeed = 180f;
    public float radius = 1.5f;
    public float startAngle = 0f;

    private float angle;

    public int maxHealth = 10;
    private int currentHealth;

    public Color damagedColor = Color.red;
    public Color defaultColor = Color.white;
    public Color reflectionColor = Color.blue; // ReflectionModeの色（インスペクターで設定）

    private bool isInvisible = true;
    private bool isBroken = false; // ★追加：シールドが壊れたかどうか管理

    private SpriteRenderer sr;
    private Collider2D col;

    private EffectManager effectManager;
    private PlayerHealth playerHealth;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        currentHealth = maxHealth;

        sr.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0f);
        col.enabled = false;

        angle = startAngle * Mathf.Deg2Rad;
        UpdateShieldPosition();

        // EffectManagerを探す
        effectManager = FindFirstObjectByType<EffectManager>();

        // PlayerHealthスクリプトを参照
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        angle += rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;
        UpdateShieldPosition();

        if (playerHealth != null && playerHealth.isInReflectionMode)
        {
            // ★リフレクションモードになったら、壊れてても復活！
            if (isBroken)
            {
                sr.enabled = true;   // スプライト復活
                col.enabled = true;  // 当たり判定復活
                isBroken = false;    // 壊れたフラグ解除
            }

            sr.color = reflectionColor;
            isInvisible = false;
            col.enabled = true;
            currentHealth = maxHealth; // ライフ最大回復
        }
        else
        {
            if (!isBroken)
            {
                sr.color = (isInvisible) ? new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0f) : defaultColor;
                col.enabled = !isInvisible;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isInvisible = !isInvisible;

            Color color = sr.color;
            color.a = isInvisible ? 0f : 1f;
            sr.color = color;

            col.enabled = !isInvisible;
        }
    }


    void UpdateShieldPosition()
    {
        if (Player != null)
        {
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            transform.position = Player.transform.position + offset;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (effectManager != null)
            {
                effectManager.CreateExplosion(transform.position);
            }

            // ReflectionMode中はダメージを受けない
            if (playerHealth != null && !playerHealth.isInReflectionMode)
            {
                TakeDamage();
            }
        }
    }

    void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            // シールド破壊 → 非表示＆当たり判定オフ
            sr.enabled = false;
            col.enabled = false;
            isBroken = true; // ★壊れたことを記録
        }
        else if (currentHealth < maxHealth / 2)
        {
            sr.color = damagedColor;
        }
    }
}
