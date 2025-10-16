using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void Pause()
    {
        SceneManager.LoadScene("PauseMenu");
    
    }
}
