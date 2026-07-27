using UnityEngine;

public class TutorialNPC : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Talk(dialogue);
    }

    public void Talk(DialogueText dialogue) 
    {
        
    }
}
