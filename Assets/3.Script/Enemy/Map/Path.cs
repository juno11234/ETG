using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    Tilemap pathrTilemap;
     TileBase pathTile;

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
                CreatePath(nowRoom.transform.position, nearRoom.transform.position);
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

            float distance = Vector3.Distance(nowRoom.transform.position, room.transform.position);
            if (distance < minDis)
            {
                minDis = distance;
                findNearRoom = room;
            }

        }
        return findNearRoom;
    }
    void CreatePath(Vector3 start,Vector3 end)
    {
        Vector3 current = start;
        Debug.Log( $"길생성{start}-{end}");
        while (Vector3.Distance(current, end) > 0.5f)
        {
            if (Mathf.Abs(current.x - end.x) > Mathf.Abs(current.y - end.y))
            {
                current.x += (end.x > current.x) ? 1f : -1f;
            }
            else
            {
                current.y += (end.y > current.y) ? 1f : -1f;
            }

            Vector3Int tilePosition = pathrTilemap.WorldToCell(current);
            pathrTilemap.SetTile(tilePosition, pathTile);
            Debug.Log($"타일배치{tilePosition}");
        }
    }
}
