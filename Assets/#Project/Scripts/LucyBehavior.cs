using UnityEngine;

public class LucyBehavior : MonoBehaviour
{
    GameManager gm;
    LucyBehavior lucy;
    bool isOnScreen;
    bool isQuestStarted;
    public void Initialize(GameManager gm, LucyBehavior lucy)
    {
        this.gm = gm;
        this.lucy = lucy;
        isOnScreen = false;
        isQuestStarted = false;
    }

    public void Process()
    {
        
    }
}
