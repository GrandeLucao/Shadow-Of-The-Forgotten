using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class charCollision : MonoBehaviour
{
    private bool bed,lib,tv,ent,pain,din,sot;
   
    private void OnTriggerEnter(Collider coll)
    {
        switch (coll.gameObject.tag){
            case "bedroom":
                FindObjectOfType<cameraChange>().CameraChange(1);
                break;
            case "library":
                FindObjectOfType<cameraChange>().CameraChange(2);
                if(!lib){
                    FindObjectOfType<AudioManager>().Play("library");
                    lib=true;
                }    
                break;
            case "tv":
                FindObjectOfType<cameraChange>().CameraChange(3);
                if(!tv){
                    FindObjectOfType<AudioManager>().Play("tv");
                    tv=true;
                }    
                break;
            case "entrance":
                FindObjectOfType<cameraChange>().CameraChange(4);
                break;
            case "painting":
                if(!pain){
                    FindObjectOfType<AudioManager>().Play("painting");
                    pain=true;
                }    
                break;
            case "dining":
                FindObjectOfType<cameraChange>().CameraChange(5);
                if(!din){
                    FindObjectOfType<AudioManager>().Play("dining");
                    din=true;
                }    
                break;
            case "sotao":
                FindObjectOfType<cameraChange>().CameraChange(6);
                if(!sot){
                    FindObjectOfType<AudioManager>().Play("sotao");
                    sot=true;
                }
                break;
            case "basement":
                    FindObjectOfType<cameraChange>().CameraChange(7);
                
                break;
            case "key":
                FindObjectOfType<baseDoor>().gotKey=true;
                Destroy(coll.gameObject);
                break;
            case "portal":
                    SceneManager.LoadScene(1);
                    FindObjectOfType<AudioManager>().Play("BGM");
                break;
            default:
                break;

        }
    }
}
