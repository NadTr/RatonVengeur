using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    GameManager gameManager;
    TMP_Text dialogueText;
    string[] dialogueActive;
    string[] startQuestDialogueLines = { "Bonjour", "J'ai besoin d'aide pour trouver mes enfants", "Ils se sont cachés dans les objets et les arbres de ce parc" };
    string[] HintDialogueLines = { "Rebonjour", "Si vous avez besoin d'aide, voici un conseil", "N'hésitez pas à faire du bruit pour leur faire peur", "Essayez d'appuyer sur A / X quand l'un d'eux apparaît" };
    string[] EndQuestDialogueLines = { "Merci", "J'ai dégagé un passage dans la cloture si ça vous intéresse, allez voir vers l'Ouest" };
    int i;
    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;

        if (this.transform.GetChild(0).GetChild(0).Find("Dialogue_Text").TryGetComponent<TMP_Text>(out TMP_Text questText))
        {
            this.dialogueText = questText;
            questText.SetText(" ");
        }
        // dialogueLines = new string[5];
        // dialogueLines = ["Bonjour", "J'ai besoin d'aide pour trouver mes enfants", "Ils sont caché dans les objets et les arbres de ce parc"];
    }
    public void StartDialogue(string dialogueName)
    {
        i = 0;
        // Debug.Log(dialogueName);
        if (dialogueName == "beginQuest")
        {
            dialogueActive = startQuestDialogueLines;
            // Debug.Log("beginQuest inside if");
        }
        if (dialogueName == "endQuest")
        {
            dialogueActive = EndQuestDialogueLines;
        }
        if (dialogueName == "hint")
        {
            dialogueActive = HintDialogueLines;
        }
        ShowDialogue(i, dialogueActive);
        // Debug.Log(dialogueActive[1]);

    }
    public void NextLine()
    {
        i++;
        ShowDialogue(i, dialogueActive);
    }

    public void ShowDialogue(int i, string[] dialogueLines)
    {
        if (i < dialogueLines.Length)
        {
            // dialogueText.SetText([i]);
            dialogueText.SetText(dialogueLines[i]);
        }
        else
        {
            gameManager.EndOfDialogue();
            i = 0;
        }
    }
    
}
