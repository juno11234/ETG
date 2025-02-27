using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int width = 10;//������ ���μ���ũ��
    [SerializeField] int height = 10;
    [SerializeField] float nodeSize = 1f;//����庰 ũ��
    private Node[,] grid;//��带 ������ ��������
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()//���ڻ����Լ�
    {
        grid = new Node[width, height];//�ʱ�ȭ

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2 worldPos = new Vector2(x * nodeSize, y * nodeSize);//
                bool isWalkable = !Physics2D.OverlapCircle(worldPos, 0.4f);//0.������������ ��ֹ����
                grid[x, y] = new Node(worldPos, isWalkable);//������
            }
        }
    }
    public Node GetNodeWorld(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x / nodeSize);//�Ҽ��� ������ȯ
        int y = Mathf.RoundToInt(position.x / nodeSize);
        return grid[x, y];
    }

    
 
}
