using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject player;
    public GameObject deactivateWeapons;

    private DialogueNode currentNode;
    public float playerInRange = 2f;
    private bool canDisable;
    private int index;

    [System.Serializable]
    public class DialogueNode
    {
        public string npcDialogue;
        public DialogueNode[] playerResponses;

        public DialogueNode(string dialogue)
        {
            npcDialogue = dialogue;
        }
    }

    void Start()
    {
        //dialoguePanel.SetActive(false);

        DialogueNode node1 = new DialogueNode("Welcome to this Dungeon! I'm here to give you some tips. Press the left mouse button to go to the next tip.");
        DialogueNode node2 = new DialogueNode("Use the WASD keys to move.");
        DialogueNode node3 = new DialogueNode("Use your left mouse button to attack and the scroll wheel to change weapons.");
        DialogueNode node4 = new DialogueNode("Use the space-bar to dash and avoid some enemies attacks.");
        DialogueNode node5 = new DialogueNode("Press the Tab button to open your inventory.");
        DialogueNode node6 = new DialogueNode("Press the E button to interact with people or environment.");

        
        node1.playerResponses = new DialogueNode[] { node2 };
        node2.playerResponses = new DialogueNode[] { node3 };
        node3.playerResponses = new DialogueNode[] { node4 };
        node4.playerResponses = new DialogueNode[] { node5 };
        node5.playerResponses = new DialogueNode[] { node6 };

        
        currentNode = node1;

        UpdateDialogueText();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.transform.position) < playerInRange)
        {
            ToggleDialoguePanel();
            deactivateWeapons.SetActive(false);
            canDisable = true;

        }
        else if (Vector2.Distance(transform.position, player.transform.position) > playerInRange && canDisable)
        {
            dialoguePanel.SetActive(false);
            deactivateWeapons.SetActive(true);
            canDisable = false;
        }

        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeInHierarchy)
        {
            DisplayNextDialogue();
        }
    }

    void ToggleDialoguePanel()
    {
        dialoguePanel.SetActive(!dialoguePanel.activeSelf);

        if (dialoguePanel.activeSelf)
        {
            UpdateDialogueText();
        }
        else
        {
            zeroText();
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    

    public void UpdateDialogueText()
    {
        dialogueText.text = currentNode.npcDialogue;
    }

    public void DisplayNextDialogue()
    {
        if (currentNode != null && currentNode.playerResponses != null && currentNode.playerResponses.Length > 0)
        {
            index = (index + 1) % currentNode.playerResponses.Length;
            currentNode = currentNode.playerResponses[index];

            UpdateDialogueText();
        }
    }

}