using UnityEngine;

public class ExplosionPiece : MonoBehaviour
{
    public Vector2 moveDirection;      // 移動方向
    public float moveSpeed = 2f;       // 移動スピード
    public float fadeSpeed = 2f;       // 透明度減少速度

    private SpriteRenderer sr;
    private Color color;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    void Update()
    {
        // 移動
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 透明度を下げる
        color.a -= fadeSpeed * Time.deltaTime;
        sr.color = color;

        // 完全に透明になったら削除
        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
