using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiecesBase
{
    public override void Init(bool isWhite, Vector2Int position)
    {
        base.Init(isWhite, position);
        this.pieceType = PieceType.Rook;
    }
    public override List<Vector2Int> GetLegalMoves()
    {
        var currentMemory = ChessManager.Instance.Memory;

        List<Vector2Int> legalMoves = new List<Vector2Int>();

        int count;

        //Forward
        count = 0;
        while (true)
        {
            count++;
            Vector2Int newPosition = new Vector2Int(position.x, position.y + count);
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
        }
        //Backward
        count = 0;
        while (true)
        {
            count++;
            Vector2Int newPosition = new Vector2Int(position.x, position.y - count);
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
        }
        //Left
        count = 0;
        while (true)
        {
            count++;
            Vector2Int newPosition = new Vector2Int(position.x - count, position.y);
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
        }
        //Right
        count = 0;
        while (true)
        {
            count++;
            Vector2Int newPosition = new Vector2Int(position.x + count, position.y);
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
        }

        return legalMoves;
    }
}
