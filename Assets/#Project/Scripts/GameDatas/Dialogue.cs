using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Game Design/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] string dialogueQuestStart;
    [SerializeField] string dialogueQuestHint;
    [SerializeField] string dialogueQuestEnd;
    public void Next()
    {
        
    }
}
