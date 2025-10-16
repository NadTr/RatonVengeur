using System.Collections.Generic;
using UnityEngine;

public class HidingPlacesManager : MonoBehaviour
{
    private GameObject possibleLocations;
    private GameObject[] prefabs;
    private Dictionary<Transform, GameObject> hidingPlacesLocations;
    public void Initialize(GameObject possibleLocations, GameObject[] prefabs)
    {
        this.possibleLocations = possibleLocations;
        this.prefabs = prefabs;
        hidingPlacesLocations = new Dictionary<Transform, GameObject>();
        SetUpLocations();
    }
    private void SetUpLocations()
    {
        for (int i = 0; i < possibleLocations.transform.childCount; i++)
        {
            int rnd = Random.Range(0, prefabs.Length);
            Transform childPos = possibleLocations.transform.GetChild(i);
            Debug.Log($"boucle {i} : posiiton {childPos.position}");
            hidingPlacesLocations.Add(childPos, prefabs[rnd]);
            Instantiate(prefabs[rnd], childPos.position, Quaternion.identity);
        }


    }
    public void Process()
    {
        
    }
}