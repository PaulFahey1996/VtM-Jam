using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("INK JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField]private bool playerInRange;

    [SerializeField] private long _iD;


    private void OnEnable()
    {
        DialogueManager.GetInstance().SubscribeToOnDialogueTriggered(StartDialogue);
    }

    private void OnDisable()
    {
        DialogueManager.GetInstance().UnSubscribeOnDialogueTriggered(StartDialogue);
    }

    private void Awake()
    {
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("Player is in range.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            Debug.Log("Bye, Felicia.");
        }
    }

    private void StartDialogue(string tag)
    {
        if (inkJSON != null)
        {
            Debug.Log("start ink");
            //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }

    }
}
