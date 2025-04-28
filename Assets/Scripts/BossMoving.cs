using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;  // �ړ����x
    public int maxHealth = 50;    // �{�X�̍ő�HP
    private int currentHealth;    // ���݂�HP

    private Vector2 targetPosition;
    private Camera mainCamera;

    public GameObject backGroundCollar; // �w�i�I�u�W�F�N�g�iBackGroundCollar�j

    private float backgroundMoveSpeed = 0.1f; // �w�i�ړ��̑��x�i�����\�j

    void Start()
    {
        mainCamera = Camera.main;  // ���C���J�����擾
        SetNewTargetPosition();

        currentHealth = maxHealth;  // HP���ő�ɃZ�b�g

        // �{�X��Collider2D��Trigger�ɐݒ�
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;  // Trigger�ɐݒ�
        }

        if (backGroundCollar == null)
        {
            Debug.LogError("BackGroundCollar���ݒ肳��Ă��܂���I");
        }
    }

    void Update()
    {
        // ���ݒn����ړI�n�Ɍ������Ĉړ�
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // �ړI�n�ɓ��B�����玟�̖ڕW�n�_��ݒ�
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        float randX = Random.Range(0.1f, 0.9f);
        float randY = Random.Range(0.1f, 0.9f);

        // �r���[�|�[�g�̍��W�����[���h���W�ɕϊ�
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(new Vector3(randX, randY, transform.position.z - mainCamera.transform.position.z));
        targetPosition = new Vector2(worldPos.x, worldPos.y);
    }

    // �_���[�W���󂯂�֐�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Boss�Ƀ_���[�W�I�c��HP: " + currentHealth);

        // �w�i��X���W���ړ�
        MoveBackground();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �w�i��X���W���ړ�������
    void MoveBackground()
    {
        if (backGroundCollar != null)
        {
            // �w�i��X���W���������ړ�������i�̗͂����邽�тɁj
            backGroundCollar.transform.position -= new Vector3(backgroundMoveSpeed, 0f, 0f);
        }
    }

    // �|�ꂽ�Ƃ��̏���
    void Die()
    {
        Debug.Log("Boss���j�I");
        Destroy(gameObject);
    }

    // �g���K�[�C�x���g�Ń��t���N�V�����G�l�~�[�Ƃ̏Փ˂����o
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ReflectionEnemy"))
        {
            // ���t���N�V�����G�l�~�[�ɓ���������_���[�W���󂯂�
            TakeDamage(1);  // �_���[�W�ʂ͓K�X�ݒ�

            // �G�t�F�N�g�𐶐�����ꍇ�́A�����ŏ���
            // effectManager.CreateBossHitEffect(transform.position);

            // ���t���N�V�����G�l�~�[���폜
            Destroy(other.gameObject);
        }
    }
}
