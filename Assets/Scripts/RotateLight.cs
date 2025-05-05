using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public float dayLength = 10;

    // degrees per second
    private float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = 360f / dayLength;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
