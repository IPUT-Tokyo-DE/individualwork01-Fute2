using UnityEngine;

public class Homing : MonoBehaviour
{
    Transform playerTr; // �v���C���[��Transform
    [SerializeField] float speed = 2f;
    [SerializeField] float rotateSpeed = 180f;

    EffectManager effectManager;
    private PlayerHealth playerHealth;

    [SerializeField] GameObject reflectionEnemyPrefab; // ���ǉ��FReflectionEnemy�v���n�u

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        effectManager = FindFirstObjectByType<EffectManager>();

        // ��PlayerHealth������Ă���i���t���N�V�������[�h�m�F�p�j
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
                // �� ���t���N�V�������[�h�� �� ReflectionEnemy�������I
                if (reflectionEnemyPrefab != null)
                {
                    Instantiate(reflectionEnemyPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                // �ʏ탂�[�h �� �ʏ�̔���
                effectManager.CreateExplosion(transform.position);
            }

            Destroy(gameObject);
        }
    }
}
