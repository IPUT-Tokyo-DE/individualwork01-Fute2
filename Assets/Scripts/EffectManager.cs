using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject explosionPiecePrefab;      // �ʏ�̔����p�[�c�̃v���n�u
    public GameObject playerExplosionPrefab;     // �v���C���[�ɓ��������Ƃ��p�̔����G�t�F�N�g
    public GameObject energyPrefab;              // �G�i�W�[�p
    public GameObject bossHitEffectPrefab;       // �{�X�ɓ��������Ƃ��̃G�t�F�N�g

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

    // �Փˎ��ɌĂ΂�郁�\�b�h�i2D�p�j
    public void HandleCollision(GameObject collidedObject, Vector3 position)
    {
        if (collidedObject.CompareTag("Player"))
        {
            // �v���C���[�ɓ��������Ƃ� �� �v���C���[�p�������o���ď�����
            CreatePlayerExplosion(position);
        }
        else if (collidedObject.CompareTag("Shield"))
        {
            // ���t���N�V�������[�h���̓G�i�W�[���o���Ȃ�
            if (playerHealth != null && !playerHealth.isInReflectionMode)
            {
                // �V�[���h�ɓ��������Ƃ� �� 20%�̊m���ŃG�i�W�[���o���ď�����
                if (Random.Range(0f, 1f) <= 0.2f)  // 0.0f����1.0f�̃����_���Ȓl�Ŋm���𔻒�
                {
                    CreateEnergy(position);  // �G�i�W�[���o��
                }
            }
        }

        // �G�I�u�W�F�N�g���폜
        Destroy(collidedObject);
    }

    // �ʏ�̔������������郁�\�b�h
    public void CreateExplosion(Vector3 position)
    {
        int count = 6; // ���˂���p�[�c�̐�

        // 6�����ɔ�΂�
        for (int i = 0; i < count; i++)
        {
            // �������v�Z
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // �����p�[�c�𐶐�
            GameObject piece = Instantiate(explosionPiecePrefab, position, Quaternion.identity);

            // �����p�[�c�̈ړ�������ݒ�
            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            ep.moveDirection = dir;  // �ړ�����
        }
    }

    // �v���C���[�ɓ��������Ƃ��̔������������郁�\�b�h
    public void CreatePlayerExplosion(Vector3 position)
    {
        int count = 6; // �p�[�c�̐��i������6�ɂ���j

        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject piece = Instantiate(playerExplosionPrefab, position, Quaternion.identity);

            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            ep.moveDirection = dir;
        }
    }

    // �G�i�W�[���o�����\�b�h
    public void CreateEnergy(Vector3 position)
    {
        // �G�i�W�[�𐶐�
        if (energyPrefab != null)
        {
            Instantiate(energyPrefab, position, Quaternion.identity);
        }
    }

    // �{�X�ɓ����������̃G�t�F�N�g�𐶐����郁�\�b�h
    public void CreateBossHitEffect(Vector3 position)
    {
        int count = 6; // �p�[�c�̐��i6�ɐݒ�j

        // 6�����ɔ�΂�
        for (int i = 0; i < count; i++)
        {
            // �������v�Z
            float angle = (360f / count) * i * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // �{�X�ɓ����������̃G�t�F�N�g�𐶐�
            GameObject piece = Instantiate(bossHitEffectPrefab, position, Quaternion.identity);

            // �{�X�q�b�g�G�t�F�N�g�̈ړ�������ݒ�
            ExplosionPiece ep = piece.GetComponent<ExplosionPiece>();
            if (ep != null)
            {
                ep.moveDirection = dir; // �ړ�����
            }
        }
    }
}
