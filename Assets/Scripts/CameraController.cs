using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    public Room currRoom;
    public float moveSpeedOnRoomChange;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedOnRoomChange);
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.y = transform.position.y;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition());
    }
}
