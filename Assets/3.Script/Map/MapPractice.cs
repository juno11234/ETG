using System.Collections.Generic;
using UnityEngine;
namespace practice
{
    public enum RoomType//�������� ���� ������
    {
        Boss,
        Start,
        Normal1,
        Normal2,
        Normal3
    }
    public class Room //������
    {
        public Vector2Int roomPos { get; private set; }//����ġ
        public RoomType roomType { get; private set; }//������
        public List<Room> connected { get; private set; }//����� �� ����Ʈ

        public Room(Vector2Int pos, RoomType type)//������
        {
            roomPos = pos;//��ġ�ʱ�ȭ
            roomType = type;//�����ʱ�ȭ
            connected = new List<Room>();//����ȹ� ����Ʈ �ʱ�ȭ
        }
        public void Connect(Room other)//�濬���ϴ� �Լ�
        {
            if (!connected.Contains(other))
            {
                connected.Add(other);
                other.connected.Add(this);
            }
        }

    }
    public class Factory
    {
        public Room Create(Vector2Int pos, RoomType type)//���丮���� ������Լ�
        {
            return new Room(pos, type);
        }
    }
    public class DungeonManager
    {
        public List<Room> roomList { get; private set; }//���� ���� ����Ʈ����
        private Factory roomFactory;//���丮�� ��������
        private int roomCount = 6;//�氳���� ��������

        public DungeonManager(Factory factory)//������ 
        {
            roomFactory = factory;//���丮 �ʱ�ȭ
            roomList = new List<Room>();//����Ʈ �ʱ�ȭ
            GenerateDungeon();

        }
        Vector2Int RandomDirection()//�������� �̵�
        {
            Vector2Int[] direction //����2�����迭 ����.�����¿� ����
                = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            return direction[Random.Range(0, direction.Length)];//�迭�� �������� ��ȯ
        }
        void GenerateDungeon()//��������
        {
            Vector2Int starPos = Vector2Int.zero;//0,0���� ������ǥ
            Room startRoom = new Room(starPos, RoomType.Start);//���۹� ����
            roomList.Add(startRoom);//�渮��Ʈ�� �߰�

            for (int i = 1; i < roomCount; i++)
            {//�氳����ŭ ���� ���������� �������⶧���� 1����
                Vector2Int newPos;//���������� ���� ����

                do
                {
                    newPos = starPos + RandomDirection();//�������ѹ�����
                } while (roomList.Exists(r => r.roomPos == newPos));
                //foreach (Room r in roomlist) {if (r.roomposition==newPos){true;}}while
                //�븮��Ʈ�ȿ� �迭�� �������� �������ǰ� ��ġ���� Ȯ�� ��ġ��Ʈ�� �ƴϸ� false
                RoomType type =
                    (RoomType)Random.Range((int)RoomType.Normal1, (int)RoomType.Normal3 + 1);
                //type�� ��Ÿ�� �븻 1�̻�3+1�̸� ���� (RoomType or int)�� ����ȯ 
                Room newRoom = roomFactory.Create(newPos, type);
                //newRoom�� ���丮 Create�Լ����� while�� ������ ����ǥ�� ����Ÿ������
                roomList.Add(newRoom);
                //�׸��� ����Ʈ�� �߰�
                startRoom.Connect(newRoom);
                //��ŸƮ�������
                starPos = newPos;
                //��������+�����������δٽ� ����for���� ����������
            }

        }

        Room FindBoss(Room starRoom)
        {
            Room findBoss = starRoom;//������ ������������
            int maxDistance = 0;//�ִ�Ÿ�

            foreach (Room room in roomList)//�渮��Ʈ ��ȸ
            {
                int distance = Mathf.Abs(room.roomPos.x - starRoom.roomPos.x)
                    + Mathf.Abs(room.roomPos.y - starRoom.roomPos.y);
                //����ư�Ÿ��������� �迭��� ���۷��� distance ���

                if (distance > maxDistance)//distance�� maxdistance ���� ũ�ٸ�
                {
                    maxDistance = distance;//maxdistance�� distance���ǰ�
                    findBoss = room;//�׹��� �������̵ȴ�
                }
            }
            return findBoss;//������ ��ȯ
        }
        void RemoveWithBoss()
        {
            Room startRoom = roomList[0];//�븮��Ʈ 0���� �������۷�
            Room farRoom = FindBoss(startRoom);//�չ��� ������ �Լ��� ���ڰ�
            //
            roomList.Remove(farRoom);//���� �չ��� �����ϰ�

            Room bossRoom = new Room(farRoom.roomPos, RoomType.Boss);
            //�չ濡 �������� ����
            roomList.Add(bossRoom);
            //�븮��Ʈ�� �߰�
        }
    }



    public class MapPractice : MonoBehaviour
    {
        [SerializeField] GameObject StartPrefab;
        [SerializeField] GameObject BossPrefab;
        [SerializeField] GameObject normalPrefab1;
        [SerializeField] GameObject normalPrefab2;
        [SerializeField] GameObject normalPrefab3;
        [SerializeField] float roomSize = 10f;

        DungeonManager dungeonManager;
        Factory factory = new Factory();

        private void Start()
        {
            dungeonManager = new DungeonManager(factory);

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
                GameObject roomPrefab = GetRoomPrefab(room.roomType);
                Vector3 worldPos = new Vector3(room.roomPos.x * roomSize,
                                               room.roomPos.y * roomSize, 0f);
                Instantiate(roomPrefab, worldPos, Quaternion.identity);
            }
        }
    }
}

