using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TMP_Text questText;
    int totalToCatch;
    public void Initialize()
    {
        if (this.transform.Find("QuestText").TryGetComponent<TMP_Text>(out TMP_Text questText))
        {
            this.questText = questText; 
            questText.SetText("Va parler à Lucie");
        }
    }
    public void LaunchOpossumQuest(int totalToCatch)
    {
        this.totalToCatch = totalToCatch;
        questText.SetText($"Nombre d'opossums trouvés : 0/{totalToCatch}");
    }
    public void UpdateOpossumQuest(int count)
    {
        questText.SetText($"Nombre d'opossums trouvés : {count}/{totalToCatch} {(count == totalToCatch? "\n \t Retourne vers Lucie" : "")}");
    }
    public void EndOpossumQuest()
    {
        questText.SetText($"Trouve la sortie ");
    }
}
