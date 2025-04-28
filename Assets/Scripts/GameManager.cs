using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;

    public bool isGameStarted = false;  // ゲームが始まったかどうか

    void Start()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        isGameStarted = false;  // 最初は止めておく

        // 最初は全部非表示
        countDown3.SetActive(false);
        countDown2.SetActive(false);
        countDown1.SetActive(false);

        yield return new WaitForSeconds(0.5f); // 少し待つ

        // 3を表示
        countDown3.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown3.SetActive(false);

        // 2を表示
        countDown2.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown2.SetActive(false);

        // 1を表示
        countDown1.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown1.SetActive(false);

        // カウントダウン終了 → ゲーム開始
        isGameStarted = true;
    }
}
