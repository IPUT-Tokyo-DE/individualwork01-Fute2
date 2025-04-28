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
    public Color reflectionColor = Color.blue; // ReflectionMode�̐F�i�C���X�y�N�^�[�Őݒ�j

    private bool isInvisible = true;
    private bool isBroken = false; // ���ǉ��F�V�[���h����ꂽ���ǂ����Ǘ�

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

        // EffectManager��T��
        effectManager = FindFirstObjectByType<EffectManager>();

        // PlayerHealth�X�N���v�g���Q��
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        angle += rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;
        UpdateShieldPosition();

        if (playerHealth != null && playerHealth.isInReflectionMode)
        {
            // �����t���N�V�������[�h�ɂȂ�����A���ĂĂ������I
            if (isBroken)
            {
                sr.enabled = true;   // �X�v���C�g����
                col.enabled = true;  // �����蔻�蕜��
                isBroken = false;    // ��ꂽ�t���O����
            }

            sr.color = reflectionColor;
            isInvisible = false;
            col.enabled = true;
            currentHealth = maxHealth; // ���C�t�ő��
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

            // ReflectionMode���̓_���[�W���󂯂Ȃ�
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
            // �V�[���h�j�� �� ��\���������蔻��I�t
            sr.enabled = false;
            col.enabled = false;
            isBroken = true; // ����ꂽ���Ƃ��L�^
        }
        else if (currentHealth < maxHealth / 2)
        {
            sr.color = damagedColor;
        }
    }
}
