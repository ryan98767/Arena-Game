using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;

    [TextArea(5, 10)]
    public string line;

    public Sprite portrait;
}

[System.Serializable]
public class DialogueText
{
    public DialogueLine[] lines;
}
