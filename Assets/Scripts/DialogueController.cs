using UnityEngine;
using TMPro;
using System.Collections.Generic;
using PlayerMovementNameSpace;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogue;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();
    private bool convoEnded;

    private string p;
    private Coroutine typeDialogueCoroutine;
    private const string HTML_ALHPA = "<color=#00000000>";   
    private const float maxTypeTime = 0.1f;

    private bool isTyping = false;

    public void DisplayNextParagraph(DialogueText dialogue)
    {
        if (paragraphs.Count == 0)
        {
            if (!convoEnded)
            {
                StartConversation(dialogue);    
            }
            else if (convoEnded && !isTyping)
            { 
                EndConversation();
                convoEnded = false;
                return;
            }
        }

        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }

        else
        {
            FinishParagraphEarly();
        }


        if (paragraphs.Count == 0)
        {
            convoEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogue)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        NPCNameText.text = dialogue.speakerName;
        playerMove.enabled = false;

        for (int i = 0; i < dialogue.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogue.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        convoEnded = true;
        playerMove.enabled = true;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        NPCDialogue.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogue.text = originalText;

            displayedText = NPCDialogue.text.Insert(alphaIndex, HTML_ALHPA);
            NPCDialogue.text = displayedText;

            yield return new WaitForSeconds(maxTypeTime / typeSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);

        NPCDialogue.text = p;
        isTyping = false;
    }
}
