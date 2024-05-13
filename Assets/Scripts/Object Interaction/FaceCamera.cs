using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AvatarManagement;

public class FaceCamera : MonoBehaviour
{
    public Camera playerCamera; // Assign your main camera in the inspector
    public float panSpeed = 8f; // Adjust for faster or slower panning
    private bool isLiveChatOpen = false;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    public GameObject player;
    public Transform focusPoint; // An optional focus point attached to your character controller


    void Start()
    {
        // Assuming the initial camera setup is focusing on the player or a specific focus point on the player
        if (focusPoint == null)
        {
            focusPoint = player.transform; // Fallback to player's transform if no specific focus point is set
        }
        transform.LookAt(focusPoint);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    
    Vector3 GetCameraTargetPosition(Transform characterTransform)
    {
        // Assuming you want the camera in front of the character with some offset
        float distanceInFront = 1f; // How far in front of the character the camera should be
        float heightOffset = 1.5f; // The height offset relative to the character's position

        Vector3 offset = characterTransform.forward * distanceInFront;
        Vector3 targetPosition = characterTransform.position + offset + Vector3.up * heightOffset;
        return targetPosition;
    }

    IEnumerator ResetCameraPositionAndRotation()
    {
        while (playerCamera.transform.localPosition != initialLocalPosition || playerCamera.transform.localRotation != initialLocalRotation)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, initialLocalPosition, panSpeed * Time.deltaTime);
            playerCamera.transform.localRotation = Quaternion.Lerp(playerCamera.transform.localRotation, initialLocalRotation, panSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Camera reset to initial focus");
    }

    public void OnLiveChatButtonClicked()
    {
        if (!isLiveChatOpen)
        {
            // Open live chat and pan camera to character
            initialLocalPosition = playerCamera.transform.localPosition;
            initialLocalRotation = playerCamera.transform.localRotation;
            Transform currentAvatarTransform = AvatarLoading.CurrentCharacter.transform;
            StartCoroutine(PanCameraToCharacter(currentAvatarTransform));
            isLiveChatOpen = true;
        }
        else
        {
            // Close live chat and reset camera rotation
            StartCoroutine(ResetCameraPositionAndRotation());
            isLiveChatOpen = false;

        }
    }

    IEnumerator PanCameraToCharacter(Transform targetTransform)
    {
        Debug.Log("Panning camera to character");
        Vector3 targetPosition = GetCameraTargetPosition(targetTransform);

        // Pan the camera to the target position
        while (Vector3.Distance(playerCamera.transform.position, targetPosition) > 0.1f)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, targetPosition, panSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Camera panned and tilted upwards to character");
    }

}
