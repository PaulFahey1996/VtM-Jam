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
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if(!dialoguePlaying)
        {
            return;
        }

        //Tempcode. Call Continue Story when submit Action is pressed
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;
        if (keyboard.aKey.wasPressedThisFrame)
        {
            ContinueStory();
        }
        //Tempcode. Call Continue Story when submit Action is pressed

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
