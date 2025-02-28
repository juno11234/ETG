using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AstarPathFind
{
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> obstacles)
    {
        List<Vector2Int> path = new List<Vector2Int>(); //���� ��θ� ������ ����Ʈ
        List<Node> openList = new List<Node>();//Ž���� ��� ���
        HashSet<Node> closeList = new HashSet<Node>();//Ž���� ��� ���
        Vector2Int[] direction = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        Node startNode = new Node(start);
        openList.Add(startNode);//��ŸƮ ������ ����

        while (openList.Count > 0)//�ּҰ��� ã��
        {
            Node currentNode = openList[0];

            foreach (Node node in openList)//Ž���ҳ�帮��Ʈ���ִ� 
            {
                if (node.fCost < currentNode.fCost//��尡 �������� f����������
                 || (node.fCost == currentNode.fCost//���� ���ٸ� 
                 && node.hCost < currentNode.hCost))//h ��
                {
                    currentNode = node;//��尡 �����庸�� �۴ٸ�  
                }
            }
            openList.Remove(currentNode);//Ž���̳����� Ž���Ҹ���Ʈ��������
            closeList.Add(currentNode);//Ž���� ����Ʈ�� �߰�

            if (currentNode.position == goal)
            {
                List<Vector2Int> paths = new List<Vector2Int>();
                while (currentNode != null)
                {
                    path.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }
                path.Reverse();
                return path;
            }


            foreach (Vector2Int dir in direction)
            {
                Vector2Int neighborPos = currentNode.position + dir;

                if (obstacles.Contains(neighborPos) || closeList.Any(n => n.position == neighborPos))
                    continue;

                Node neighbor = new Node(neighborPos)
                {
                    gCost = currentNode.gCost + 1,
                    hCost = Mathf.Abs(goal.x - neighborPos.x) + Mathf.Abs(goal.y - neighborPos.y),
                    parent = currentNode
                };

                if (!openList.Exists(n => n.position == neighborPos) || neighbor.gCost < openList.Find(n => n.position == neighborPos).gCost)
                    openList.Add(neighbor);
            }
        }
        return new List<Vector2Int>();
        
    }
}
