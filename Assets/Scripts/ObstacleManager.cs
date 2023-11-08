using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject log;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject heart;

    [Header("GameObjects")] 
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject player;

    [Header("Scripts")] 
    [SerializeField] private SpikeManager spikeManager;
    [SerializeField] private LogManager logManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private HeartManager heartManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlayerController playerController;

    private bool doSpawning;

    enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        grid = GameObject.FindGameObjectWithTag("GRID");
        gridManager = grid.GetComponent<GridManager>();
        gridManager.SetupGrid();
        spikeManager = GetComponent<SpikeManager>();
        spikeManager.SetupSpikes(spike, warning, gridManager);
        logManager = GetComponent<LogManager>();
        logManager.SetupLogs(log, warning, gridManager);
        coinManager = GetComponent<CoinManager>();
        coinManager.SetupCoins(coin, gridManager);
        heartManager = GetComponent<HeartManager>();
        heartManager.SetupHearts(heart, gridManager);
        heartManager.SpawnHearts();
        coinManager.SpawnCoins();
        doSpawning = true;
        StartCoroutine("SpawnObstacles");
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(2.5f);
        Difficulty diff;
        while (doSpawning)
        {
            diff = GetDifficulty();
            
            if (diff == Difficulty.EASY)
            {
                int whichObstacle = Random.Range(0, 2);
                if (whichObstacle == 0)
                {
                    int numLogs = Random.Range(1, 3);
                    float numTime = Random.Range(2, 4);
                    logManager.SpawnLogs(numLogs, numTime);
                    yield return new WaitWhile(() => logManager.doSpawning);
                }
                else if (whichObstacle == 1)
                {
                    int numSpikes = Random.Range(10, 15);
                    int numRounds = Random.Range(1, 4);
                    spikeManager.SpawnSpikes(numSpikes, numRounds);
                    yield return new WaitWhile(() => spikeManager.doSpawning);
                }
                else
                {
                    Debug.Log("neither, returning");
                    yield return new WaitForSeconds(0f);
                }
            }
            
            else if (diff == Difficulty.MEDIUM)
            {
                int whichObstacle = Random.Range(0, 2);
                if (whichObstacle == 0)
                {
                    int numLogs = Random.Range(3, 6);
                    float numTime = Random.Range(1, 3);
                    logManager.SpawnLogs(numLogs, numTime);
                    yield return new WaitWhile(() => logManager.doSpawning);
                }
                else if (whichObstacle == 1)
                {
                    int numSpikes = Random.Range(15, 20);
                    int numRounds = Random.Range(1, 3);
                    spikeManager.SpawnSpikes(numSpikes, numRounds);
                    yield return new WaitWhile(() => spikeManager.doSpawning);
                }
                else
                {
                    Debug.Log("neither, returning");
                    yield return new WaitForSeconds(0f);
                }
            }
            
            else if (diff == Difficulty.HARD)
            {
                int whichObstacle = Random.Range(0, 2);
                if (whichObstacle == 0)
                {
                    int numLogs = Random.Range(4, 9);
                    float numTime = 0.5f;
                    logManager.SpawnLogs(numLogs, numTime);
                    yield return new WaitWhile(() => logManager.doSpawning);
                }
                else if (whichObstacle == 1)
                {
                    int numSpikes = Random.Range(20, 30);
                    int numRounds = Random.Range(1, 5);
                    spikeManager.SpawnSpikes(numSpikes, numRounds);
                    yield return new WaitWhile(() => spikeManager.doSpawning);
                }
                else
                {
                    Debug.Log("neither, returning");
                    yield return new WaitForSeconds(0f);
                }
            }
        }
    }

    Difficulty GetDifficulty()
    {
        Difficulty diff;
        int coinAmt = playerController.coinAmt;

        if (coinAmt < 10 && coinAmt >= 0)
        {
            diff = Difficulty.EASY;
        }
        else if (coinAmt < 25 && coinAmt >= 10)
        {
            diff = Difficulty.MEDIUM;
        }
        else if (coinAmt >= 25)
        {
            diff = Difficulty.HARD;
        }
        else
        {
            diff = Difficulty.EASY;
        }


        return diff;
    }
}
