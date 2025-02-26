using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    [SerializeField] Tilemap pathrTilemap;
    [SerializeField] TileBase pathTile;

    List<GameObject> roomObj = new List<GameObject>();

    public void Initialize(Tilemap tilemap, TileBase tile)
    {
        pathrTilemap = tilemap;
        pathTile = tile;
    }

    public void SetRoom(List<GameObject> room)
    {
        roomObj = room;
    }
    public void ConnectRooms()
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        HashSet<GameObject> visited = new HashSet<GameObject>();

        if (roomObj.Count == 0)
        {
            Debug.LogError("방이 없어");
            return;
        }

        GameObject startRoom = roomObj[0];
        queue.Enqueue(startRoom);
        visited.Add(startRoom);

        while (queue.Count > 0)
        {
            GameObject nowRoom = queue.Dequeue();
            GameObject nearRoom = FindNearRoom(nowRoom,visited);

            if (nearRoom != null)
            {
                visited.Add(nearRoom);
                queue.Enqueue(nearRoom);
                Debug.Log($"방연결됨: {nowRoom.transform.position}-{nearRoom.transform.position}");
                
            }
        }
    }
    GameObject FindNearRoom(GameObject nowRoom,HashSet<GameObject> visited)
    {
        GameObject findNearRoom = null;
        float minDis = float.MaxValue;

        foreach(GameObject room in roomObj)
        {
            if (visited.Contains(room)) continue;


        }
    }
}
