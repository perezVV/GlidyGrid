using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpikeManager : MonoBehaviour
{

    [Header("SFX")] 
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip warningSound;
    
    private GridManager gridManager;

    private GameObject spike;
    private GameObject warning;
    public bool doSpawning;

    private int rounds;
    private int currentRound;

    IEnumerator WarnSpike(int rand)
    {
        GameObject newWarning = Instantiate(warning, gridManager.gridPoints[rand].transform, false);
        newWarning.GetComponent<Animator>().Play("blinkWarning");
        yield return new WaitForSeconds(1.3f);
        Destroy(newWarning);
    }

    IEnumerator SpawnSpike()
    {
        //Get the GridPoint that will be used
        int rand = Random.Range(0, gridManager.gridPoints.Length);
        while (gridManager.IsGridPointInUse(rand))
        {
            // Debug.Log(gridPoints[rand] + " in use! Finding a new one...");
            rand = Random.Range(0, gridManager.gridPoints.Length);
        }
        gridManager.UseGridPoint(rand);
        
        //Begin the warning
        IEnumerator coroutine = WarnSpike(rand);
        StartCoroutine(coroutine);
        yield return new WaitForSeconds(1.3f);
        
        //Spawn the spike
        GameObject newSpike = Instantiate(spike, gridManager.gridPoints[rand].transform, false);
        newSpike.GetComponent<Animator>().Play("spikeUp");
        CameraShake.Shake(0.5f, 0.5f);
        yield return new WaitForSeconds(1f);
        
        //Despawn the spike
        gridManager.StopUsingGridPoint(rand);
        newSpike.GetComponent<Animator>().Play("spikeDown");
        Destroy(newSpike, 1f);
    }

    IEnumerator SFX()
    {
        yield return new WaitForSeconds(1.3f);
        SFXController.instance.PlaySFX(attack, transform, 0.05f);
    }

    IEnumerator WarningSFX()
    {
        SFXController.instance.PlaySFX(warningSound, transform, 0.02f);
        yield return new WaitForSeconds(0.5f);
        SFXController.instance.PlaySFX(warningSound, transform, 0.02f);
        yield return new WaitForSeconds(0.5f);
        SFXController.instance.PlaySFX(warningSound, transform, 0.02f);
    }

    public void SetupSpikes(GameObject s, GameObject w, GridManager g)
    {
        spike = s;
        warning = w;
        gridManager = g;
    }

    public void SpawnSpikes(int amt, int amtRounds)
    {
        doSpawning = true;
        rounds = amtRounds;
        IEnumerator coroutine = Rounds(amt);
        StartCoroutine(coroutine);
    }

    IEnumerator Rounds(int amt)
    {
        for (int i = 0; i < rounds; i++)
        {
            StartCoroutine("SFX");
            StartCoroutine("WarningSFX");
            for (int j = 0; j < amt; j++)
            {
                StartCoroutine("SpawnSpike");
            }
            yield return new WaitForSeconds(2.3f);
        }

        doSpawning = false;
    }

    public bool AreSpikesActive()
    {
        return doSpawning;
    }
}
