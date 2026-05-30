using UnityEngine;

public class InteractionUIPosition : MonoBehaviour
{
    public Transform playerCamera;
    public Transform ui;

    public float distanceFromObject = 0.5f;

    void Update()
    {
        Vector3 dirToCamera = (playerCamera.position - transform.position).normalized;

        ui.position = transform.position + dirToCamera * distanceFromObject;

        ui.LookAt(playerCamera);
        ui.Rotate(0, 180f, 0);
    }
}