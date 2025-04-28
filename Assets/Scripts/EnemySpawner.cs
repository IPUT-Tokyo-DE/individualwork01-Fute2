using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // ��������Enemy�v���n�u
    public Transform player;           // Player��Transform
    public float spawnRadius = 13f;    // �o�����a

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 0.5f); // 1�b�ォ��3�b�����ɏo��
    }

    void SpawnEnemy()
    {
        // �����_���Ȋp�x�i���W�A���j���擾
        float angle = Random.Range(0f, Mathf.PI * 2);

        // �~����̍��W���v�Z
        Vector3 spawnPos = new Vector3(
            player.position.x + Mathf.Cos(angle) * spawnRadius,
            player.position.y + Mathf.Sin(angle) * spawnRadius,
            0f // 2D�Ȃ̂�Z��0
        );

        // Enemy�𐶐�
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
