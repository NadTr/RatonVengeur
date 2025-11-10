using UnityEngine;

public class LucyBehavior : MonoBehaviour
{
    GameManager gm;
    LucyBehavior lucy;
    GameObject lucyLocations;
    bool isOnScreen;
    public bool IsQuestStarted  { get; set; }
    public bool IsQuestCompleted  { get; set; }
    public void Initialize(GameManager gm, LucyBehavior lucy, GameObject lucyLocations)
    {
        this.gm = gm;
        this.lucy = lucy;
        this.lucyLocations = lucyLocations;
        isOnScreen = false;
        IsQuestStarted = false;
        IsQuestCompleted = false;
        // GetLocationsList();
        Appear();
    }

    public void Process()
    {
        if (isOnScreen)
        {
            LucyMove();
        }
    }
    private void Appear()
    {
        
        this.transform.position = lucyLocations.transform.GetChild(0).localPosition;
        this.gameObject.SetActive(true);
        // StartCoroutine(Pacing());
    }

    private void LucyMove()
    {
        
    }
    public void MarkQuestAsCompleted()
    {
        IsQuestCompleted = true;
    }
    public void LucyDialogue()
    {
        if (!IsQuestStarted)
        {
            //start quest dialogue
            Debug.Log("debut de la quête");
            gm.StartOpossumQuest();
            IsQuestStarted = true;
        }
        else
        {
            if (IsQuestCompleted)
            {
                Debug.Log("fin de la quête");
                //start dialogue end of quest 
                //gm.OpenWayToNextScene()
            }
            else
            {
                Debug.Log("conseil");
                //start dialogue to give hint to use A to startle the babies
            }
        }
    }
}
