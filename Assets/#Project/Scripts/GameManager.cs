using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
     private const string ACTION_MAP = "UIandMenu";
    private const string PAUSE = "Pause";
    private InputActionAsset actions;
    private InputAction pause;
    RaccoonController player;
    CameraManager cam;
    HidingPlacesManager hidingPlacesManager;
    OpossumManager opossum;
    UIManager pauseMenu;
    AudioSource bgMusic;
    bool onPause;
    private int numberOfOpossumCaught;
    private int numberOfOpossumToCatch;
    public void Initialize(RaccoonController player, InputActionAsset actions, CameraManager cam, HidingPlacesManager hidingPlacesManager, OpossumManager opossum, UIManager pauseMenu, int opossumCount, AudioSource bgMusic)
    {
        this.player = player;
        this.actions = actions;
        this.cam = cam;
        this.hidingPlacesManager = hidingPlacesManager;
        this.opossum = opossum;
        this.pauseMenu = pauseMenu;
        this.bgMusic = bgMusic;

        onPause = false;
        numberOfOpossumCaught = 0;
        numberOfOpossumToCatch = opossumCount;
    }
    void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        actions.FindActionMap(ACTION_MAP).FindAction(PAUSE).performed += OnPause;
    }


    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction(PAUSE).performed -= OnPause;
    }

    void Update()
    {
        player.Process();
        cam.Process();
    }

    public void OnPause(InputAction.CallbackContext callbackContext)
    {
        onPause = !onPause;
        Debug.Log(onPause);
        Time.timeScale = onPause? 0f : 1f;
        // player.gameObject.SetActive(!onPause);
        pauseMenu.gameObject.SetActive(onPause);
    }

    public void IsThereAnOpossumThere(GameObject place)
    {
        hidingPlacesManager.IsItOnTheList(place);
    }
    public void SpawnOpossum(Vector3 localisation)
    {
        opossum.SpawnIn(localisation);
        Debug.Log($"Spawn an opossum in {localisation}");
    }
    public void RaccoonGrumble(Transform raccoon)
    {
        Vector3 distance3 = raccoon.position - opossum.transform.position;
        float distance = Vector3.Distance (raccoon.position, opossum.transform.position);
        Debug.Log($"distance = {distance}");
        if(distance < 3f)
        {
            StartleOpossum();
        }
    }
    public void StartleOpossum()
    {
        opossum.Startled();
    }
    public void CatchOpossum()
    {
        numberOfOpossumCaught++;

        opossum.Caught();
        pauseMenu.UpdateQuest(numberOfOpossumCaught);
        if(numberOfOpossumCaught == numberOfOpossumToCatch)
        {
            Debug.Log("All opossum caught!");
        }
    }
    public void SetUpNewOpossumLocation()
    {
        hidingPlacesManager.SaveAnotherLocation();
    }

}
