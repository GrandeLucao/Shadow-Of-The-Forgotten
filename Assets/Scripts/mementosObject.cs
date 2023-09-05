using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mementosObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag=="Player")
        {
            FindObjectOfType<AudioManager>().Play("Gotcha");
            gameController.instance.Objective();
            Destroy(gameObject);
        }
    }
}
