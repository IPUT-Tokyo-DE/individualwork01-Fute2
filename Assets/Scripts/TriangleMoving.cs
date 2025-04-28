using UnityEngine;

public class TriangleMoving : MonoBehaviour
{

    public float span = 3f;

    void Start()
    {
        InvokeRepeating("Logging", span, span);
    }

    void Logging()
    {
        transform.Rotate(new Vector3(0, 0, 30));
    }
}