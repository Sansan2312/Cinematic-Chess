using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiecesBase
{
    public override void Init(bool isWhite, Vector2Int position)
    {
        base.Init(isWhite, position);
        this.pieceType = PieceType.Bishop;
    }
    public override List<Vector2Int> GetLegalMoves()
    {
        var currentMemory = ChessManager.Instance.Memory;
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        // Directions for diagonal movement (top-right, top-left, bottom-right, bottom-left)
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1),  // Top-right diagonal
            new Vector2Int(-1, 1), // Top-left diagonal
            new Vector2Int(1, -1), // Bottom-right diagonal
            new Vector2Int(-1, -1) // Bottom-left diagonal
        };

        foreach (var direction in directions)
        {
            int count = 1;

            while (true)
            {
                Vector2Int newPosition = position + direction * count;

                if (IsWithinBounds(newPosition))
                {
                    if (!IsOccupied(currentMemory, newPosition))
                    {
                        legalMoves.Add(newPosition);
                    }
                    else if (IsCapturable(currentMemory, newPosition, isWhite))
                    {
                        legalMoves.Add(newPosition);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

                count++; 
            }
        }

        return legalMoves;
    }
}
