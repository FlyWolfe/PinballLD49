using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramps : MonoBehaviour
{
    public float speedUpForce;



    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player)
        { 
            player.AddForce(transform.forward, speedUpForce);
        }
    }

}
