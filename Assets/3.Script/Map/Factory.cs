using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
  public Room CreateRoom(Vector2Int position, RoomType type)//Room��ü�� �����ϴ� ���丮����
    {
        return new Room(position, type);
    }
}
