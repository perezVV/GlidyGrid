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

    [Header("Scripts")] 
    [SerializeField] private SpikeManager spikeManager;
    [SerializeField] private LogManager logManager;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private HeartManager heartManager;
    [SerializeField] private GridManager gridManager;
    
    // Start is called before the first frame update
    void Start()
    {
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
        logManager.SpawnLogs(100, 5f);
        // spikeManager.SpawnSpikes(10, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
