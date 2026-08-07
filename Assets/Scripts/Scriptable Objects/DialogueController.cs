using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using PlayerMovementNameSpace;
using System.Collections;
using UnityEngine.Events;
using PlayerState;


public class DialogueController : MonoBehaviour
{
    public UnityEvent onConversationEnd;

    [SerializeField] protected TextMeshProUGUI NPCNameText;
    [SerializeField] protected TextMeshProUGUI NPCDialogue;
    [SerializeField] protected Image portraitImage;
    [SerializeField] protected PlayerStates stateManager;
    [SerializeField] protected float typeSpeed = 10;

    protected Queue<DialogueLine> lines = new Queue<DialogueLine>();
    protected bool convoEnded;
    protected DialogueLine currentLine;
    protected Coroutine typeDialogueCoroutine;
    protected const string HTML_ALHPA = "<color=#00000000>";   
    protected const float maxTypeTime = 0.1f;

    protected bool isTyping = false;

    public void DisplayNextParagraph(DialogueText dialogue)
    {
        if (lines.Count == 0)
        {
            if (!convoEnded)
            {
                StartConversation(dialogue);
                Debug.Log("Starting Conversation!");
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
            currentLine = lines.Dequeue();
            NPCNameText.text = currentLine.speakerName;
            portraitImage.sprite = currentLine.portrait;
            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(currentLine.line));
        }

        else
        {
            FinishParagraphEarly();
        }

        if (lines.Count == 0)
        {
            convoEnded = true;
            Debug.Log("Conversation Ended!");
        }
    }

    protected void StartConversation(DialogueText dialogue)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        stateManager.SetState(States.InDialogue);

        foreach (DialogueLine line in dialogue.lines)
        {
            lines.Enqueue(line);
        }
    }

    protected void EndConversation()
    {
        convoEnded = true;
        stateManager.SetState(States.Normal);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        onConversationEnd?.Invoke();
    }

    protected IEnumerator TypeDialogueText(string p)
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

    protected void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);

        NPCDialogue.text = currentLine.line;
        isTyping = false;
    }
}
