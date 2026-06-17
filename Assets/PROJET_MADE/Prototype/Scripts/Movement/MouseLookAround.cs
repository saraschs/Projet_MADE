using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public float sensitivity = 15f;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;

        // 👇 limite verticale
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}