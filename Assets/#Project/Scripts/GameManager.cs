using UnityEngine;

public class GameManager : MonoBehaviour
{
    RaccoonController player;
    CameraManager cam;
    public void Initialize(RaccoonController player, CameraManager cam)
    {
        this.player = player;
        this.cam = cam;
    }
    void Update()
    {
        player.Process();
        cam.Process();
    }
}
