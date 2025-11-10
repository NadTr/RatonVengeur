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
    LucyBehavior lucy;
    HidingPlacesManager hidingPlacesManager;
    BabyOpossumBehavior babyOpossum;
    UIManager pauseMenu;
    SoundManager soundManager;
    AudioSource bgMusic;
    bool onPause;
    bool isOpossumSpawned;
    private int numberOfOpossumCaught;
    private int numberOfOpossumToCatch;
    public void Initialize(RaccoonController player, InputActionAsset actions, CameraManager cam, HidingPlacesManager hidingPlacesManager, LucyBehavior lucy, BabyOpossumBehavior babyOpossum, UIManager pauseMenu, int opossumCount, SoundManager soundManager)
    {
        this.player = player;
        this.actions = actions;
        this.cam = cam;
        this.lucy = lucy;
        this.hidingPlacesManager = hidingPlacesManager;
        this.babyOpossum = babyOpossum;
        this.pauseMenu = pauseMenu;
        this.soundManager = soundManager;
        // this.bgMusic = bgMusic;

        isOpossumSpawned = false;
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

    public void RaccoonGrumble(Transform raccoon)
    {
        Vector3 distance3 = raccoon.position - babyOpossum.transform.position;
        float distance = Vector3.Distance(raccoon.position, babyOpossum.transform.position);
        // Debug.Log($"distance = {distance}");
        if (distance < 2f && isOpossumSpawned)
        {
            StartleOpossum();
            soundManager.RaccoonNoise("grumble");
        }
        else
        {
            soundManager.RaccoonNoise("chatter");
        }
    }
    
    public void WaterFallSound(Transform playerTransform, bool playerIsClose)
    {
        soundManager.WaterFallSoundBool(playerIsClose);
        soundManager.WaterFallSound(playerTransform);
    }

    public void LucyDialogue()
    {
        lucy.LucyDialogue();
    }
    public void StartOpossumQuest()
    {
        pauseMenu.LaunchOpossumQuest(numberOfOpossumToCatch);
        hidingPlacesManager.SetUpOpossums(numberOfOpossumToCatch);
    }
    public void IsThereAnOpossumThere(GameObject place)
    {
        hidingPlacesManager.IsItOnTheList(place);
        soundManager.rummageSoundPlay();

    }
    public void SpawnOpossum(Vector3 localisation)
    {
        babyOpossum.SpawnIn(localisation);
        isOpossumSpawned = true;
        Debug.Log($"Spawn an opossum in {localisation}");
    }
    

    public void StartleOpossum()
    {
        babyOpossum.Startled();
    }
    public void CatchOpossum()
    {
        numberOfOpossumCaught++;

        babyOpossum.Caught();
        pauseMenu.UpdateOpossumQuest(numberOfOpossumCaught);
        isOpossumSpawned = false;

        if(numberOfOpossumCaught == numberOfOpossumToCatch)
        {
            Debug.Log("All opossum caught!");
            lucy.MarkQuestAsCompleted();
            //make a sound and show something to notify that the quest is completed
        }
    }
    public void SetUpNewOpossumLocation()
    {
        hidingPlacesManager.SaveAnotherLocation();
        isOpossumSpawned = false;

    }

}
