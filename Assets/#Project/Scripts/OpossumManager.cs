using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OpossumManager : MonoBehaviour
{
    bool isStartled;
    bool isCaught;
    GameManager gm;
    public void Initialize(GameManager gm)
    {
        this.gm = gm;
    }

    public void SpawnIn(Vector3 localisation)
    {
        isStartled = false;
        isCaught = false;
        localisation.z -= 0.05f;
        this.transform.position = localisation;
        this.gameObject.SetActive(true);
        StartCoroutine(RunAway());
    }
    public void Startled()
    {
        Debug.Log("opossum startled");
        isStartled = true;
         StartCoroutine(StartledCoroutine());
    }
    public void Caught()
    {
        Debug.Log("opossum caught");  
        this.gameObject.SetActive(false);
 
    }
    private IEnumerator StartledCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        isStartled = false;
        StartCoroutine(RunAway());  
    }
    private IEnumerator RunAway()
    {
        // while( transform.position)
        yield return new WaitForSeconds(0.6f);
        // transform.position += Time.deltaTime * 0.35f * Vector3.right;

        float chrono = 0f;

        while (chrono < 10f && !isStartled)
        {
            chrono += Time.deltaTime;
            transform.position += Time.deltaTime * 1.5f * Vector3.right;
            yield return new WaitForEndOfFrame();
        }
    }
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
        gm.SetUpNewOpossumLocation();

    }
}
