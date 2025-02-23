using UnityEngine;

public class LampRotate : MonoBehaviour

{
    public float rotationAngle = 15f; // Maximum rotation angle
    public float speed = 2f; // Speed of rotation

    private float startRotation;

    void Start()
    {
        startRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * rotationAngle;
        transform.rotation = Quaternion.Euler(0, 0, startRotation + angle);
    }
}
