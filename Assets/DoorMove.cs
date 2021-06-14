using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public GameObject door;
    public GameObject door1;
    public GameObject door2;
    public bool doorswitch = false;
    public bool doorswitch1 = false;
    public bool doorswitch2 = false;
    Vector3 open;
    Vector3 open1;
    Vector3 open2;
    Vector3 closed;
    Vector3 closed1;
    Vector3 closed2;
    private void Start()
    {
        open = door.transform.position + Vector3.up * 20;
        closed = door.transform.position;
        open1 = door1.transform.position + Vector3.up * 20;
        closed1 = door1.transform.position;
        open2 = door2.transform.position + Vector3.up * 20;
        closed2 = door2.transform.position;

    }


    void Update()
    {
        if (doorswitch == true)
        {
            door.transform.position = Vector3.Lerp(closed, open, 3);
        }
        if (doorswitch1 == true)
        {
            door1.transform.position = Vector3.Lerp(closed1, open1, 3);
        }
        if (doorswitch2 == true)
        {
            door2.transform.position = Vector3.Lerp(closed2, open2, 3);
        }

    }
}
