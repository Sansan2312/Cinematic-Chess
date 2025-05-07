using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LegalMovesHighlighter : UniversalBase
{
    public GameObject highlightGray;
    private readonly List<GameObject> highlights = new();

    public void ShowHighlights(List<Vector2Int> positions)
    {
        ClearHighlights();

        foreach (var pos in positions)
        {
            Vector3 position = GetTilePosition(pos.x, pos.y, -0.22f);
            GameObject obj = Instantiate(highlightGray, position, Quaternion.identity);
            highlights.Add(obj);
        }
    }
    public void ClearHighlights()
    {
        foreach (var highlight in highlights)
        {
            Destroy(highlight);
        }
        highlights.Clear();
    }
}
