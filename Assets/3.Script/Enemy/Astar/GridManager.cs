using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int width = 10;//격자의 가로세로크기
    [SerializeField] int height = 10;
    [SerializeField] float nodeSize = 1f;//각노드별 크기
    private Node[,] grid;//노드를 저장할 변수선언
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()//격자생성함수
    {
        grid = new Node[width, height];//초기화

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2 worldPos = new Vector2(x * nodeSize, y * nodeSize);//
                bool isWalkable = !Physics2D.OverlapCircle(worldPos, 0.4f);//0.반지름원으로 장애물계산
                grid[x, y] = new Node(worldPos, isWalkable);//노드생성
            }
        }
    }
    public Node GetNodeWorld(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / nodeSize);//소수를 정수변환
        int y = Mathf.RoundToInt(position.x / nodeSize);
        return grid[x, y];
    }

    
 
}
