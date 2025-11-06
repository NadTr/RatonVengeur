using UnityEngine;

public class LucyBehavior : MonoBehaviour
{
    GameManager gm;
    LucyBehavior lucy;
    GameObject lucyLocations;
    bool isOnScreen;
    bool isQuestStarted;
    bool isQuestCompleted ;
    public void Initialize(GameManager gm, LucyBehavior lucy, GameObject lucyLocations)
    {
        this.gm = gm;
        this.lucy = lucy;
        this.lucyLocations = lucyLocations;
        isOnScreen = false;
        isQuestStarted = false;
        isQuestCompleted = false;
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
        isQuestCompleted = true;
    }
    public void LucyDialogue()
    {
        if (!isQuestStarted)
        {
            //start quest dialogue
            isQuestStarted = true;
        }
        else
        {
            if (isQuestCompleted)
            {
                //start dialogue end of quest 
                //gm.OpenWayToNextScene()
            }
            else
            {
                //start dialogue to give hint to use A to startle the babies
            }
        }
    }
}
