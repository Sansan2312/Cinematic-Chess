using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made by Fran Klasic


public class ChessManager : MonoBehaviour
{
    //Access current memory like this: 
    //var currentMemory = ChessManager.Instance.Memory;
    public static ChessManager Instance { get; private set; }

    [SerializeField] private PiecesGenerator piecesGenerator;
    [SerializeField] private BoardGenerator boardGenerator;
    [SerializeField] private PieceSelection pieceSelection;
    [SerializeField] private ChessPiecesBase chessPiecesBase;
    [SerializeField] private LegalMovesHighlighter legalMovesHighlighter;
    [SerializeField] private UIElements uIElements;

    public GameObject HighlightRed;
    private GameObject CurrentHighlightRed;

    private ChessPiecesBase selectedPiece;

    //Not in use in version 1.2
    public ChessPiecesBase whiteKing;
    public ChessPiecesBase blackKing;


    public PiecesMemory Memory { get; private set; }

    private bool isWhiteTurn = true;

    private void Awake()
    {
        Instance = this;
        Memory = new PiecesMemory();
    }


    //Not in use in version 1.2
    public bool IsInCheck(bool isWhite)
    {
        var currentMemory = ChessManager.Instance.Memory;
        ChessPiecesBase king = isWhite ? whiteKing : blackKing;
        Vector2Int kingPos = king.position;
        foreach (var piece in currentMemory.GetAllPieces())
        {
            if (piece != null && piece.isWhite != isWhite)
            {
                List<Vector2Int> moves = piece.GetLegalMoves();
                if (moves.Contains(kingPos))
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsKingAlive(bool isWhiteTurn, PiecesMemory currentMemory)
    {


        if (currentMemory == null)
        {
            Debug.LogError("Memory is not initialized!");
            return false; // Or handle it appropriately
        }

        var pieces = currentMemory.GetAllPieces();

        if (pieces == null || pieces.Count == 0)
        {
            Debug.LogError("No pieces found in memory.");
            return false;
        }

        foreach (var piece in pieces)
        {
            if (piece != null && piece.pieceType == PieceType.King && piece.isWhite == isWhiteTurn)
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator WaitAndLoadNewScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GenerateAnimationUI");

    }



    private void Start()
    {
        Debug.Log("Start method is running");
        boardGenerator.GenerateBoard();
        piecesGenerator.PlacePieces(Memory);
        CurrentHighlightRed = Instantiate(HighlightRed, new Vector3(0f, 0f, 0.5f), Quaternion.identity);
        CurrentHighlightRed.SetActive(false);
    }

    public bool hasGameEnded = false;
    public bool hasSavedToJSON = false;
    private void Update()
    {
        bool hasMoved = false;

        if (Input.GetMouseButtonDown(0))
        {
            legalMovesHighlighter.ClearHighlights();
            if (!hasGameEnded)
            {
                if (!isWhiteTurn && uIElements.HasButtonBeenPresed(isWhiteTurn))
                {
                    Debug.Log("Black Resigns!!!");
                    hasGameEnded = true;
                }
                if (isWhiteTurn && uIElements.HasButtonBeenPresed(isWhiteTurn))
                {
                    Debug.Log("White Resigns!!!");
                    hasGameEnded = true;
                }
            }
            if (hasGameEnded && uIElements.HasGenerateAnimationPressed())
            {
                StartCoroutine(WaitAndLoadNewScene());
            }
        }
        if (hasGameEnded && !hasSavedToJSON)
        {
            hasSavedToJSON = true;
            MoveLogger.SaveToJson();
        }


        if ((IsKingAlive(true, ChessManager.Instance.Memory) || IsKingAlive(false, ChessManager.Instance.Memory)) && !hasGameEnded)
        {
            hasMoved = pieceSelection.IsPieceSelected(Input.GetMouseButtonDown(0), CurrentHighlightRed, legalMovesHighlighter, isWhiteTurn);
        }
        if (hasMoved)
        {
            isWhiteTurn = !isWhiteTurn;
        }
        if (!hasGameEnded)
        {
            //if white king (true) is dead, black wins
            if (!IsKingAlive(true, ChessManager.Instance.Memory))
            {
                hasGameEnded = true;
                uIElements.EndGame(!isWhiteTurn);
            }
            //if black king (false) is dead, white wins
            if (!IsKingAlive(false, ChessManager.Instance.Memory))
            {
                hasGameEnded = true;
                uIElements.EndGame(!isWhiteTurn);
            }
        }
    }

}
