using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerState playerState;
    [Space]
    [Header("Movement")]
    [SerializeField] private float WalkSpeed = 1.2f;
    [SerializeField] private float SprintSpeed = 3.5f;
    [SerializeField] private float SprintTime = 30f;
    [SerializeField] private float SprintTiredSpeed = 0.8f;
    [SerializeField] private float SprintReduceSpeed = 2f;
    [SerializeField] private float SprintIncreaseSpeed = 4f;
    [SerializeField] private float SprintRefuelWaitTime = 10f;
    [SerializeField] private float gravity = 9.81f;
    private CharacterController controller;
    private Vector3 inputDirection;
    private float currentSpeed;
    private bool canSprint;
    private float sprintMovementTime = 0f;
    private Vector3 velocity = Vector3.zero;
    private float ySpeed = 0f;

    [Space]
    [Header("Camera")]
    [SerializeField] private float idleHeadBobSpeed = 1f;
    [SerializeField] private float walkHeadBobSpeed = 7f;
    [SerializeField] private float sprintHeadBobSpeed = 13f;
    [SerializeField] private float sprintTiredHeadBobSpeed = 3.5f;
    [SerializeField][Range(0, 0.01f)] private float xAxisRange = 0.00025f;
    [SerializeField][Range(0, 0.1f)] private float yWalkAxisRange = 0.0008f;
    [SerializeField][Range(0, 0.1f)] private float ySprintAxisRange = 0.003f;
    [SerializeField][Range(0, 0.1f)] private float yTiredAxisRange = 0.0006f;
    [SerializeField][Range(0, 0.1f)] private float yIdleAxisRange = 0.0002f;
    [SerializeField] private Transform cameraTransform;
    private float headBobSpeed = 0f;
    private float headBobRange = 0f;
    private float headBobXRange;

    [Space]
    [Header("Sound Timing")]
    [SerializeField] private float WalkTiming = 0.7f;
    [SerializeField] private float SprintTiming = 0.3f;
    [SerializeField] private float TiredTiming = 3f;
    [SerializeField] private LayerMask WoodLayer;
    [SerializeField] private LayerMask GrassLayer;
    [SerializeField] private string currentSoundId;
    private float currentTimeSpeed;
    private float currentSoundTime = 0f;
    private Ray ray;
    private RaycastHit raycastHit;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = WalkSpeed;
        sprintMovementTime = SprintTime;
        currentSoundTime = WalkTiming;
        currentTimeSpeed = WalkTiming;
    }

    private void Update()
    {
        handleMovement();
        PlayMovingSound();
    }

    private string CheckGroundLayer()
    {
        ray = new Ray(transform.position, transform.up * -1);
        if (Physics.Raycast(ray, out raycastHit, 3f))
        {
            if ((WoodLayer.value & (1 << raycastHit.collider.gameObject.layer)) != 0)
                return "wood";

            if ((GrassLayer.value & (1 << raycastHit.collider.gameObject.layer)) != 0)
                return "grass";
        }
        return "";
    }

    private void PlayMovingSound()
    {
        if (inputDirection.magnitude > 0)
        {
            currentSoundTime += Time.deltaTime;
            if (currentSoundTime > currentTimeSpeed)
            {
                SoundManager.Instance.PlayRandomEffect(currentSoundId);
                currentSoundTime = 0;
            }
        }
        else
            currentSoundTime = currentTimeSpeed;
    }

    private void handleInputDirection()
    {
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        inputDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * inputDirection;
        inputDirection.Normalize();
        velocity = inputDirection;
    }

    private void handleSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canSprint && inputDirection.magnitude > 0)
        {
            currentSpeed = SprintSpeed;
            sprintMovementTime -= Time.deltaTime * SprintReduceSpeed;
            UiManager.Instance.UpdateSprintTime(sprintMovementTime * 100 / SprintTime);
            playerState = PlayerState.Sprint;
        }
        else
        {
            currentSpeed = WalkSpeed;
            if (canSprint && velocity.magnitude > 0)
                playerState = PlayerState.Walk;
            if (sprintMovementTime <= SprintTime)
            {
                sprintMovementTime += Time.deltaTime * SprintIncreaseSpeed;
                UiManager.Instance.UpdateSprintTime(sprintMovementTime * 100 / SprintTime);
            }
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

    private void handleGravity()
    {
        if (!controller.isGrounded)
            ySpeed -= gravity * Time.deltaTime;
        else
            ySpeed = 0f;

        velocity.y = ySpeed;
    }

    private void handleHeadBob()
    {

        float sinX = cameraTransform.localPosition.x + Mathf.Sin(Time.time * headBobSpeed) * headBobXRange;
        float sinY = cameraTransform.localPosition.y + Mathf.Cos(Time.time * headBobSpeed) * headBobRange;
        cameraTransform.localPosition = new Vector3(sinX, sinY, cameraTransform.localPosition.z);
    }

    private void handleState()
    {
        switch (playerState)
        {
            case PlayerState.Walk:
                headBobSpeed = walkHeadBobSpeed;
                headBobRange = yWalkAxisRange;
                headBobXRange = xAxisRange;
                currentTimeSpeed = WalkTiming;
                currentSoundId = CheckGroundLayer() + "-walk";
                break;
            case PlayerState.Sprint:
                headBobSpeed = sprintHeadBobSpeed;
                headBobRange = ySprintAxisRange;
                headBobXRange = xAxisRange;
                currentTimeSpeed = SprintTiming;
                currentSoundId = CheckGroundLayer() + "-run";
                break;
            case PlayerState.SprintTired:
                headBobSpeed = sprintTiredHeadBobSpeed;
                headBobRange = yTiredAxisRange;
                headBobXRange = xAxisRange;
                currentTimeSpeed = TiredTiming;
                currentSoundId = "heavy-breathing";
                break;
            default:
                headBobSpeed = idleHeadBobSpeed;
                headBobRange = yIdleAxisRange;
                headBobXRange = 0f;
                break;
        }

        if (velocity.magnitude <= 0)
        {
            currentSoundId = "";
            playerState = PlayerState.Idle;
        }
    }

    private void handleMovement()
    {
        handleInputDirection();
        handleSpeed();
        handleGravity();
        handleState();
        handleHeadBob();
        controller.Move(velocity * Time.deltaTime * currentSpeed);
    }

}

public enum PlayerState
{
    Idle,
    Walk,
    Sprint,
    SprintTired
}