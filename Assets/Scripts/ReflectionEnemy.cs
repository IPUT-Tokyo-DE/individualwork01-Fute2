using UnityEngine;

public class ReflectionEnemy : MonoBehaviour
{
    Transform bossTr; // ボスのTransform
    [SerializeField] float speed = 5f; // 飛ぶスピード

    void Start()
    {
        // ボスを探す（ボスのタグは "Boss" にしておく）
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            bossTr = boss.transform;
        }
        else
        {
            Debug.LogWarning("Bossを撃破!!");
        }
    }

    void Update()
    {
        if (bossTr == null) return;

        // ボスに向かって直線移動
        transform.position = Vector2.MoveTowards(
            transform.position,
            bossTr.position,
            speed * Time.deltaTime);
    }

    // OnTriggerEnter2Dでトリガーイベントを処理
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Boss"))
        {
            // ボスにダメージを与える
            BossEnemy boss = collider.GetComponent<BossEnemy>();
            if (boss != null)
            {
                boss.TakeDamage(1); // ダメージ
            }

            // 自分を消す
            Destroy(gameObject);
        }
    }
}
