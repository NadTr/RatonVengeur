using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlacesManager : MonoBehaviour
{
    private GameObject possibleLocations;
    private GameObject[] prefabs;
    private Dictionary<Transform, GameObject> hidingPlacesLocations;
    private Dictionary<Transform, GameObject> opossumLocations;
    public void Initialize(GameObject possibleLocations, GameObject[] prefabs)
    {
        this.possibleLocations = possibleLocations;
        this.prefabs = prefabs;
        hidingPlacesLocations = new Dictionary<Transform, GameObject>();
        SetUpLocations();
        // SetUpOpossums(1);
    }
    private void SetUpLocations()
    {
        for (int i = 0; i < possibleLocations.transform.childCount; i++)
        {
            int rnd = Random.Range(0, prefabs.Length);
            Transform childPos = possibleLocations.transform.GetChild(i);
            hidingPlacesLocations.Add(childPos, prefabs[rnd]);
            Instantiate(prefabs[rnd], childPos.position, Quaternion.identity);
        }
    }

    private void SetUpOpossums(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int rnd = Random.Range(0, hidingPlacesLocations.Count);
            opossumLocations.Add(hidingPlacesLocations.ElementAt(rnd).Key, hidingPlacesLocations.ElementAt(rnd).Value);
        }
        Debug.Log(opossumLocations);
    }


    public void Process()
    {
        
    }
}