using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public Color reflectionColor = Color.green;

    public bool isInReflectionMode = false;
    private bool isInvincible = false;
    private SpriteRenderer playerRenderer;
    private int energyLevel = 0;

    private float reflectionModeDuration = 15f;
    private float reflectionModeStartTime;

    [SerializeField] private Color endReflectionColor = Color.red;

    private bool canMove = true; // ★操作できるかどうか（初期はtrue）

    void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
        {
            return; // ★ 操作停止中なら何もさせない（Update終了）
        }

        MovePlayer();
        transform.rotation = Quaternion.identity;

        if (isInReflectionMode)
        {
            isInvincible = true;

            if (Time.time - reflectionModeStartTime >= reflectionModeDuration)
            {
                DeactivateReflectionMode();
            }
        }
        else
        {
            isInvincible = false;
        }
    }


    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public void ActivateReflectionMode()
    {
        health = maxHealth;
        energyLevel = 0;
        isInReflectionMode = true;
        playerRenderer.color = reflectionColor;
        reflectionModeStartTime = Time.time;

        Debug.Log("Reflection Mode Activated!");
    }

    public void DeactivateReflectionMode()
    {
        isInReflectionMode = false;
        playerRenderer.color = endReflectionColor;

        Debug.Log("Reflection Mode Deactivated!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(10);
        }
        else if (collision.gameObject.CompareTag("Energy"))
        {
            energyLevel++;
            Destroy(collision.gameObject);
            Debug.Log("Energy Level: " + energyLevel);

            if (energyLevel >= 10)
            {
                ActivateReflectionMode();
            }
        }
    }

    void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("Player is invincible! No damage taken.");
            return;
        }

        health -= damage;
        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            Time.timeScale = 0;
            transform.localScale = new Vector3(0f, 0f, 0f);
            Debug.Log("Player died!");
        }
    }

    // ★ボス撃破で呼び出す用のメソッドを追加
    public void StopPlayerMove()
    {
        canMove = false;
        Debug.Log("Player操作停止！");
    }
}
