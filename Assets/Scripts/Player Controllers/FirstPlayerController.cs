using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro; // Include the TMPro namespace for TextMeshPro components
using AvatarManagement; // Include the AvatarManagement namespace to access the AvatarData class

public class FirstPlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float sensitivity = 2.0f; // Sensitivity for mouse/touch rotation
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    public TMP_InputField inputField; // Assign this from the Unity Editor
    public static bool canMove = true;

    // [SerializeField] private Transform targetTransform;

    // Rotation variables
    private float rotationX = 0;
    private float rotationY = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor for a better first-person experience in desktop
        Cursor.lockState = CursorLockMode.Locked;
        canMove = true; // Enable movement by default
        // Store the initial rotation of the camera at the start
    }

    // Vector3 GetCameraTargetPosition(Transform characterTransform)
    // {
    //     // Assuming you want the camera in front of the character with some offset
    //     float distanceInFront = 0.5f; // How far in front of the character the camera should be
    //     float heightOffset = 1f; // The height offset relative to the character's position

    //     Vector3 offset = characterTransform.forward * distanceInFront;
    //     Vector3 targetPosition = characterTransform.position + offset + Vector3.up * heightOffset;
    //     return targetPosition;
    // }

    public void ToggleMovement()
    {
        canMove = !canMove;
    }

    // public void MoveToTargetPosition()
    // {
    //     StartCoroutine(MoveToPositionCoroutine(targetTransform.position));
    // }

    // public void RotateToTargetRotation()
    // {
    //     StartCoroutine(RotateToRotationCoroutine(targetTransform.rotation));
    // }

    // IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
    // {
    //     while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
    //     {
    //         transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    // public void OnLiveChatButtonClicked()
    // {
    //     Transform currentAvatarTransform = AvatarLoading.CurrentCharacter.transform;
    //     Debug.Log("Current Avatar Position: " + currentAvatarTransform.position);
    //     targetTransform.position = GetCameraTargetPosition(currentAvatarTransform);
    //     Debug.Log("Target Position: " + targetTransform.position);
    //     ToggleMovement(); // Disable movement
    //     MoveToTargetPosition();
    //     RotateToTargetRotation();
    // }


    // IEnumerator RotateToRotationCoroutine(Quaternion targetRotation)
    // {
    //     while (Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
    //     {
    //         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, sensitivity * Time.deltaTime);
    //         yield return null;
    //     }
    // }


    void Update()
    {
        if (!canMove) return; // Skip processing input if movement is disabled

        // Check if the Input Field is focused
        if (inputField != null && inputField.isFocused)
        {
            return; // Stop processing movement keys
        }
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;
        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotation when the right mouse button is held down
        if (Input.GetMouseButton(1)) // Check if right mouse button is held down
        {
            // Get mouse movement to calculate rotation
            rotationX += Input.GetAxis("Mouse X") * sensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * sensitivity; // Subtract to invert Y-axis tilt
            rotationY = Mathf.Clamp(rotationY, -90, 90); // Clamp to prevent flipping

            // Apply rotation to the player object for Yaw (left/right)
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            // Apply rotation to the camera for Pitch (up/down)
            Camera.main.transform.localEulerAngles = new Vector3(rotationY, 0, 0);
        }

        // Android: Rotate view with touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationX += touch.deltaPosition.x * sensitivity * 0.1f; // Scale down touch sensitivity
                rotationY += touch.deltaPosition.y * sensitivity * 0.1f;
                rotationY = Mathf.Clamp(rotationY, -90, 90); // Limit vertical rotation

                // Apply rotation
                transform.localEulerAngles = new Vector3(0, rotationX, 0);
                Camera.main.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
            }
        }
    }
}
