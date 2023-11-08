using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private GameObject coin;
    private GridManager gridManager;

    private bool doSpawning;
    
    IEnumerator SpawnCoin()
    {
        while (doSpawning)
        {
            int rand = Random.Range(0, gridManager.gridPoints.Length);
            int randTime = Random.Range(1, 11);

            GameObject newCoin = Instantiate(coin, gridManager.gridPoints[rand].transform, false);
            IEnumerator coroutine = SpawnAnim(newCoin);
            StartCoroutine(coroutine);
            yield return new WaitForSeconds(randTime);
        }
    }

    IEnumerator SpawnAnim(GameObject newCoin)
    {
        newCoin.GetComponent<Animator>().Play("coinSpawn");
        yield return new WaitForSeconds(0.5f);
        if (newCoin != null)
        {
            newCoin.GetComponent<Animator>().Play("coinSpin");
        }
    }
    
    public void SetupCoins(GameObject c, GridManager g)
    {
        coin = c;
        gridManager = g;
    }

    public void SpawnCoins()
    {
        doSpawning = true;
        StartCoroutine("SpawnCoin");
    }
}
