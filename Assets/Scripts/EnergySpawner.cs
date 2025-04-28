using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public GameObject energyPrefab;  // �o��������G�i�W�[�̃v���n�u
    public float spawnInterval = 10f; // �G�i�W�[���o��������Ԋu�i�b�j

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;  // ���C���J�����̎擾
        // �G�i�W�[�����I�ɏo��������
        InvokeRepeating("SpawnEnergy", 0f, spawnInterval);
    }

    // �G�i�W�[���o�������郁�\�b�h
    void SpawnEnergy()
    {
        // ��ʂ̃��[���h���W���Ń����_���Ȉʒu���v�Z
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;  // ��ʂ̉���
        float screenHeight = mainCamera.orthographicSize;  // ��ʂ̍���

        Vector3 randomPosition = new Vector3(
            Random.Range(-screenWidth, screenWidth),  // x���W�͈̔�
            Random.Range(-screenHeight, screenHeight), // y���W�͈̔�
            0f); // z���W��0�ɐݒ�i2D�Ȃ̂ŕK�v�Ȃ��j

        // �G�i�W�[�𐶐�
        if (energyPrefab != null)
        {
            Instantiate(energyPrefab, randomPosition, Quaternion.identity); // �G�i�W�[����
        }
    }
}
