using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OpossumManager : MonoBehaviour
{
    GameManager gm;
    float location_y = -0.05f;
    float delaySpawn = 0.5f;
    float delayStartled = 0.5f;
    float runAwaySpeed = 2.5f;
    bool isStartled;
    bool isCaught;
    public void Initialize(GameManager gm, float location_y, float delaySpawn, float delayStartled, float runAwaySpeed)
    {
        this.gm = gm;
        this.location_y = location_y;
        this.delaySpawn = delaySpawn;
        this.delayStartled = delayStartled;
        this.runAwaySpeed = runAwaySpeed;
    }

    public void SpawnIn(Vector3 localisation)
    {
        isStartled = false;
        isCaught = false;
        localisation.y -= location_y;
        this.transform.position = localisation;
        this.gameObject.SetActive(true);
        StartCoroutine(RunAway());
    }
    public void Startled()
    {
        if (gameObject.activeSelf)
        {     
            // Debug.Log("opossum startled");
            isStartled = true;
            StartCoroutine(StartledCoroutine());
        }
    }
    public void Caught()
    {
        // Debug.Log("opossum caught");  
        isCaught = true;
        this.gameObject.SetActive(false);
    }
    private IEnumerator StartledCoroutine()
    {
        yield return new WaitForSeconds(delayStartled);
        isStartled = false;
        StartCoroutine(RunAway());  
    }
    private IEnumerator RunAway()
    {
        // while( transform.position)
        yield return new WaitForSeconds(delaySpawn);
        // transform.position += Time.deltaTime * 0.35f * Vector3.right;

        float chrono = 0f;

        while (chrono < 10f && !isStartled)
        {
            chrono += Time.deltaTime;
            transform.position += Time.deltaTime * runAwaySpeed * Vector3.right;
            yield return new WaitForEndOfFrame();
        }
    }
    void OnBecameInvisible()
    {
        if (isCaught) return;
        this.gameObject.SetActive(false);
        gm.SetUpNewOpossumLocation();

    }
}
