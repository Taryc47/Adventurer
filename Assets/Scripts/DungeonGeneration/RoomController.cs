using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
	public string name;
	public int X;
	public int Z;
}

public class RoomController : MonoBehaviour
{
	
	public static RoomController instance;
    readonly string currentWorldName = "Forest";
	
	RoomInfo currentLoadRoomData;

    Room currRoom;
	
	Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
	
	public List<Room> loadedRooms = new List<Room>();
	
	bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;
	
	void Awake()
	{
		instance = this;
	}

    void Start()
    {

        /*
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
        */
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.CreateDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Z);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Z == tempRoom.Z);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Exit", tempRoom.X, tempRoom.Z);
        }
    }

    public void LoadRoom(string name, int x, int z)
	{
        if (DoesRoomExist(x, z))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo
        {
            name = name,
            X = x,
            Z = z
        };

        loadRoomQueue.Enqueue(newRoomData);
	}
	
	IEnumerator LoadRoomRoutine(RoomInfo info)
	{
		string roomName = currentWorldName + info.name;
		
		AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
		
		while(loadRoom.isDone == false)
		{
			yield return null;
		}
	}

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Z))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width, 0,
                currentLoadRoomData.Z * room.Height);

            room.X = currentLoadRoomData.X;
            room.Z = currentLoadRoomData.Z;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Z;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
	
	public bool DoesRoomExist(int x, int z)
	{
		return loadedRooms.Find(item => item.X == x && item.Z == z) != null;
	}

    public Room FindRoom(int x, int z)
    {
        return loadedRooms.Find(item => item.X == x && item.Z == z);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
    }
}