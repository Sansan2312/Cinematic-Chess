using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//Made by Fran Klasic

public class PieceSelection : UniversalBase
{

    private ChessPiecesBase selectedPiece = null;
    private Vector2Int selectedPosition;
    private Vector2Int GetTilePositionFromWorld(Vector2 worldPosition)
    {
        // Get the X and Y of the mouse world position and round them to the nearest tile
        int x = Mathf.RoundToInt((worldPosition.x + 3.5f) / 1f); // Adjusting based on tile size and offset
        int y = Mathf.RoundToInt((worldPosition.y + 3.5f) / 1f); // Adjusting based on tile size and offset

        return new Vector2Int(x, y);
    }

    private PieceType WhatPieceTypeOnHit(PiecesMemory memory, Vector2Int tilePosition)
    {
        if (IsOccupied(memory, tilePosition))
        {
            var piece = memory.memoryList.FirstOrDefault(p => p.address == tilePosition);
            if (piece != null)
            {
                return piece.pieceType;
            }
        }
        throw new System.Exception("No piece found at the given position.");
    }
    private bool IsWhitePieceOnHit(PiecesMemory memory, Vector2Int tilePosition)
    {
        if (IsOccupied(memory, tilePosition))
        {
            var piece = memory.memoryList.FirstOrDefault(p => p.address == tilePosition);
            if (piece != null)
            {
                return piece.isWhite;
            }
        }
        throw new System.Exception("No piece found at the given position.");
    }

    public bool IsPieceSelected(bool mouseButton0, GameObject CurrentHighlightRed, LegalMovesHighlighter legalMovesHighlighter, bool isWhiteTurn)
    {
        bool hasAnythingMoved = false;
        List<Vector2Int> legalMoves = new List<Vector2Int>();

        if (mouseButton0) //If a button has been pressed
        {
            CurrentHighlightRed.SetActive(false);

            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);


            Vector2Int tilePosition = GetTilePositionFromWorld(mouseWorldPosition);

            var memory = ChessManager.Instance.Memory;

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition);


            if (hit != null)
            {
                ChessPiecesBase piece = hit.GetComponentInParent<ChessPiecesBase>() ?? hit.GetComponentInChildren<ChessPiecesBase>();
                Debug.Log($"Hit object: {hit.gameObject.name}");
                Debug.Log($"Components on hit: {string.Join(", ", hit.GetComponents<Component>().Select(c => c.GetType().Name))}");
                if (piece != null && piece.isWhite == isWhiteTurn)
                {
                    //Piece has been selected
                    selectedPiece = piece;
                    selectedPosition = piece.position;
                    legalMoves = piece.GetLegalMoves();
                    legalMovesHighlighter.ShowHighlights(legalMoves);
                }
                else
                {
                    Debug.Log("No ChessPiecesBase component found on the hit object.");
                    if (selectedPiece != null && selectedPiece.GetLegalMoves().Contains(tilePosition))
                    {
                        // If it's an empty tile or contains an enemy piece
                        if (piece == null || piece.isWhite != isWhiteTurn)
                        {
                            // Move the piece if the target is legal
                            MoveSelectedPiece(selectedPosition, tilePosition);
                            //Saves move data to JSON
                            MoveLogger.LogMove(selectedPiece.pieceType.ToString(), selectedPosition.ToString(), tilePosition.ToString(), selectedPiece.isWhite);
                            hasAnythingMoved = true;

                            // Reset the selection
                            selectedPiece = null;
                            legalMovesHighlighter.ClearHighlights();
                            legalMoves.Clear();
                        }
                    }
                    else
                    {
                        selectedPiece = null;
                    }
                }

                if (IsOccupied(memory, tilePosition) && piece != null && piece.isWhite == isWhiteTurn)
                {
                    CurrentHighlightRed.transform.position = GetTilePosition(tilePosition.x, tilePosition.y, 0.5f);
                    CurrentHighlightRed.SetActive(true);
                    PieceType type = WhatPieceTypeOnHit(memory, tilePosition);
                    bool isWhite = IsWhitePieceOnHit(memory, tilePosition);

                }
            }
        }
        return hasAnythingMoved;
    }

    const float queenKingOffset = 0.22f;

    public void MoveSelectedPiece(Vector2Int from, Vector2Int to)
    {
        var memory = ChessManager.Instance.Memory;

        //1. Check if there's an enemy piece at the destination and remove it
        var targetEntry = memory.GetMemoryEntryAt(to);

        if (targetEntry != null && targetEntry.pieceScript != null)
        {
            GameObject.Destroy(targetEntry.pieceScript.gameObject); // Remove from scene
            memory.RemovePieceAt(to); // Remove from memory list (make sure this method exists)
        }

        //2. Update memory with the move
        memory.MovePiece(from, to);

        //3. Move the GameObject
        var entry = memory.GetMemoryEntryAt(to);
        if (entry != null && entry.pieceScript != null)
        {
            entry.pieceScript.SetPosition(to); // Update internal position field
            if (entry.pieceType == PieceType.King || entry.pieceType == PieceType.Queen)
            {
                entry.pieceScript.transform.position = (entry.pieceType == PieceType.King) ?
                        GetTilePosition(to.x, to.y, -0.7f) + new Vector3(0, queenKingOffset, 0) :
                        GetTilePosition(to.x, to.y, -0.6f) + new Vector3(0, queenKingOffset, 0);

                //entry.pieceScript.transform.position = GetTilePosition(to.x, to.y, -0.6f) + new Vector3(0, queenKingOffset, 0);
            }
            else
            {
                entry.pieceScript.transform.position = GetTilePosition(to.x, to.y, -0.5f);
            }
        }
    }

}