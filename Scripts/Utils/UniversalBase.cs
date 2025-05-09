using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

//Made by Fran Klasic
//This contains helper functions and other stuff we might need across all scripts

public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }


public class UniversalBase : MonoBehaviour
{
    public static UniversalBase Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    const float tileSize = 1f;
    public Vector3 GetTilePosition(int x, int y, float z = 0)
    {
        float xPos = x * tileSize - (tileSize * 7) / 2f;
        float yPos = y * tileSize - (tileSize * 7) / 2f;
        return new Vector3(xPos, yPos, z);
    }
    public bool IsOccupied(PiecesMemory memory, Vector2Int position)
    {
        return memory.memoryList.Exists(p => p.address == position);
    }

    public bool IsCapturable(PiecesMemory memory, Vector2Int position, bool isWhite)
    {
        return memory.memoryList.Exists(p => p.address == position && p.isWhite != isWhite);
    }
    public bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
    }
}