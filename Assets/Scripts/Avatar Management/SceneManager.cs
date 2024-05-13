using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // Required for scene management functions
using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Core;

namespace AvatarManagement
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject loading;

        public void HandleAvatarCreated(string avatarUrl)
        {
            loading.SetActive(true);
            // Save the avatar URL to the AvatarData class
            AvatarData.AvatarUrl = avatarUrl;
            Debug.Log("AvatarData.AvatarUrl: " + AvatarData.AvatarUrl);
            loading.SetActive(false);
            LoadNextScene();
        }

        private void LoadNextScene()
        {
            int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;

            if (nextIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextIndex); // Load the next scene by index
            }
            else
            {
                Debug.LogWarning("No next scene available.");
            }
        }
    }
}
