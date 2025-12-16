using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] GameObject playInstruct;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnPlay1Button ()
    {
        playInstruct.SetActive(true);
        // SceneManager.LoadScene("Parc");
    }
   public void OnPlayButton ()
    {
        SceneManager.LoadScene("Parc");
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton ()
    {
        Application.Quit();
    }
}
