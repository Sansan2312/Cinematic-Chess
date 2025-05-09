using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class ChessMove
{
    public string pieceType;
    public string from;
    public string to;
    public bool isWhite;
}

[System.Serializable]
public class MoveHistory
{
    public List<ChessMove> moves = new List<ChessMove>();
}


public class MoveLogger : MonoBehaviour
{
    public static MoveHistory moveHistory = new MoveHistory();

    public static void LogMove(string pieceType, string from, string to, bool isWhite)
    {
        ChessMove move = new ChessMove
        {
            pieceType = pieceType,
            from = from,
            to = to,
            isWhite = isWhite
        };

        moveHistory.moves.Add(move);
    }
    public static void ResetJson()
    {
        moveHistory.moves.Clear();
    }
    public static void SaveToJson(string filename = "chess_moves.json")
    {
        string json = JsonUtility.ToJson(moveHistory, true);
        string path = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllText(path, json);
        Debug.Log("Saved move history to: " + path);
    }
}
