using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] public GameObject[] gridPoints;
    
    [Header("Rows")]
    [SerializeField] private GameObject[] gridRowOne;
    [SerializeField] private GameObject[] gridRowTwo;
    [SerializeField] private GameObject[] gridRowThree;
    [SerializeField] private GameObject[] gridRowFour;
    [SerializeField] private GameObject[] gridRowFive;
    [SerializeField] private GameObject[] gridRowSix;
    [SerializeField] private GameObject[] gridRowSeven;
    [SerializeField] private GameObject[] gridRowEight;

    public GameObject[][] gridRows;
    
    // [Header("Columns")]
    // [SerializeField] private GameObject[] gridColumnOne;
    // [SerializeField] private GameObject[] gridColumnTwo;
    // [SerializeField] private GameObject[] gridColumnThree;
    // [SerializeField] private GameObject[] gridColumnFour;
    // [SerializeField] private GameObject[] gridColumnFive;
    // [SerializeField] private GameObject[] gridColumnSix;
    // [SerializeField] private GameObject[] gridColumnSeven;
    // [SerializeField] private GameObject[] gridColumnEight;
    // [SerializeField] private GameObject[] gridColumnNine;
    // [SerializeField] private GameObject[] gridColumnTen;
    // [SerializeField] private GameObject[] gridColumnEleven;
    // [SerializeField] private GameObject[] gridColumnTwelve;
    
    private List<bool> isInUse;

    private bool printOnce = true;
    
    public void SetupGrid()
    {
        // gridPoints = GameObject.FindGameObjectsWithTag("GRIDPOINT");
        isInUse = new List<bool>();
        for (int i = 0; i < gridPoints.Length - 1; i++)
        {
            isInUse.Add(false);
        }

        gridRows = new GameObject[][]
        {
            gridRowOne,
            gridRowTwo,
            gridRowThree,
            gridRowFour,
            gridRowFive,
            gridRowSix,
            gridRowSeven,
            gridRowEight
        };
    }

    public GameObject[] GetGridPoints()
    {
        return gridPoints;
    }

    public void UseGridPoint(int whichPoint)
    {
        if (whichPoint > isInUse.Count - 1)
        {
            // Debug.Log("index too large");
            return;
        }
        isInUse[whichPoint] = true;
    }

    public void StopUsingGridPoint(int whichPoint)
    {
        if (whichPoint > isInUse.Count - 1)
        {
            // Debug.Log("index too large");
            return;
        }
        isInUse[whichPoint] = false;
    }

    public void GridPointsInUse()
    {
        if (!printOnce)
        {
            return;
        }

        printOnce = false;
        for (int i = 0; i < gridPoints.Length - 1; i++)
        {
            if (isInUse[i] == true)
            {
                // Debug.Log(gridPoints[i] + " is in use!");
            }
        }
    }

    public int FindGridIndex(GameObject gridPoint)
    {
        for (int i = 0; i < gridPoints.Length - 1; i++)
        {
            if (gridPoints[i] == gridPoint)
            {
                return i;
            }
        }
        return 0;
    }

    public bool IsGridPointInUse(int whichPoint)
    {
        if (whichPoint > isInUse.Count - 1)
        {
            // Debug.Log("index too large");
            return false;
        }
        if (isInUse[whichPoint])
        {
            return true;
        }
        return false;
    }
}
