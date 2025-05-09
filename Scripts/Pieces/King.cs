using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiecesBase
{
    public override void Init(bool isWhite, Vector2Int position)
    {
        base.Init(isWhite, position);
        this.pieceType = PieceType.King;
    }
    public override List<Vector2Int> GetLegalMoves()
    {
        var currentMemory = ChessManager.Instance.Memory;
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        // All 8 possible directions the king can move
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),   // Right
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(0, 1),   // Up
            new Vector2Int(0, -1),  // Down
            new Vector2Int(1, 1),   // Up-Right
            new Vector2Int(-1, 1),  // Up-Left
            new Vector2Int(1, -1),  // Down-Right
            new Vector2Int(-1, -1)  // Down-Left
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int newPosition = position + dir;

            if (IsWithinBounds(newPosition))
            {
                if (!IsOccupied(currentMemory, newPosition) || IsCapturable(currentMemory, newPosition, isWhite))
                {
                    legalMoves.Add(newPosition);
                }
            }
        }

        return legalMoves;
    }
}
