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

    public Room(Vector2Int position, RoomType type)//�����Ҷ� ���ڰ����� ����2,�������� �޴´�
    {
        roomposition = position;
        roomtype = type;
      //  connectedRooms = new List<Room>();//������ �ȿ��� �ʱ�ȭ ���� �ٸ��ʵ� ���� �����ϱ� ����

    }
  /*  public void Connect(Room other)//Ŀ��Ʈ ���ڰ� ��Ŭ����
    {
        if (!connectedRooms.Contains(other))//����Ʈ�� other��������� 
        {
            connectedRooms.Add(other);
            other.connectedRooms.Add(this);//other���� �Լ�������� ��ü�߰�
        }
    }*/
}
