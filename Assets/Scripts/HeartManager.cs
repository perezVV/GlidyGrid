using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    private GameObject heart;
    private GridManager gridManager;

    private bool doSpawning;

    private GameObject player;
    
    IEnumerator SpawnHeart()
    {
        while (doSpawning)
        {
            int rand = Random.Range(0, gridManager.gridPoints.Length);
            yield return new WaitForSeconds(10f);

            GameObject newCoin = Instantiate(heart, gridManager.gridPoints[rand].transform, false);
            newCoin.GetComponent<Animator>().Play("coinSpin");
        }
    }
    
    public void SetupHearts(GameObject h, GridManager g)
    {
        heart = h;
        gridManager = g;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnHearts()
    {
        doSpawning = true;
        StartCoroutine("SpawnHeart");
    }

    void Update()
    {
        
    }
}
