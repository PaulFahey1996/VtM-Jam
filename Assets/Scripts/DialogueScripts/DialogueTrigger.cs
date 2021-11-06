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

    private bool playerInRange;



    private void Awake()
    {
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);

            //Temp code. Call player Interaction script here
            var keyboard = Keyboard.current; 
            if (keyboard == null)
                return;
            if(keyboard.eKey.wasPressedThisFrame)
            {
                Debug.Log(inkJSON);
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
          
            //Temp code. Call player Interaction script here

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
}
