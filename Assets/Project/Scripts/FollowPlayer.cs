using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 13, 12);
    private float speed = 20.0f;
    //private Quaternion offsetRotation = new Quaternion.Euler(34.3f,-180f,0f);
    //Quaternion newRotation = player.transform.rotation * otherTransform.rotation;


    // Update is called once per frame
    void Update()
    {
        // Offset the camera behind the player by adding to the player's position
        transform.position = player.transform.position + offset;
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y - 180; //I forgot to write down the .y on transform.position
        targetPos.x = transform.position.x + 34.3f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos, transform.position), speed * Time.deltaTime);
        //Quaternion newRotation = transform.rotation * ;
        //transform.rotation = player.transform.rotation;
    }
}
