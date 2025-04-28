using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject energyPrefab; // Energy�v���n�u��ǉ�

    // PlayerHealth�̎Q�Ɓi���t���N�V�������[�h��Ԃ̎擾�p�j
    private PlayerHealth playerHealth;

    void Start()
    {
        // PlayerHealth���擾
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
            // Player�ɓ��������ꍇ�A�����͂Ȃ�
            Destroy(gameObject); // �G�I�u�W�F�N�g���폜
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            // Shield�ɓ��������ꍇ�A�G�i�W�[����
            Destroy(gameObject); // �G�I�u�W�F�N�g���폜

            // ���t���N�V�������[�h���̓G�i�W�[���o���Ȃ�
            if (playerHealth != null && !playerHealth.isInReflectionMode)
            {
                // 5���1��G�i�W�[���o���i�m��20%�j
                if (Random.Range(0f, 1f) <= 0.2f)  // 0����1�̊ԂŃ����_���Ȓl���擾
                {
                    if (energyPrefab != null)
                    {
                        Instantiate(energyPrefab, transform.position, Quaternion.identity); // �G�i�W�[����
                    }
                }
            }
        }
    }
}
