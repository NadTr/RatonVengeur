using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] GameObject playInstruct;
         private const string ACTION_MAP = "UIandMenu";
        private const string START = "Start";
        private const string QUIT = "Quit";
        bool startGame = false;
    [SerializeField] private InputActionAsset actions;
     void OnEnable()
    {
        actions.FindActionMap(ACTION_MAP).Enable();
        actions.FindActionMap(ACTION_MAP).FindAction(START).performed += OnStart;
        actions.FindActionMap(ACTION_MAP).FindAction(QUIT).performed += OnQuit;
    }

    void OnDisable()
    {
        actions.FindActionMap(ACTION_MAP).Disable();
        actions.FindActionMap(ACTION_MAP).FindAction(START).performed -= OnStart;
        actions.FindActionMap(ACTION_MAP).FindAction(QUIT).performed -= OnQuit;
    }

     public void OnStart(InputAction.CallbackContext callbackContext)
    {
        if (!startGame)
        {
            OnNextButton();
        }
        else
        {
            OnPlayButton();
        }
    }
     public void OnQuit(InputAction.CallbackContext callbackContext)
    {
        OnQuitButton();
    }

   public void OnNextButton ()
    {
        playInstruct.SetActive(true);
        startGame = true;
    }
   public void OnPlayButton ()
    {
        SceneManager.LoadScene("Parc");
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
