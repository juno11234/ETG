using UnityEngine;

public class Node
{
    public Vector2 Position { get; private set; }
    public bool IsWalkable { get; private set; }
    public Node(Vector2 position, bool isWalkable)//위치와 장애물여부
    {
        Position = position;
        IsWalkable = isWalkable;
    }



}
