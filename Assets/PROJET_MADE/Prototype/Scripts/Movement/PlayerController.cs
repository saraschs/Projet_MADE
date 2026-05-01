using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _jumpForce = 200;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _camera; // 👈 AJOUT

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 👇 directions basées sur la caméra
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        var vel = (forward * v + right * h) * _speed;

        vel.y = _rb.velocity.y;
        _rb.velocity = vel;

        if (Input.GetKeyDown(KeyCode.Space))
            _rb.AddForce(Vector3.up * _jumpForce);
    }
}