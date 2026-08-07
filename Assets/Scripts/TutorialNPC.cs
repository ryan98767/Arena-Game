using UnityEngine;

public class TutorialNPC : NPC, ITalkable
{
    //Being repurposed as cutscene controller
    [SerializeField] protected DialogueText dialogue;
    [SerializeField] protected DialogueController dialogueCon;
    [SerializeField] protected bool isCutscene;


    public override void Interact()
    {
        Talk(dialogue);
    }

    public void Talk(DialogueText dialogue) 
    {
        dialogueCon.DisplayNextParagraph(dialogue);
    }

    public void StartCutscene()
    {
        if (isCutscene)
        {
            Talk(dialogue);
        }
    }
}
