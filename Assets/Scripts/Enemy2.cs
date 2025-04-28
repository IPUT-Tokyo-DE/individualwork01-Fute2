using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 5f; // �e�̑��x

    // EffectManager�ւ̎Q��
    private EffectManager effectManager;

    // ReflectionEnemy �v���n�u
    [SerializeField] GameObject reflectionEnemyPrefab;

    // PlayerHealth�ւ̎Q��
    private PlayerHealth playerHealth;

    void Start()
    {
        // EffectManager��T���Ď擾
        effectManager = FindFirstObjectByType<EffectManager>();

        // PlayerHealth���擾�i���t���N�V�������[�h�p�j
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        // ���[�J���́u��v�����ɐi��
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        // ��ʊO�ɏo����폜
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    // �v���C���[�ƏՓ˂����ꍇ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�ɓ��������� �� �v���C���[�p�̔������o��
            effectManager.CreatePlayerExplosion(transform.position);

            // �������g���폜
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            // ���t���N�V�������[�h�� �� ReflectionEnemy������
            if (playerHealth != null && playerHealth.isInReflectionMode)
            {
                if (reflectionEnemyPrefab != null)
                {
                    Instantiate(reflectionEnemyPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                // �ʏ탂�[�h �� �ʏ�̔������o��
                effectManager.CreateExplosion(transform.position);
            }

            // �������g���폜
            Destroy(gameObject);
        }
    }
}
