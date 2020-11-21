using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 72.29f, 34);      


    // Update is called once per frame
    void Update()
    {       

        // Offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
        
    }
}
