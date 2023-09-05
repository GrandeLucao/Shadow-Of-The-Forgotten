using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDoor : MonoBehaviour
{
    public bool gotKey=false;

    private void Update(){
        if(gotKey){
            Destroy(this.gameObject);
        }
    }
}
