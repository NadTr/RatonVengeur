using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;

    [Header("Terrain")]
    [SerializeField] Grid terrain;

    [Space]
    [Header("Player")]
    [SerializeField] RaccoonController player;
    [SerializeField] private InputActionAsset actions;

    [Space]
    [Header("HidingPlaces")]
    [SerializeField] int opossumCount = 5;
    [SerializeField] OpossumManager opossum;
    [SerializeField] GameObject[] trees;
    [SerializeField] GameObject treesPossibleLocations;
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject objectPossibleLocations;
    [SerializeField] HidingPlacesManager hidingPlaces;
    [Space]
    [Header("Sounds")]
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource raccoonGrumble;
    [SerializeField] private AudioSource footsteps;


    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    // [SerializeField] UIManager uIManager;
    // [SerializeField] TMP_Text text_score ;
    [SerializeField] UIManager pause;

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
        gameManager = Instantiate(gameManager);
        pause = Instantiate(pause);

        player = Instantiate(player);
        hidingPlaces = Instantiate(hidingPlaces);
        opossum = Instantiate(opossum);
        // uIManager = Instantiate(uIManager);

        bgMusic = Instantiate(bgMusic);
        raccoonGrumble = Instantiate(raccoonGrumble);
        footsteps = Instantiate(footsteps);
    }

    private void InitializeObjects()
    {
        // uIManager.Initialize(text_score, 0);
        cameraManager.Initialize(player.transform);
        gameManager.Initialize(player, actions, cameraManager, hidingPlaces, opossum, pause, opossumCount, bgMusic);
        gameManager.gameObject.SetActive(true);
        pause.Initialize(opossumCount);

        hidingPlaces.Initialize(gameManager, treesPossibleLocations, trees, objectPossibleLocations, objects, opossumCount);
        opossum.Initialize(gameManager, gameData.OpossumData.location_y, gameData.OpossumData.delaySpawn, gameData.OpossumData.delayStartled, gameData.OpossumData.runAwaySpeed);

        player.Initialize(gameManager, player, actions, gameData.PlayerData.playerSpeed, gameData.PlayerData.playerStartPos, raccoonGrumble, footsteps);
        player.gameObject.SetActive(true);

    }
}
