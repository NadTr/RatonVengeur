using UnityEngine;
using UnityEngine.InputSystem;


public class GameInitializer : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CameraManager cameraManager;

    [Space]
    [Header("Player")]
    [SerializeField] RaccoonController player;
    [SerializeField] private InputActionAsset actions;

    [Space]
    [Header("SearchableObjects")]
    [SerializeField] GameObject rock;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject tree;
    [SerializeField] GameObject possibleLocations;

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
        gameManager = Instantiate(gameManager);
        player = Instantiate(player);
        // uIManager = Instantiate(uIManager);
    }

    private void InitializeObjects()
    {
        // uIManager.Initialize(text_score, 0);
        player.Initialize(gameManager,player, actions, gameData.PlayerData.playerSpeed, gameData.PlayerData.playerStartPos);
        cameraManager.Initialize(player.transform);
        gameManager.Initialize(player, cameraManager);
        // gameManager.Initialize(uIManager, player);
    }
}
