using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    RaccoonController player;
    CameraManager cam;
    HidingPlacesManager hidingPlacesManager;
    public void Initialize(RaccoonController player, CameraManager cam, HidingPlacesManager hidingPlacesManager)
    {
        this.player = player;
        this.cam = cam;
        this.hidingPlacesManager = hidingPlacesManager;
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
    public void IsThereAnOpossumThere(GameObject place)
    {
        hidingPlacesManager.IsItOnTheList(place);
    }
    public void SpawnOpossum(Vector3 localisation)
    {
        Debug.Log($"Spawn an opossum in {localisation}");
    }
}
