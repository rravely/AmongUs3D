using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPun
{
    [Header("Character Input Values")]
    public Vector2 move;
    float targetRotation;
    float rotationVelocity;
    float rotationSmoothTime = 0.12f;

    [Header("Move speed")]
    public float moveSpeed = 0.03f;

    Animator playerAni;
    PhotonView PV;
    PlayerControl playerControl;
    Collider bodyCollider;

    private void Start()
    {
        playerAni = GetComponent<Animator>();
        playerControl = GetComponent<PlayerControl>();
        bodyCollider = GetComponent<Collider>();
        TryGetComponent<PhotonView>(out PV);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (PV.IsMine && !playerControl.isDead && !playerControl.isSeat)
        {
            Move();
        }
        
        if (PV.IsMine && !playerControl.isDead && playerControl.isInSeat)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!playerControl.isSeat)
                {
                    //Seat
                    if (playerControl.seatPos != null)
                    {
                        playerControl.isSeat = true;
                        transform.position = playerControl.seatPos;
                        transform.eulerAngles = playerControl.seatRot;

                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
                else //stand up
                {
                    playerControl.isSeat = false;
                    transform.position = playerControl.standPos;

                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
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
        transform.position += new Vector3(move.x * moveSpeed, 0f, move.y * moveSpeed);
    }
}
