using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;

    public bool isGameStarted = false;  // �Q�[�����n�܂������ǂ���

    void Start()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        isGameStarted = false;  // �ŏ��͎~�߂Ă���

        // �ŏ��͑S����\��
        countDown3.SetActive(false);
        countDown2.SetActive(false);
        countDown1.SetActive(false);

        yield return new WaitForSeconds(0.5f); // �����҂�

        // 3��\��
        countDown3.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown3.SetActive(false);

        // 2��\��
        countDown2.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown2.SetActive(false);

        // 1��\��
        countDown1.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDown1.SetActive(false);

        // �J�E���g�_�E���I�� �� �Q�[���J�n
        isGameStarted = true;
    }
}
