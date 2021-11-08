using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    private bool dialoguePlaying;

    private static DialogueManager instance;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple Dialogue Managers detected.");
        }
        instance = this;
        Debug.Log(instance);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePlaying = false;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if(!dialoguePlaying)
        {
            return;
        }

        //Tempcode. Call Continue Story when submit Action is pressed
        var mouse = Mouse.current;
        if (mouse == null)
            return;
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            ContinueStory();
        }
        //Tempcode. Call Continue Story when submit Action is pressed

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialoguePlaying = false;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (dialogueText != null)
            {
                dialogueText.text = currentStory.Continue();
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }


    #region Events

    private event Action<string> OnDialogueTriggerd;

    public void SubscribeToOnDialogueTriggered(Action<string> startDialogue)
    {
        if (startDialogue != null)
        {
            OnDialogueTriggerd -= startDialogue;
            OnDialogueTriggerd += startDialogue;
        }
    }

    public void UnSubscribeOnDialogueTriggered(Action<string> startDialogue)
    {
        if (startDialogue != null)
        {
            OnDialogueTriggerd -= startDialogue;
        }
    }

    #endregion

    public void TriggerInteractionByTag(string gameObjectTag)
    {
        if (OnDialogueTriggerd != null)
        {
            OnDialogueTriggerd(gameObjectTag);
        }
    }
}
