using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector3> avaiablePoints = new List<Vector3>();

    void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 9;
        grid.rows = room.Height - 9;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.z;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int z = 0; z < grid.rows; z++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector3(x - (grid.columns - grid.horizontalOffset), 1,
                    z - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Z: " + z;
                avaiablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitializeObjectSpawning();
    }
}
