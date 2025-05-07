using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONDecoder : MonoBehaviour
{
    public static MoveHistoryFor3DScene moveHistory = new MoveHistoryFor3DScene();

    public static void LogMove(string pieceType, string from, string to, bool isWhite)
    {
        MoveFor3DScene move = new MoveFor3DScene
        {
            pieceType = pieceType,
            from = from,
            to = to,
            isWhite = isWhite
        };

        moveHistory.moves.Add(move);
    }


    public static void Decoder(List<string> loggedMoves)
    {
        string fileName = "chess_moves.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);

            moveHistory = JsonUtility.FromJson<MoveHistoryFor3DScene>(jsonContent);

            Debug.Log("Loaded " + moveHistory.moves.Count + " moves.");

            foreach (MoveFor3DScene move in moveHistory.moves)
            {
                string logEntry = $"Piece Type: {move.pieceType}, From: {move.from}, To: {move.to}, Is White: {move.isWhite}";
                loggedMoves.Add(logEntry);
                Debug.Log(logEntry);

            }
        }
        else
        {
            Debug.LogError("File not found at: " + path);
        }
    }
}

[System.Serializable]
public class MoveFor3DScene
{
    public string pieceType;
    public string from;
    public string to;
    public bool isWhite;
}

[System.Serializable]
public class MoveHistoryFor3DScene
{
    public List<MoveFor3DScene> moves = new List<MoveFor3DScene>();
}
