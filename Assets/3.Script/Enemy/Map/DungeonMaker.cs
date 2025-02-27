using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonMaker : MonoBehaviour
{
    [SerializeField] GameObject StartPrefab;
    [SerializeField] GameObject BossPrefab;
    [SerializeField] GameObject normalPrefab1;
    [SerializeField] GameObject normalPrefab2;
    [SerializeField] GameObject normalPrefab3;
    [SerializeField] float roomSpacing= 10f;
    [SerializeField] Tilemap pathrTilemap;
    [SerializeField] TileBase pathTile;
    private List<GameObject> spawnedRooms = new List<GameObject>();
    Path path;
    
  
    DungeonManager dungeonManager;
    Factory roomfactory = new Factory();
    void Start()
    {
        dungeonManager = new DungeonManager(roomfactory);    

        RenderDungeon();       
    }
   
    GameObject GetRoomPrefab(RoomType type)
    {
        
        switch (type)
        {
            case RoomType.Start:
                return StartPrefab;
            case RoomType.Boss:
                return BossPrefab;
            case RoomType.Normal1:
                return normalPrefab1;
            case RoomType.Normal2:
                return normalPrefab2;
            case RoomType.Normal3:
                return normalPrefab3;
            default:
                return normalPrefab1;
        }
    }
        
    void RenderDungeon()
    {
        
        foreach (Room room in dungeonManager.roomList)
        {            
            GameObject roomPrefab = GetRoomPrefab(room.roomtype);
            float RoomSize= roomSpacing;//false;일ㄱ 경우대비 방어코드
            if(TryGetComponentInChildren<Tilemap>(roomPrefab, out Tilemap tilemap ))
            {
                RoomSize = tilemap.cellBounds.size.x;
            }
            
            roomSpacing = Mathf.Max(roomSpacing, RoomSize);
            Vector3 worldPos = new Vector3(room.roomposition.x * (roomSpacing+RoomSize/2),
                                           room.roomposition.y * (roomSpacing+RoomSize/2), 0f);
            Instantiate(roomPrefab, worldPos, Quaternion.identity);
        }
    }       
           
    bool TryGetComponentInChildren<T>(GameObject obj, out T component) where T :Component
    {
        component = obj.GetComponentInChildren<T>();
        return component != null;
    }

     

    
   
}
