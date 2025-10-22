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
    [SerializeField] GameObject[] trees;
    [SerializeField] GameObject treesPossibleLocations;
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject objectPossibleLocations;
    [SerializeField] HidingPlacesManager hidingPlaces;

    [Space]
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    // [SerializeField] UIManager uIManager;
    // [SerializeField] TMP_Text text_score ;

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
        player = Instantiate(player);
        hidingPlaces = Instantiate(hidingPlaces);
        // uIManager = Instantiate(uIManager);
    }

    private void InitializeObjects()
    {
        // uIManager.Initialize(text_score, 0);
        cameraManager.Initialize(player.transform);
        gameManager.Initialize(player, cameraManager, hidingPlaces);
        
        hidingPlaces.Initialize(gameManager, treesPossibleLocations, trees, objectPossibleLocations, objects);

        player.Initialize(gameManager, player, actions, gameData.PlayerData.playerSpeed, gameData.PlayerData.playerStartPos);
        player.gameObject.SetActive(true);

    }
}
