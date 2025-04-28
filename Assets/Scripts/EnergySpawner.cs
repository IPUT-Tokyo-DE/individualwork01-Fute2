using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public GameObject energyPrefab;  // 出現させるエナジーのプレハブ
    public float spawnInterval = 10f; // エナジーを出現させる間隔（秒）

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;  // メインカメラの取得
        // エナジーを定期的に出現させる
        InvokeRepeating("SpawnEnergy", 0f, spawnInterval);
    }

    // エナジーを出現させるメソッド
    void SpawnEnergy()
    {
        // 画面のワールド座標内でランダムな位置を計算
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;  // 画面の横幅
        float screenHeight = mainCamera.orthographicSize;  // 画面の高さ

        Vector3 randomPosition = new Vector3(
            Random.Range(-screenWidth, screenWidth),  // x座標の範囲
            Random.Range(-screenHeight, screenHeight), // y座標の範囲
            0f); // z座標は0に設定（2Dなので必要なし）

        // エナジーを生成
        if (energyPrefab != null)
        {
            Instantiate(energyPrefab, randomPosition, Quaternion.identity); // エナジー生成
        }
    }
}
