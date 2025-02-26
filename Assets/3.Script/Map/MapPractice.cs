using System.Collections.Generic;
using UnityEngine;
namespace practice
{
    public enum RoomType//방종류를 담을 열거형
    {
        Boss,
        Start,
        Normal1,
        Normal2,
        Normal3
    }
    public class Room //방정보
    {
        public Vector2Int roomPos { get; private set; }//방위치
        public RoomType roomType { get; private set; }//방종류
        public List<Room> connected { get; private set; }//연결된 방 리스트

        public Room(Vector2Int pos, RoomType type)//생성자
        {
            roomPos = pos;//위치초기화
            roomType = type;//종류초기화
            connected = new List<Room>();//연결된방 리스트 초기화
        }
        public void Connect(Room other)//방연결하는 함수
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
        public Room Create(Vector2Int pos, RoomType type)//팩토리패턴 방생성함수
        {
            return new Room(pos, type);
        }
    }
    public class DungeonManager
    {
        public List<Room> roomList { get; private set; }//방을 담을 리스트뱐수
        private Factory roomFactory;//팩토리를 담을변수
        private int roomCount = 6;//방개수를 담을변수

        public DungeonManager(Factory factory)//생성자 
        {
            roomFactory = factory;//팩토리 초기화
            roomList = new List<Room>();//리스트 초기화
            GenerateDungeon();

        }
        Vector2Int RandomDirection()//랜덤방향 이동
        {
            Vector2Int[] direction //벡터2정수배열 벡터.상하좌우 저장
                = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            return direction[Random.Range(0, direction.Length)];//배열중 랜덤으로 반환
        }
        void GenerateDungeon()//던전생성
        {
            Vector2Int starPos = Vector2Int.zero;//0,0으로 시작좌표
            Room startRoom = new Room(starPos, RoomType.Start);//시작방 생성
            roomList.Add(startRoom);//방리스트에 추가

            for (int i = 1; i < roomCount; i++)
            {//방개수만큼 생성 시작지점이 정해졌기때문에 1부터
                Vector2Int newPos;//새포지션을 담을 변수

                do
                {
                    newPos = starPos + RandomDirection();//무조건한번실행
                } while (roomList.Exists(r => r.roomPos == newPos));
                //foreach (Room r in roomlist) {if (r.roomposition==newPos){true;}}while
                //룸리스트안에 배열의 포지션이 새포지션과 겹치는지 확인 겹치면트루 아니면 false
                RoomType type =
                    (RoomType)Random.Range((int)RoomType.Normal1, (int)RoomType.Normal3 + 1);
                //type은 룸타입 노말 1이상3+1미만 랜덤 (RoomType or int)는 값변환 
                Room newRoom = roomFactory.Create(newPos, type);
                //newRoom은 팩토리 Create함수실행 while로 정해진 새좌표와 랜덤타입으로
                roomList.Add(newRoom);
                //그리고 리스트에 추가
                startRoom.Connect(newRoom);
                //스타트룸과연결
                starPos = newPos;
                //뉴포지션+랜덤방향으로다시 돌림for조건 끝날때까지
            }

        }

        Room FindBoss(Room starRoom)
        {
            Room findBoss = starRoom;//보스룸 시작지점부터
            int maxDistance = 0;//최대거리

            foreach (Room room in roomList)//방리스트 순회
            {
                int distance = Mathf.Abs(room.roomPos.x - starRoom.roomPos.x)
                    + Mathf.Abs(room.roomPos.y - starRoom.roomPos.y);
                //맨해튼거리공식으로 배열룸과 시작룸의 distance 계산

                if (distance > maxDistance)//distance가 maxdistance 보다 크다면
                {
                    maxDistance = distance;//maxdistance는 distance가되고
                    findBoss = room;//그방이 보스방이된다
                }
            }
            return findBoss;//보스방 반환
        }
        void RemoveWithBoss()
        {
            Room startRoom = roomList[0];//룸리스트 0번은 변수시작룸
            Room farRoom = FindBoss(startRoom);//먼방은 보스룸 함수의 인자값
            //
            roomList.Remove(farRoom);//가장 먼방을 제거하고

            Room bossRoom = new Room(farRoom.roomPos, RoomType.Boss);
            //먼방에 보스방을 생성
            roomList.Add(bossRoom);
            //룸리스트에 추가
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

