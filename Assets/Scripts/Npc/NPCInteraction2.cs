using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction2 : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject player;
    public GameObject Weapon1;
    public GameObject Weapon2;

    private DialogueNode currentNode;
    public float playerInRange = 2f;
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
        dialoguePanel.SetActive(false);

        DialogueNode node1 = new DialogueNode("Im here to help you defeat Luminaris the evil God of thunder.");
        DialogueNode node2 = new DialogueNode("Go to the chests to receive my bottles of divine water it will give you greater health.");
        DialogueNode node3 = new DialogueNode("You can also have my old weapon");

        
        node1.playerResponses = new DialogueNode[] { node2 };
        node2.playerResponses = new DialogueNode[] { node3 };

        
        currentNode = node1;

        UpdateDialogueText();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(transform.position, player.transform.position) < playerInRange)
        {
            ToggleDialoguePanel();
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);

        }
        else if (Vector2.Distance(transform.position, player.transform.position) > playerInRange)
        {
            dialoguePanel.SetActive(false);
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1) && dialoguePanel.activeInHierarchy)
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