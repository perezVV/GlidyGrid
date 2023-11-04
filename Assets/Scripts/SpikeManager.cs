using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{

    [SerializeField] private GameObject[] gridPoints;

    [SerializeField] private GameObject spike;
    private bool doSpawning;
    
    // Start is called before the first frame update
    void Start()
    {
        gridPoints = GameObject.FindGameObjectsWithTag("GRIDPOINT");
        doSpawning = true;
        StartCoroutine("SpawnSpike");
    }

    IEnumerator SpawnSpike()
    {
        while (doSpawning)
        {
            int rand = Random.Range(0, gridPoints.Length - 1);
            Debug.Log("spawning at " + gridPoints[rand]);
            yield return new WaitForSeconds(6.0f);
            GameObject newSpike = Instantiate(spike, gridPoints[rand].transform, false);
            Destroy(newSpike, 3f);
        }
        
    }
    
}
