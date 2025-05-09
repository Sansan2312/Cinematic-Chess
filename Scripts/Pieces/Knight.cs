using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiecesBase
{
    public override void Init(bool isWhite, Vector2Int position)
    {
        base.Init(isWhite, position);
        this.pieceType = PieceType.Knight;
    }

    public override List<Vector2Int> GetLegalMoves()
    {
        var currentMemory = ChessManager.Instance.Memory;
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        // Knight's possible moves (L-shape)
        Vector2Int[] possibleMoves = new Vector2Int[]
        {
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, 2), new Vector2Int(1, -2),
            new Vector2Int(-1, 2), new Vector2Int(-1, -2)
        };

        foreach (var move in possibleMoves)
        {
            Vector2Int newPosition = position + move;

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
