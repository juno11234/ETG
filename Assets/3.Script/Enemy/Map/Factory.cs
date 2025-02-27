using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
  public Room CreateRoom(Vector2Int position, RoomType type)//Room객체를 생서하는 팩토리패턴
    {
        return new Room(position, type);
    }
}
