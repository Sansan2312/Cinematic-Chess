using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

//Made by Fran Klasic

public class SceneFlowController : MonoBehaviour
{
    public Animator animator;
    private bool hasPlayedMain = false;
    public Camera mainCamera;
    private Vector3 startingCameraPosition;
    private Quaternion startingCameraRotation;
    private float duration = 1f;

    private bool didIntroPlay = false;

    //Game pieces
    //i did a boo boo and fliped the colors in the scene (:
    //In the AnimationManager Inspector tab, example for GEO_BlackPawn_01 i will have to drag and drop GEO_WhitePawn_01
    public GameObject GEO_BlackPawn_01;
    public GameObject GEO_BlackPawn_02;
    public GameObject GEO_BlackPawn_03;
    public GameObject GEO_BlackPawn_04;
    public GameObject GEO_BlackPawn_05;
    public GameObject GEO_BlackPawn_06;
    public GameObject GEO_BlackPawn_07;
    public GameObject GEO_BlackPawn_08;
    public GameObject GEO_BlackRook_01;
    public GameObject GEO_BlackRook_02;
    public GameObject GEO_BlackKnight_01;
    public GameObject GEO_BlackKnight_02;
    public GameObject GEO_BlackBishop_01;
    public GameObject GEO_BlackBishop_02;
    public GameObject GEO_BlackQueen;
    public GameObject GEO_BlackKing;

    public GameObject GEO_WhitePawn_01;
    public GameObject GEO_WhitePawn_02;
    public GameObject GEO_WhitePawn_03;
    public GameObject GEO_WhitePawn_04;
    public GameObject GEO_WhitePawn_05;
    public GameObject GEO_WhitePawn_06;
    public GameObject GEO_WhitePawn_07;
    public GameObject GEO_WhitePawn_08;
    public GameObject GEO_WhiteRook_01;
    public GameObject GEO_WhiteRook_02;
    public GameObject GEO_WhiteKnight_01;
    public GameObject GEO_WhiteKnight_02;
    public GameObject GEO_WhiteBishop_01;
    public GameObject GEO_WhiteBishop_02;
    public GameObject GEO_WhiteQueen;
    public GameObject GEO_WhiteKing;


    private Vector2Int PosBlackPawn_01 = new Vector2Int(0, 6);
    private Vector2Int PosBlackPawn_02 = new Vector2Int(1, 6);
    private Vector2Int PosBlackPawn_03 = new Vector2Int(2, 6);
    private Vector2Int PosBlackPawn_04 = new Vector2Int(3, 6);
    private Vector2Int PosBlackPawn_05 = new Vector2Int(4, 6);
    private Vector2Int PosBlackPawn_06 = new Vector2Int(5, 6);
    private Vector2Int PosBlackPawn_07 = new Vector2Int(6, 6);
    private Vector2Int PosBlackPawn_08 = new Vector2Int(7, 6);
    private Vector2Int PosBlackRook_01 = new Vector2Int(0, 7);
    private Vector2Int PosBlackRook_02 = new Vector2Int(7, 7);
    private Vector2Int PosBlackKnight_01 = new Vector2Int(1, 7);
    private Vector2Int PosBlackKnight_02 = new Vector2Int(6, 7);
    private Vector2Int PosBlackBishop_01 = new Vector2Int(2, 7);
    private Vector2Int PosBlackBishop_02 = new Vector2Int(5, 7);
    private Vector2Int PosBlackQueen = new Vector2Int(3, 7);
    private Vector2Int PosBlackKing = new Vector2Int(4, 7);

    private Vector2Int PosWhitePawn_01 = new Vector2Int(0, 1);
    private Vector2Int PosWhitePawn_02 = new Vector2Int(1, 1);
    private Vector2Int PosWhitePawn_03 = new Vector2Int(2, 1);
    private Vector2Int PosWhitePawn_04 = new Vector2Int(3, 1);
    private Vector2Int PosWhitePawn_05 = new Vector2Int(4, 1);
    private Vector2Int PosWhitePawn_06 = new Vector2Int(5, 1);
    private Vector2Int PosWhitePawn_07 = new Vector2Int(6, 1);
    private Vector2Int PosWhitePawn_08 = new Vector2Int(7, 1);
    private Vector2Int PosWhiteRook_01 = new Vector2Int(0, 0);
    private Vector2Int PosWhiteRook_02 = new Vector2Int(7, 0);
    private Vector2Int PosWhiteKnight_01 = new Vector2Int(1, 0);
    private Vector2Int PosWhiteKnight_02 = new Vector2Int(6, 0);
    private Vector2Int PosWhiteBishop_01 = new Vector2Int(2, 0);
    private Vector2Int PosWhiteBishop_02 = new Vector2Int(5, 0);
    private Vector2Int PosWhiteQueen = new Vector2Int(3, 0);
    private Vector2Int PosWhiteKing = new Vector2Int(4, 0);

    private Dictionary<Vector2Int, GameObject> piecePositions = new Dictionary<Vector2Int, GameObject>();



    //Trial and error untill right (:
    private static readonly float tileOffset = 0.56726f * 2 - 0.2f; //Probably good
    //Vector3(Left/Right,Up/Down,Forwards/Backwards)

    private float moveSpeed = 1f;
    private float time = 4f;

    private List<string> moves = new List<string>();

    private IEnumerator WaitForIntro()
    {
        // Wait for the duration of the intro animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //After this Intro is done
        startingCameraPosition = mainCamera.transform.position;
        startingCameraRotation = mainCamera.transform.rotation;

        foreach (var move in moves)
        {
            yield return StartCoroutine(MovePiece(move));
        }
        hasPlayedMain = true;


    }
    private IEnumerator MovePiece(string move)
    {
        var from = GetFromJSON(move);
        var to = GetToJSON(move);
        var pieceType = GetPieceTypeJSON(move);
        var isWhite = GetIsWhiteJSON(move);
        if (piecePositions.ContainsKey(from))
        {
            GameObject piece = piecePositions[from];
            //Forward/Backward Jump
            int FBJump = to.y - from.y;
            //Left/Right Jump
            int LRJump = to.x - from.x;

            yield return StartCoroutine(AnimateMove(piece, new Vector3(tileOffset * LRJump, 0f, tileOffset * FBJump), duration, from, to));

            piecePositions.Remove(from);
            piecePositions[to] = piece;
        }
    }
    private IEnumerator AnimateMove(GameObject piece, Vector3 offset, float duration, Vector2Int from, Vector2Int to)
    {
        Vector3 startPos = piece.transform.position;
        Vector3 endPos = startPos + offset;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            piece.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        piece.transform.position = endPos;

        if (piecePositions.ContainsKey(to))
        {
            GameObject enemyPiece = piecePositions[to];

            if (enemyPiece != piece)
            {
                enemyPiece.SetActive(false);
                piecePositions.Remove(to);
            }
        }
    }
    bool didRun = false;
    private void Update()
    {
        if (hasPlayedMain && !didRun)
        {
            didRun = true;
            Debug.Log("Outro starting");
            PlayOutro();
        }
    }
    private void Start()
    {
        piecePositions[PosBlackPawn_01] = GEO_BlackPawn_01;
        piecePositions[PosBlackPawn_02] = GEO_BlackPawn_02;
        piecePositions[PosBlackPawn_03] = GEO_BlackPawn_03;
        piecePositions[PosBlackPawn_04] = GEO_BlackPawn_04;
        piecePositions[PosBlackPawn_05] = GEO_BlackPawn_05;
        piecePositions[PosBlackPawn_06] = GEO_BlackPawn_06;
        piecePositions[PosBlackPawn_07] = GEO_BlackPawn_07;
        piecePositions[PosBlackPawn_08] = GEO_BlackPawn_08;
        piecePositions[PosBlackRook_01] = GEO_BlackRook_01;
        piecePositions[PosBlackRook_02] = GEO_BlackRook_02;
        piecePositions[PosBlackKnight_01] = GEO_BlackKnight_01;
        piecePositions[PosBlackKnight_02] = GEO_BlackKnight_02;
        piecePositions[PosBlackBishop_01] = GEO_BlackBishop_01;
        piecePositions[PosBlackBishop_02] = GEO_BlackBishop_02;
        piecePositions[PosBlackQueen] = GEO_BlackQueen;
        piecePositions[PosBlackKing] = GEO_BlackKing;

        piecePositions[PosWhitePawn_01] = GEO_WhitePawn_01;
        piecePositions[PosWhitePawn_02] = GEO_WhitePawn_02;
        piecePositions[PosWhitePawn_03] = GEO_WhitePawn_03;
        piecePositions[PosWhitePawn_04] = GEO_WhitePawn_04;
        piecePositions[PosWhitePawn_05] = GEO_WhitePawn_05;
        piecePositions[PosWhitePawn_06] = GEO_WhitePawn_06;
        piecePositions[PosWhitePawn_07] = GEO_WhitePawn_07;
        piecePositions[PosWhitePawn_08] = GEO_WhitePawn_08;
        piecePositions[PosWhiteRook_01] = GEO_WhiteRook_01;
        piecePositions[PosWhiteRook_02] = GEO_WhiteRook_02;
        piecePositions[PosWhiteKnight_01] = GEO_WhiteKnight_01;
        piecePositions[PosWhiteKnight_02] = GEO_WhiteKnight_02;
        piecePositions[PosWhiteBishop_01] = GEO_WhiteBishop_01;
        piecePositions[PosWhiteBishop_02] = GEO_WhiteBishop_02;
        piecePositions[PosWhiteQueen] = GEO_WhiteQueen;
        piecePositions[PosWhiteKing] = GEO_WhiteKing;
        JSONDecoder.Decoder(moves);
        StartCoroutine(WaitForIntro());
    }

    private Vector2Int GetFromJSON(string json)
    {
        int fromIndex = json.IndexOf("From: ") + "From: ".Length;
        int toIndex = "(0,0)".Length + 1;
        string result = json.Substring(fromIndex, toIndex).Trim();
        Debug.Log(StringToVector2IntConverter(result));
        return StringToVector2IntConverter(result);
    }
    private Vector2Int GetToJSON(string json)
    {
        int fromIndex = json.IndexOf("To: ") + "To: ".Length;
        int toIndex = "(0,0)".Length + 1;
        string result = json.Substring(fromIndex, toIndex).Trim();
        Debug.Log(StringToVector2IntConverter(result));
        return StringToVector2IntConverter(result);
    }
    private PieceType GetPieceTypeJSON(string json)
    {
        int fromIndex = json.IndexOf("Piece Type: ") + "Piece Type: ".Length;
        int toIndex = json.IndexOf(",", fromIndex);
        string result = json.Substring(fromIndex, toIndex - fromIndex).Trim();
        Debug.Log(Enum.Parse<PieceType>(result));
        return Enum.Parse<PieceType>(result);
    }

    private bool GetIsWhiteJSON(string json)
    {
        int fromIndex = json.IndexOf("Is White: ") + "Is White: ".Length;
        string result = json.Substring(fromIndex).Trim();
        Debug.Log(bool.Parse(result));
        return bool.Parse(result);
    }

    private Vector2Int StringToVector2IntConverter(string str)
    {
        str = str.Trim('(', ')');
        string[] split = str.Split(',');

        int x = int.Parse(split[0].Trim());
        int y = int.Parse(split[1].Trim());

        return new Vector2Int(x, y);
    }
    public void PlayOutro()
    {
        animator.SetTrigger("PlayOutro");
        StartCoroutine(WaitForOutro());

    }
    IEnumerator WaitForOutro()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        //SceneManager.LoadScene("EndScreenUI");
    }
}