using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public Text dialogueTextUI;

    public void DisplayDialogue(string dialogue)
    {
        dialogueTextUI.text = dialogue;
    }
}
