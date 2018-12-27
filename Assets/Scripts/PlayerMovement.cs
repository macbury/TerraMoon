using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50;

    private Vector3 moveDirection;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        moveDirection = new Vector3();
    }

    void Update()
    {
        moveDirection.Set(
            Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")
        );

        moveDirection.Normalize();
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}
