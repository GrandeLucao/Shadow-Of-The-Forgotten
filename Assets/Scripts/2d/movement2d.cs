using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2d : MonoBehaviour
{
    
    private Rigidbody rig;
    public float speed;


    void Start()
    {
        rig=GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float movement= Input.GetAxis("Horizontal");
        float movement2= Input.GetAxis("Vertical");
        rig.velocity=new Vector2(movement*speed, movement2*speed);
        if(movement>0){
            transform.eulerAngles=new Vector3(0,0,0);
            //anim.SetInteger("transition",1);
        }
        if(movement<0){
            transform.eulerAngles=new Vector3(0,180,0);
            //anim.SetInteger("transition",1);
        }
        /*if(movement==0){
            anim.SetInteger("transition",0);
        }*/

    }

}
