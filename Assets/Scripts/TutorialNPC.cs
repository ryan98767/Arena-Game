using UnityEngine;

public class TutorialNPC : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogue;
    [SerializeField] private DialogueController dialogueCon;

    public override void Interact()
    {
        Talk(dialogue);
    }

    public void Talk(DialogueText dialogue) 
    {
        dialogueCon.DisplayNextParagraph(dialogue);
    }
}
