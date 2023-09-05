using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraChange : MonoBehaviour
{
    public Transform bedroom;
    public Transform library;
    public Transform waiting;
    public Transform entrance;
    public Transform dining;
    public Transform sotao;
    public Transform basement;

    public void Start()
    {        
        transform.position=bedroom.position;
    }

    public void CameraChange(int roomN)
    {
        switch (roomN){
            case 1:
                transform.position=bedroom.position;
                break;
            case 2:
                transform.position=library.position;
                break;
            case 3:
                transform.position=waiting.position;
                break;
            case 4:
                transform.position=entrance.position;
                break;
            case 5:
                transform.position=dining.position;
                break;
            case 6:
                transform.position=sotao.position;
                break;
            case 7:
                transform.position=basement.position;
                break;
        }
        
    }
}
