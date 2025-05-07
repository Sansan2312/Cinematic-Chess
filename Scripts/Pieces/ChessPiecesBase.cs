using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class ChessPiecesBase : UniversalBase
{
    public bool isWhite { get; set; }
    public Vector2Int position { get; set; }
    public PieceType pieceType { get; set; }

    public bool hasMoved { get; set; } = false;

    public virtual void Init(bool isWhite, Vector2Int position)
    {
        this.isWhite = isWhite;
        this.position = position;
        MoveToPosition(position);
    }

    public abstract List<Vector2Int> GetLegalMoves();
    public virtual void SetPosition(Vector2Int newPosition)
    {
        this.position = newPosition;
    }
    const float queenKingOffset = 0.22f;
    public virtual void MoveToPosition(Vector2Int newPosition)
    {
        SetPosition(newPosition);

        // Apply offset for King and Queen
        Vector3 newWorldPosition = GetTilePosition(newPosition.x, newPosition.y, -0.5f);
        if (pieceType == PieceType.King || pieceType == PieceType.Queen)
        {
            newWorldPosition.y += queenKingOffset;  // Apply the offset
            newWorldPosition.z = (pieceType == PieceType.King)? -0.7f : -0.6f;
        }

        transform.position = newWorldPosition;

        hasMoved = true;
    }
}
