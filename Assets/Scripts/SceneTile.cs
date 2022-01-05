using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTile : MonoBehaviour
{
    public List<int> sides = new List<int>();

    [SerializeField] private int leftOpen;
    [SerializeField] private int leftClose;
    [SerializeField] private int topOpen;
    [SerializeField] private int topClose;
    [SerializeField] private int rightOpen;
    [SerializeField] private int rightClose;
    [SerializeField] private int bottomOpen;
    [SerializeField] private int bottomClose;


    public int gridX;
    public int gridY;

    public bool MatchesToTheLeft(SceneTile sceneTile)
    {
        return sceneTile.rightOpen == leftOpen && sceneTile.rightClose == leftClose && sides.Contains(0);
    }
    public bool MatchesToTheRight(SceneTile sceneTile)
    {
        return sceneTile.MatchesToTheLeft(this);
    }
    public bool MatchesToTheTop(SceneTile sceneTile)
    {
        return sceneTile.bottomOpen == topOpen && sceneTile.bottomClose == topClose && sides.Contains(1);
    }
    public bool MatchesToTheBottom(SceneTile sceneTile)
    {
        return sceneTile.MatchesToTheTop(this);
    }
    public bool Matches(SceneTile sceneTile, int side)
    {
        switch (side)
        {
            case 0:
                return MatchesToTheLeft(sceneTile);
            case 1:
                return MatchesToTheTop(sceneTile);
            case 2:
                return MatchesToTheRight(sceneTile);
            case 3:
                return MatchesToTheBottom(sceneTile);
            default:
                return false;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(leftOpen != leftClose)
        {
            sides.Add(0);
        }
        if(topOpen != topClose)
        {
            sides.Add(1);
        }
        if(rightOpen != rightClose)
        {
            sides.Add(2);
        }
        if(bottomOpen != bottomClose)
        {
            sides.Add(3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
