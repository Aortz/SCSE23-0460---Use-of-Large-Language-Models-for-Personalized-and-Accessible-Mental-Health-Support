using UnityEngine.Networking;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OpenAI;
using AssistantModel;
using ThreadModels;
using MessageModel;
using RunModel;
using TMPro;
using HelperFunction;

public class PersonalityManager : MonoBehaviour
{
    public GameObject personalityPanel; // Assign in the Inspector

    public void togglePersonalityPanel()
    {
        if (personalityPanel.activeSelf)
        {
            personalityPanel.SetActive(false);
        }
        else
        {
            personalityPanel.SetActive(true);
        }
    }
}