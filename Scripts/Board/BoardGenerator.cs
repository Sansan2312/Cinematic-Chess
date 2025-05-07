using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;


//INFO
//Made by Fran Klasic
//This makes the board and the letters






public class BoardGenerator : UniversalBase
{
    public GameObject TileWhite;
    public GameObject TileBlack;
    public List<GameObject> LetterList = new List<GameObject>();
    public List<GameObject> NumberList = new List<GameObject>();

    public void GenerateBoard()
    {
        foreach (GameObject obj in LetterList) { obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        foreach (GameObject obj in NumberList) { obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); }
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject tileToSpawn = (x + y) % 2 == 0 ? TileBlack : TileWhite;
                var tilePosition = GetTilePosition(x, y, 1);
                //Building the board: 
                GameObject tile = Instantiate(tileToSpawn, tilePosition, Quaternion.identity);
                tile.transform.SetParent(this.transform);
                tile.name = $"Tile_{x}_{y}";


            }
            Instantiate(LetterList[x], GetTilePosition(x,-1,-1), Quaternion.identity);
            Instantiate(NumberList[x], GetTilePosition(-1,x,-1), Quaternion.identity);

        }
    }

}
