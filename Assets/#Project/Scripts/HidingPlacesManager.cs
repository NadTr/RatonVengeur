using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlacesManager : MonoBehaviour
{
    private GameManager gm;
    private Dictionary<Transform, GameObject> hidingPlacesLocations;
    private Dictionary<Vector3, GameObject> opossumLocations;
    public void Initialize(GameManager gm,GameObject treesLocations, GameObject[] treesPrefabs, GameObject objectsLocations, GameObject[] ObjectPrefabs)
    {
        this.gm = gm;
        hidingPlacesLocations = new Dictionary<Transform, GameObject>();
        opossumLocations = new Dictionary<Vector3, GameObject>();
        SetUpLocations(treesLocations, treesPrefabs);
        SetUpLocations(objectsLocations, ObjectPrefabs);
        SetUpOpossums(7);
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

    private void SetUpOpossums(int n)
    {
        for (int i = 0; i < n; i++)
        {
            int rnd = Random.Range(0, hidingPlacesLocations.Count);
            while (opossumLocations.ContainsKey(hidingPlacesLocations.ElementAt(rnd).Key.position))
            {
                rnd = Random.Range(0, hidingPlacesLocations.Count);
                Debug.Log(rnd);
            }

            Debug.Log($"{rnd} at position : {hidingPlacesLocations.ElementAt(rnd).Key.position}");
            opossumLocations.Add(hidingPlacesLocations.ElementAt(rnd).Key.position, hidingPlacesLocations.ElementAt(rnd).Value);
        }
    }

    // public void Process()
    // {

    // }
    public void IsItOnTheList(GameObject place)
    {
        if (opossumLocations.ContainsKey(place.transform.position))
        {
            gm.SpawnOpossum(place.transform.position);
        }
    }
}