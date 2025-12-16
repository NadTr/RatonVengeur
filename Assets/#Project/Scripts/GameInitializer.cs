using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;

    [Header("Terrain")]
    [SerializeField] Grid terrain;
    [SerializeField] GameObject waterfallPosition;
    GameObject rockToDestroy;
    [Space]

    [Header("Player")]
    [SerializeField] RaccoonController player;
    [SerializeField] private InputActionAsset actions;
    [Space]

    [Header("PNJ Sprites")]
    [SerializeField] LucyBehavior lucy;
    [SerializeField] BabyOpossumBehavior babyOpossum;
    [SerializeField] GameObject lucyLocations;
    [Space]

    [Header("HidingPlaces")]
    [SerializeField] int opossumCount = 5;
    [SerializeField] GameObject[] trees;
    [SerializeField] GameObject treesPossibleLocations;
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject objectPossibleLocations;
    [SerializeField] HidingPlacesManager hidingPlaces;
    [Space]

    [Header("Sounds")]
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource raccoonGrumble;
    [SerializeField] private AudioSource raccoonChatter;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource rummageSound;
    [SerializeField] private AudioSource opossumSound;
    [SerializeField] private AudioSource waterFallSound;
    [SerializeField] private AudioMixerSnapshot snapshotNormal;
    [SerializeField] private AudioMixerSnapshot snapshotWaterFall;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AnimationCurve animationCurveWaterFall;




    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    // [SerializeField] UIManager uIManager;
    // [SerializeField] TMP_Text text_score ;
    [SerializeField] SoundManager soundManager;
    [SerializeField] UIManager pauseCanva;
    [SerializeField] DialogueManager dialogueCanva;

    [Space]
    [Header("GameData")]
    [SerializeField] private GameData gameData;
 void Start()
    {
        CreateObjects();
        InitializeObjects();
        Destroy(gameObject);
    }

    private void CreateObjects()
    {
        cameraManager = Instantiate(cameraManager);
        terrain = Instantiate(terrain);
        waterfallPosition = Instantiate(waterfallPosition);
        rockToDestroy = terrain.transform.Find("SpritesOnMap").Find("RockToDestroy").gameObject;

        gameManager = Instantiate(gameManager);
        pauseCanva = Instantiate(pauseCanva);
        dialogueCanva = Instantiate(dialogueCanva);

        player = Instantiate(player);
        lucy = Instantiate(lucy);
        hidingPlaces = Instantiate(hidingPlaces);
        babyOpossum = Instantiate(babyOpossum);
        // uIManager = Instantiate(uIManager);

        bgMusic = Instantiate(bgMusic);
        raccoonGrumble = Instantiate(raccoonGrumble);
        raccoonChatter = Instantiate(raccoonChatter);
        rummageSound = Instantiate(rummageSound);
        footsteps = Instantiate(footsteps);
        opossumSound = Instantiate(opossumSound);
        waterFallSound = Instantiate(waterFallSound);
    }

    private void InitializeObjects()
    {
        // uIManager.Initialize(text_score, 0);
        cameraManager.Initialize(player.transform);
        gameManager.Initialize(player, actions, cameraManager, hidingPlaces, lucy, babyOpossum, pauseCanva, dialogueCanva, opossumCount, soundManager, rockToDestroy);
        soundManager.Initialize(bgMusic, raccoonGrumble, raccoonChatter, rummageSound, opossumSound, waterFallSound, snapshotNormal, snapshotWaterFall, audioMixer, animationCurveWaterFall);
        gameManager.gameObject.SetActive(true);

        pauseCanva.Initialize();
        dialogueCanva.Initialize(gameManager);

        lucy.Initialize(gameManager, lucy, lucyLocations);

        hidingPlaces.Initialize(gameManager, treesPossibleLocations, trees, objectPossibleLocations, objects, opossumCount);
        babyOpossum.Initialize(gameManager, gameData.OpossumData.location_y, gameData.OpossumData.delaySpawn, gameData.OpossumData.delayStartled, gameData.OpossumData.runAwaySpeed);

        player.Initialize(gameManager, player, actions, gameData.PlayerData.playerSpeed, gameData.PlayerData.playerStartPos, footsteps);
        player.gameObject.SetActive(true);

    }
}
