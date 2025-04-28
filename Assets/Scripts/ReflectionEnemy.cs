using UnityEngine;

public class ReflectionEnemy : MonoBehaviour
{
    Transform bossTr; // �{�X��Transform
    [SerializeField] float speed = 5f; // ��ԃX�s�[�h

    void Start()
    {
        // �{�X��T���i�{�X�̃^�O�� "Boss" �ɂ��Ă����j
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            bossTr = boss.transform;
        }
        else
        {
            Debug.LogWarning("Boss�����j!!");
        }
    }

    void Update()
    {
        if (bossTr == null) return;

        // �{�X�Ɍ������Ē����ړ�
        transform.position = Vector2.MoveTowards(
            transform.position,
            bossTr.position,
            speed * Time.deltaTime);
    }

    // OnTriggerEnter2D�Ńg���K�[�C�x���g������
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Boss"))
        {
            // �{�X�Ƀ_���[�W��^����
            BossEnemy boss = collider.GetComponent<BossEnemy>();
            if (boss != null)
            {
                boss.TakeDamage(1); // �_���[�W
            }

            // ����������
            Destroy(gameObject);
        }
    }
}
