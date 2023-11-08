using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    private GameObject heart;
    private GridManager gridManager;
    
    private bool canSpawn = false;
    private bool doSpawning;
    private bool doOnce = true;

    private GameObject player;
    
    IEnumerator SpawnHeart()
    {
        int rand = Random.Range(0, gridManager.gridPoints.Length);
        yield return new WaitForSeconds(10f);

        GameObject newHeart = Instantiate(heart, gridManager.gridPoints[rand].transform, false);
        newHeart.GetComponent<Animator>().Play("coinSpin");
    }

    IEnumerator TryToSpawn()
    {
        while (canSpawn)
        {
            int rand = Random.Range(0, 11);
            if (rand == 0)
            {
                Debug.Log("spawned!");
                StartCoroutine("SpawnHeart");
            }
            else
            {
                Debug.Log("Heart didn't spawn! Trying again...");
            }

            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator CheckIfCanSpawn()
    {
        while (doSpawning)
        {
            if (player.GetComponent<PlayerController>().isNotFullHealth && doOnce)
            {
                Debug.Log("heart spawning enabled!");
                canSpawn = true;
                StartCoroutine("TryToSpawn");
                doOnce = false;
            }

            else if (!player.GetComponent<PlayerController>().isNotFullHealth && !doOnce)
            {
                Debug.Log("heart spawning disabled!");
                canSpawn = false;
                StopCoroutine("TryToSpawn");
                doOnce = true;
            }
            yield return new WaitForSeconds(1f);
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
        StartCoroutine("CheckIfCanSpawn");
    }
}
