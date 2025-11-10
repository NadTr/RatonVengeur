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
            questText.SetText(" ");
        }
    }
    public void LaunchOpossumQuest(int totalToCatch)
    {

        this.totalToCatch = totalToCatch;
        questText.SetText($"Nombre d'opossums trouvés : 0/{totalToCatch}");
    }
    public void UpdateOpossumQuest(int count)
    {
        questText.SetText($"Nombre d'opossums trouvés : {count}/{totalToCatch}");
    }
}
