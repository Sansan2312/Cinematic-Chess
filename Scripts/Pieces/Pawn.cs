using System.Collections.Generic;
using UnityEngine;

//Made by Fran Klasic

public class Pawn : ChessPiecesBase
{
    public override void Init(bool isWhite, Vector2Int position)
    {
        base.Init(isWhite, position);
        this.pieceType = PieceType.Pawn;
    }

    public override List<Vector2Int> GetLegalMoves()
    {
        var currentMemory = ChessManager.Instance.Memory;

        List<Vector2Int> legalMoves = new List<Vector2Int>();
        int direction = isWhite ? 1 : -1;

        // Forward movement (one square)
        Vector2Int oneSquareForward = new Vector2Int(position.x, position.y + direction);
        if (IsWithinBounds(oneSquareForward) && !IsOccupied(currentMemory, oneSquareForward))
        {
            legalMoves.Add(oneSquareForward);
        }
        // Forward movement (two squares from starting position)
        if ((isWhite && position.y == 1) || (!isWhite && position.y == 6))  // First move for pawns
        {
            Vector2Int twoSquaresForward = new Vector2Int(position.x, position.y + 2 * direction);
            if (IsWithinBounds(twoSquaresForward) && !IsOccupied(currentMemory, twoSquaresForward) && !IsOccupied(currentMemory, oneSquareForward))
            {
                legalMoves.Add(twoSquaresForward);
            }
        }
        // Diagonal capture (if an opponent piece is there)
        Vector2Int captureLeft = new Vector2Int(position.x - 1, position.y + direction);
        Vector2Int captureRight = new Vector2Int(position.x + 1, position.y + direction);

        if (IsWithinBounds(captureLeft) && IsCapturable(currentMemory, captureLeft, isWhite))
        {
            legalMoves.Add(captureLeft);
        }
        if (IsWithinBounds(captureRight) && IsCapturable(currentMemory, captureRight, isWhite))
        {
            legalMoves.Add(captureRight);
        }

        return legalMoves;
    }
}
