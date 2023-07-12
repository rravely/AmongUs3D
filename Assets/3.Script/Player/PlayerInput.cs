using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    float targetRotation;
    float rotationVelocity;
    float rotationSmoothTime = 0.12f;

    Animator playerAni;

    private void Start()
    {
        playerAni = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    void Move()
    {
        //Rotation
        Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

        if (move.Equals(Vector2.zero)) playerAni.SetBool("Walk", false);
        else
        {
            playerAni.SetBool("Walk", true);

            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        //Move
        transform.position += new Vector3(move.x * 0.02f, 0f, move.y * 0.02f);
    }
}
