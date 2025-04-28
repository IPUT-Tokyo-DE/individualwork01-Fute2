using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;  // �e�iEnemy2�j�̃v���n�u
    public float fireInterval = 3f; // ���ˊԊu
    public float bulletSpeed = 5f;  // �e�̃X�s�[�h
    public int bulletCount = 8;    // �~����ɔz�u����e�̐�
    public float radius = 1f;       // �e�𔭐�������~�̔��a

    void Start()
    {
        InvokeRepeating(nameof(SpawnBulletCircle), 0f, fireInterval);
    }

    void SpawnBulletCircle()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // �p�x�v�Z�i0?360�x�𓙊Ԋu�ɕ�����j
            float angle = i * (360f / bulletCount);
            float rad = angle * Mathf.Deg2Rad;

            // �~����̈ʒu
            Vector2 spawnPos = new Vector2(
                transform.position.x + Mathf.Cos(rad) * radius,
                transform.position.y + Mathf.Sin(rad) * radius
            );

            // �e�𐶐��i��]�����킹��j
            GameObject bullet = Instantiate(
                enemyPrefab,
                spawnPos,
                Quaternion.Euler(0f, 0f, angle - 90f) // ������Ɍ�����
            );

            // Rigidbody2D �őO�����ɔ���
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
