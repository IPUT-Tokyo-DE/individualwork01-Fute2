using UnityEngine;

public class ExplosionPiece : MonoBehaviour
{
    public Vector2 moveDirection;      // �ړ�����
    public float moveSpeed = 2f;       // �ړ��X�s�[�h
    public float fadeSpeed = 2f;       // �����x�������x

    private SpriteRenderer sr;
    private Color color;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    void Update()
    {
        // �ړ�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // �����x��������
        color.a -= fadeSpeed * Time.deltaTime;
        sr.color = color;

        // ���S�ɓ����ɂȂ�����폜
        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
