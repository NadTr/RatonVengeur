using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    RaccoonController player;
    CameraManager cam;
    HidingPlacesManager hidingPlacesManager;
    OpossumManager opossum;
    UIManager pause;
    bool onPause;
    public void Initialize(RaccoonController player, CameraManager cam, HidingPlacesManager hidingPlacesManager, OpossumManager opossum, UIManager pause)
    {
        this.player = player;
        this.cam = cam;
        this.hidingPlacesManager = hidingPlacesManager;
        this.opossum = opossum;
        this.pause = pause;
        onPause = false;
    }

    void Update()
    {
        player.Process();
        cam.Process();
    }

    public void Pause()
    {
        Debug.Log(onPause);
        Time.timeScale = onPause? 0f : 1f;
        // player.gameObject.SetActive(!onPause);
        pause.gameObject.SetActive(onPause);
        onPause = !onPause;
    }

    public void IsThereAnOpossumThere(GameObject place)
    {
        hidingPlacesManager.IsItOnTheList(place);
    }
    public void SetUpNewOpossumLocation()
    {
        hidingPlacesManager.SaveAnotherLocation();
    }

    public void SpawnOpossum(Vector3 localisation)
    {
        opossum.SpawnIn(localisation);
        Debug.Log($"Spawn an opossum in {localisation}");
    }
}
