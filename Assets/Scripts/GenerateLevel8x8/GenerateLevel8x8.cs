using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel8x8 : MonoBehaviour
{
    [SerializeField] private GameObject playerPositionSetter;
    [SerializeField] private GameObject vanDropOff;


    [SerializeField] private List<GameObject> StartingTiles;
    [SerializeField] private List<GameObject> AdvancingTiles;
    [SerializeField] private List<GameObject> AdvancedFromTiles;
    [SerializeField] private List<GameObject> TraversingTiles;
    [SerializeField] private List<GameObject> FillerTiles;
    [SerializeField] private List<GameObject> EndingTiles;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int tileSize;

    GameObject[,] generatedMap;
    // Start is called before the first frame update
    void Awake()
    {
        generatedMap = new GameObject[width, height];
        SetPath();
        FillRemainingTiles();
        InstantiateAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetPath()
    {
        //Set Starting Tile
        int startingColumn = Random.Range(0, width);
        generatedMap[startingColumn,0] = GrabRandomTile(StartingTiles);
        playerPositionSetter.transform.position = new Vector3(startingColumn * tileSize - 16, 0);
        vanDropOff.transform.position = new Vector3(startingColumn * tileSize -16, 0);
        int currColumn = startingColumn;
        int currRow = 0;
        SetRow(currColumn, currRow, PickDirection(currColumn));
    }

    void SetRow(int currColumn, int currRow, int direction)
    {
        while (currColumn + direction >= 0 && currColumn + direction < width)
        {
            currColumn = currColumn + direction;
            generatedMap[currColumn, currRow] = GrabRandomTile(TraversingTiles);
            //chance to move ahead without reaching the end
            if (ProbabilityTrue(15f))
            {
                break;
            }
        }
        generatedMap[currColumn, currRow] = GrabRandomTile(AdvancingTiles);
        if (currRow + 1 < height)
        {
            currRow++;
            generatedMap[currColumn, currRow] = GrabRandomTile(AdvancedFromTiles);
            SetRow(currColumn, currRow, PickDirection(currColumn));
        }
        else
        {
            generatedMap[currColumn, currRow] = GrabRandomTile(EndingTiles);
        }
    }

    bool ProbabilityTrue(float percentageTrue)
    {
        if(Random.Range(0, 100) > percentageTrue)
        {
            return false;
        }
        return true;
    }

    int PickDirection(int currColumn)
    {
        if(currColumn == width-1)
        {
            return -1;
        }
        if(currColumn == 0)
        {
            return 1;
        }
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    void FillRemainingTiles()
    {
        for(int i = 0; i< width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(generatedMap[i,j] == null)
                {
                    generatedMap[i, j] = GrabRandomTile(FillerTiles);
                }
            }
        }
    }

    void InstantiateAllTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject.Instantiate(generatedMap[i, j], new Vector3(i * tileSize, j * tileSize), Quaternion.identity);
            }
        }
    }

    GameObject GrabRandomTile(List<GameObject> selectionList)
    {
        return selectionList[Random.Range(0, selectionList.Count)];
    }
}
