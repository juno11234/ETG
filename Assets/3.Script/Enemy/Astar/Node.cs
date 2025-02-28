using UnityEngine;

public class Node
{
    public Vector2Int position;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;
    public Node parent;

    public Node(Vector2Int pos)
    {
        position = pos;
    }

}
