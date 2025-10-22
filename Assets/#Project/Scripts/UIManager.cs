using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TMP_Text questText;
    int totalToCatch;
    public void Initialize(int totalToCatch)
    {
        if (this.transform.Find("QuestText").TryGetComponent<TMP_Text>(out TMP_Text questText))
        {
            this.questText = questText; 
        }
        this.totalToCatch = totalToCatch;
        questText.SetText($"Nombre d'opossums trouvés : 0/{totalToCatch}");
    }
    public void UpdateQuest(int count)
    {
        questText.SetText($"Nombre d'opossums trouvés : {count}/{totalToCatch}");
    }
}
