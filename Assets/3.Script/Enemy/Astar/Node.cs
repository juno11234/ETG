using UnityEngine;

public class Node
{
    public Vector2 Position { get; private set; }
    public bool IsWalkable { get; private set; }
    public Node(Vector2 position, bool isWalkable)//��ġ�� ��ֹ�����
    {
        Position = position;
        IsWalkable = isWalkable;
    }



}
