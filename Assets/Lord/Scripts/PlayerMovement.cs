using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerState playerState;
    [Space]
    [Header("Movement")]
    [SerializeField] private float WalkSpeed = 2f;
    [SerializeField] private float SprintSpeed = 5f;
    [SerializeField] private float SprintTime = 5f;
    [SerializeField] private float SprintTiredSpeed = 1.5f;
    [SerializeField] private float SprintReduceMultiple = 2f;
    [SerializeField] private float SprintIncreaseMultiple = 2f;
    [SerializeField] private float SprintRefuelWaitTime = 2f;
    private CharacterController controller;
    private Vector3 inputDirection;
    private float currentSpeed;
    private bool canSprint;
    private float sprintMovementTime = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = WalkSpeed;
    }

    private void Update()
    {
        handleMovement();
    }

    private void handleInputDirection()
    {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputDirection.Normalize();
    }

    private void handleSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            currentSpeed = SprintSpeed;
            sprintMovementTime -= Time.deltaTime * SprintReduceMultiple;
            playerState = PlayerState.Sprint;
        }
        else
        {
            currentSpeed = WalkSpeed;
            if (canSprint)
                playerState = PlayerState.Walk;
            if (sprintMovementTime <= SprintTime)
                sprintMovementTime += Time.deltaTime * SprintIncreaseMultiple;
        }

        if (sprintMovementTime > SprintRefuelWaitTime)
            canSprint = true;
        if (sprintMovementTime <= 0)
        {
            playerState = PlayerState.SprintTired;
            currentSpeed = SprintTiredSpeed;
            canSprint = false;
        }
    }

    private void handleMovement()
    {
        handleInputDirection();
        handleSpeed();

        controller.Move(inputDirection * Time.deltaTime * currentSpeed);
    }

}

public enum PlayerState
{
    Walk,
    Sprint,
    SprintTired
}