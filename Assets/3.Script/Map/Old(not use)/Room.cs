using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public enum RoomType
{
    Start,
    Boss,
    Normal1,
    Normal2,
    Normal3
}
public class Room 
{   
    public Vector2Int roomposition { get; private set; }
    public RoomType roomtype { get; private set; }
   // public List<Room> connectedRooms { get; private set;  }

    public Room(Vector2Int position, RoomType type)//생성할때 인자값으로 벡터2,열거형을 받는다
    {
        roomposition = position;
        roomtype = type;
      //  connectedRooms = new List<Room>();//생성자 안에서 초기화 해줌 다른필드 값에 의존하기 때문

    }
  /*  public void Connect(Room other)//커넥트 인자값 룸클래스
    {
        if (!connectedRooms.Contains(other))//리스트에 other가없어야함 
        {
            connectedRooms.Add(other);
            other.connectedRooms.Add(this);//other에도 함수가실행된 객체추가
        }
    }*/
}
