using System.Collections.Generic;
using UnityEngine;

//Made by Fran Klasic

public class PiecesGenerator : UniversalBase
{
    public GameObject WhitePawn;
    public GameObject WhiteRook;
    public GameObject WhiteKnight;
    public GameObject WhiteBishop;
    public GameObject WhiteQueen;
    public GameObject WhiteKing;

    public GameObject BlackPawn;
    public GameObject BlackRook;
    public GameObject BlackKnight;
    public GameObject BlackBishop;
    public GameObject BlackQueen;
    public GameObject BlackKing;

    public void PlacePieces(PiecesMemory memory)
    {
        Debug.Log("Placing pieces..."); // Check if this is called
        const float queenKingOffset = 0.22f;

        for (int x = 0; x < 8; x++)
        {
            // Black Pawn
            Vector2Int blackBoardPos = new Vector2Int(x, 6);
            Vector3 blackWorldPos = GetTilePosition(x, 6, -0.5f);
            GameObject blackPieceObj = Instantiate(BlackPawn, blackWorldPos, Quaternion.identity);
            ChessPiecesBase blackPieceScript = blackPieceObj.GetComponent<ChessPiecesBase>();
            blackPieceScript.Init(false, blackBoardPos); // false = black
            memory.AddToMemory(blackBoardPos, PieceType.Pawn, false, blackPieceScript);


            // White Pawn
            Vector2Int whiteBoardPos = new Vector2Int(x, 1);
            Vector3 whiteWorldPos = GetTilePosition(x, 1, -0.5f);
            GameObject whitePieceObj = Instantiate(WhitePawn, whiteWorldPos, Quaternion.identity);
            ChessPiecesBase whitePieceScript = whitePieceObj.GetComponent<ChessPiecesBase>();
            whitePieceScript.Init(true, whiteBoardPos); // true = white
            memory.AddToMemory(whiteBoardPos, PieceType.Pawn, true, whitePieceScript);
        }

        // Black Rook
        Vector2Int boardPos = new Vector2Int(0, 7);
        Vector3 worldPos = GetTilePosition(0, 7, -0.5f);
        GameObject pieceObj = Instantiate(BlackRook, worldPos, Quaternion.identity);
        ChessPiecesBase pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Rook, false, pieceScript);

        boardPos = new Vector2Int(7, 7);
        worldPos = GetTilePosition(7, 7, -0.5f);
        pieceObj = Instantiate(BlackRook, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Rook, false, pieceScript);

        // White Rook
        boardPos = new Vector2Int(0, 0);
        worldPos = GetTilePosition(0, 0, -0.5f);
        pieceObj = Instantiate(WhiteRook, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Rook, true, pieceScript);

        boardPos = new Vector2Int(7, 0);
        worldPos = GetTilePosition(7, 0, -0.5f);
        pieceObj = Instantiate(WhiteRook, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Rook, true, pieceScript);

        // Black Knight
        boardPos = new Vector2Int(1, 7);
        worldPos = GetTilePosition(1, 7, -0.5f);
        pieceObj = Instantiate(BlackKnight, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Knight, false, pieceScript);

        boardPos = new Vector2Int(6, 7);
        worldPos = GetTilePosition(6, 7, -0.5f);
        pieceObj = Instantiate(BlackKnight, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Knight, false, pieceScript);

        // White Knight
        boardPos = new Vector2Int(1, 0);
        worldPos = GetTilePosition(1, 0, -0.5f);
        pieceObj = Instantiate(WhiteKnight, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Knight, true, pieceScript);

        boardPos = new Vector2Int(6, 0);
        worldPos = GetTilePosition(6, 0, -0.5f);
        pieceObj = Instantiate(WhiteKnight, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Knight, true, pieceScript);

        // Black Bishop
        boardPos = new Vector2Int(2, 7);
        worldPos = GetTilePosition(2, 7, -0.5f);
        pieceObj = Instantiate(BlackBishop, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Bishop, false, pieceScript);

        boardPos = new Vector2Int(5, 7);
        worldPos = GetTilePosition(5, 7, -0.5f);
        pieceObj = Instantiate(BlackBishop, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Bishop, false, pieceScript);

        // White Bishop
        boardPos = new Vector2Int(2, 0);
        worldPos = GetTilePosition(2, 0, -0.5f);
        pieceObj = Instantiate(WhiteBishop, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Bishop, true, pieceScript);

        boardPos = new Vector2Int(5, 0);
        worldPos = GetTilePosition(5, 0, -0.5f);
        pieceObj = Instantiate(WhiteBishop, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Bishop, true, pieceScript);

        // Black Queen
        boardPos = new Vector2Int(3, 7);
        worldPos = GetTilePosition(3, 7, -0.6f);
        worldPos.y += queenKingOffset;
        pieceObj = Instantiate(BlackQueen, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.Queen, false, pieceScript);
        pieceObj.transform.position = new Vector3(worldPos.x, worldPos.y, -0.6f);

        // White Queen
        boardPos = new Vector2Int(3, 0);
        worldPos = GetTilePosition(3, 0, -0.6f);
        worldPos.y += queenKingOffset;
        pieceObj = Instantiate(WhiteQueen, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.Queen, true, pieceScript);
        pieceObj.transform.position = new Vector3(worldPos.x, worldPos.y, -0.6f);

        // Black King
        boardPos = new Vector2Int(4, 7);
        worldPos = GetTilePosition(4, 7, -0.7f);
        worldPos.y += queenKingOffset;
        pieceObj = Instantiate(BlackKing, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(false, boardPos); // false = black
        memory.AddToMemory(boardPos, PieceType.King, false, pieceScript);
        pieceObj.transform.position = new Vector3(worldPos.x, worldPos.y, -0.7f);
        ChessManager.Instance.blackKing = pieceScript;

        // White King
        boardPos = new Vector2Int(4, 0);
        worldPos = GetTilePosition(4, 0, -0.7f);
        worldPos.y += queenKingOffset;
        pieceObj = Instantiate(WhiteKing, worldPos, Quaternion.identity);
        pieceScript = pieceObj.GetComponent<ChessPiecesBase>();
        pieceScript.Init(true, boardPos); // true = white
        memory.AddToMemory(boardPos, PieceType.King, true, pieceScript);
        pieceObj.transform.position = new Vector3(worldPos.x,worldPos.y,-0.7f);
        ChessManager.Instance.whiteKing = pieceScript;
    }
}
