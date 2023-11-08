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
    
    void SpawnHeart()
    {
        int rand = Random.Range(0, gridManager.gridPoints.Length);
        GameObject newHeart = Instantiate(heart, gridManager.gridPoints[rand].transform, false);
        IEnumerator coroutine = SpawnAnim(newHeart);
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnAnim(GameObject newHeart)
    {
        newHeart.GetComponent<Animator>().Play("coinSpawn");
        yield return new WaitForSeconds(0.5f);
        newHeart.GetComponent<Animator>().Play("coinSpin");
    }

    IEnumerator TryToSpawn()
    {
        while (canSpawn)
        {
            int rand = Random.Range(0, 11);
            if (rand == 0)
            {
                SpawnHeart();
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
                canSpawn = true;
                StartCoroutine("TryToSpawn");
                doOnce = false;
            }

            else if (!player.GetComponent<PlayerController>().isNotFullHealth && !doOnce)
            {
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
