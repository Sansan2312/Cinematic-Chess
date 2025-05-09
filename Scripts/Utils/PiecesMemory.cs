using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;


//Made by Fran Klasic
//This stores the memory of the pieces color and location and type

public class AddressAndType
{
    public Vector2Int address; //Position on the board
    public PieceType pieceType; //Type of piece
    public bool isWhite; //Color of piece
    public ChessPiecesBase pieceScript; //Piece script

    public AddressAndType(Vector2Int address, PieceType pieceType, bool isWhite, ChessPiecesBase pieceScript)
    {
        this.address = address;
        this.pieceType = pieceType;
        this.isWhite = isWhite;
        this.pieceScript = pieceScript;
    }
}
public class PiecesMemory
{
    public List<AddressAndType> memoryList = new List<AddressAndType>();
    const float queenKingOffset = 0.22f;


    public void AddToMemory(Vector2Int address, PieceType pieceType, bool isWhite, ChessPiecesBase pieceScript)
    {
        memoryList.Add(new AddressAndType(address, pieceType, isWhite, pieceScript));
    }
    public ChessPiecesBase GetPieceAt(Vector2Int pos)
    {
        return memoryList.Find(p => p.address == pos)?.pieceScript;
    }
    public List<ChessPiecesBase> GetAllPieces()
    {
        List<ChessPiecesBase> allPieces = new List<ChessPiecesBase>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                var piece = GetPieceAt(pos);
                if (piece == null) { continue; }
                allPieces.Add(piece);
            }
        }
        return allPieces;
    }
    public AddressAndType GetMemoryEntryAt(Vector2Int pos)
    {
        return memoryList.Find(p => p.address == pos);
    }
    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        var entry = GetMemoryEntryAt(from);
        if (entry != null)
        {
            entry.address = to;

            entry.pieceScript.SetPosition(to);
            //entry.pieceScript.transform.position = UniversalBase.Instance.GetTilePosition(to.x, to.y, -0.5f);
            Vector3 newPosition;
            if (entry.pieceScript.pieceType == PieceType.King || entry.pieceScript.pieceType == PieceType.Queen)
            {
                newPosition = UniversalBase.Instance.GetTilePosition(to.x, to.y, -0.6f);
                newPosition = (entry.pieceScript.pieceType == PieceType.King) ?
                    UniversalBase.Instance.GetTilePosition(to.x, to.y, -0.7f) :
                    UniversalBase.Instance.GetTilePosition(to.x, to.y, -0.6f);

                newPosition.y += queenKingOffset;
                entry.pieceScript.transform.position = newPosition;
            }
            else
            {
                newPosition = UniversalBase.Instance.GetTilePosition(to.x, to.y, -0.5f);
            }
            entry.pieceScript.SetPosition(to);
            entry.pieceScript.transform.position = newPosition;

        }
    }
    public void RemovePieceAt(Vector2Int pos)
    {
        memoryList.RemoveAll(p => p.address == pos);
    }
}
