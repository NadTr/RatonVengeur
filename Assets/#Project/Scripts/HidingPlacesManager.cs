using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class HidingPlacesManager : MonoBehaviour
{
    private GameManager gm;
    private Dictionary<Transform, GameObject> hidingPlacesLocations;
    private Dictionary<Vector3, GameObject> opossumLocations;
    public void Initialize(GameManager gm,GameObject treesLocations, GameObject[] treesPrefabs, GameObject objectsLocations, GameObject[] ObjectPrefabs, int opossumCount)
    {
        this.gm = gm;
        hidingPlacesLocations = new Dictionary<Transform, GameObject>();
        opossumLocations = new Dictionary<Vector3, GameObject>();
        SetUpLocations(treesLocations, treesPrefabs);
        SetUpLocations(objectsLocations, ObjectPrefabs);
        // SetUpOpossums(opossumCount);
    }
    private void SetUpLocations(GameObject possibleLocations, GameObject[] prefabs)
    {
        for (int i = 0; i < possibleLocations.transform.childCount; i++)
        {
            int rnd = Random.Range(0, prefabs.Length);
            Transform childPos = possibleLocations.transform.GetChild(i);
            hidingPlacesLocations.Add(childPos, prefabs[rnd]);
            Instantiate(prefabs[rnd], childPos.position, Quaternion.identity);
        }
    }

    public void SetUpOpossums(int n)
    {
        for (int i = 0; i < n; i++)
        {
            SaveOpossumLocation();
        }
    }

    private void SaveOpossumLocation()
    {
        int rnd = Random.Range(0, hidingPlacesLocations.Count);
        while (opossumLocations.ContainsKey(hidingPlacesLocations.ElementAt(rnd).Key.position))
        {
            // Debug.Log(rnd);
            rnd = Random.Range(0, hidingPlacesLocations.Count);
        }

        Debug.Log($"{rnd} at position : {hidingPlacesLocations.ElementAt(rnd).Key.position}");
        opossumLocations.Add(hidingPlacesLocations.ElementAt(rnd).Key.position, hidingPlacesLocations.ElementAt(rnd).Value);
    }

    public void IsItOnTheList(GameObject place)
    {
        StartCoroutine(ShakeTree(place, 0.3f));
        if (opossumLocations.ContainsKey(place.transform.position))
        {
            gm.SpawnOpossum(place.transform.position);
            opossumLocations.Remove(place.transform.position);
            // Debug.Log($" 2 = {opossumLocations.ContainsKey(place.transform.position)}");
        }
    }
    private IEnumerator ShakeTree(GameObject place, float delay = 2.5f)
    {
        Animator animator = place.transform.gameObject.GetComponent<Animator>();
        animator.SetBool("shake", true);        
        yield return new WaitForSeconds(delay);
        animator.SetBool("shake", false);    
    }
    
    public void SaveAnotherLocation()
    {
        SaveOpossumLocation();
    }
}