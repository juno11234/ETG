using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonManager
{
    public List<Room> roomList { get; private set; }
    Factory roomFactory;
    int roomCount = 6;
   
    public DungeonManager( Factory factory)
    {
        
        roomFactory = factory;
        
        roomList = new List<Room>();
        GenerateDungeon();
    }
    Vector2Int RandomDirection()//방생성방향을 랜덤으로해줄 함수
    {
        Vector2Int[] direction = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        return direction[Random.Range(0, direction.Length)];
    }

    Room FindBossRoom(Room startRoom)
    {
        Room findBossRoom = startRoom;
        int maxDistance = 0;

        foreach (Room room in roomList)
        {
            int distance = Mathf.Abs(room.roomposition.x - startRoom.roomposition.x)
            + Mathf.Abs(room.roomposition.y - startRoom.roomposition.y);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                findBossRoom = room;
            }
        }
        return findBossRoom;
    }

    private void GenerateDungeon()
    {
        Vector2Int startPos = Vector2Int.zero;
        //Room startRoom = roomFactory.CreateRoom(startPos, RoomType.Start);
        Room startRoom = new Room(startPos, RoomType.Start);
        roomList.Add(startRoom);

        for (int i = 1; i < roomCount; i++)
        {
            Vector2Int newPos;

            do
            {
                newPos = startPos + RandomDirection();
            } while (roomList.Exists(r => r.roomposition == newPos));
            //foreach (Room r in roomlist) {if (r.roomposition==newPos){true;}}while

            RoomType roomType =
               (RoomType)Random.Range((int)RoomType.Normal1, (int)RoomType.Normal3 + 1);

            Room newRoom = roomFactory.CreateRoom(newPos, roomType);
            roomList.Add(newRoom);
           // startRoom.Connect(newRoom);//
            startPos = newPos;

        }
        RemoveWithBoss();
    }
    void RemoveWithBoss()
    {
        Room startRoom = roomList[0];
        Room farRoom = FindBossRoom(startRoom);

        roomList.Remove(farRoom);

        Room bossRoom = new Room(farRoom.roomposition, RoomType.Boss);
        roomList.Add(bossRoom);
    }

}


