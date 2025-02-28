using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AstarPathFind
{
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> obstacles)
    {
        List<Vector2Int> path = new List<Vector2Int>(); //최종 경로를 저장할 리스트
        List<Node> openList = new List<Node>();//탐색할 노드 목록
        HashSet<Node> closeList = new HashSet<Node>();//탐색한 노드 목록
        Vector2Int[] direction = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        Node startNode = new Node(start);
        openList.Add(startNode);//스타트 노드부터 시작

        while (openList.Count > 0)//최소값을 찾음
        {
            Node currentNode = openList[0];

            foreach (Node node in openList)//탐색할노드리스트에있는 
            {
                if (node.fCost < currentNode.fCost//노드가 현재노드의 f보다작을때
                 || (node.fCost == currentNode.fCost//만약 같다면 
                 && node.hCost < currentNode.hCost))//h 비교
                {
                    currentNode = node;//노드가 현재노드보다 작다면  
                }
            }
            openList.Remove(currentNode);//탐색이끝나면 탐색할리스트에서제거
            closeList.Add(currentNode);//탐색한 리스트에 추가

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
