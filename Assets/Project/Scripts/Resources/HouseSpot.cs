using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpot : MonoBehaviour
{
    public ResourceBundle[] requests;

    private PlayerStash player;

    public void Start()
    {
        player = GlobalConstants.Player.GetComponent<PlayerStash>();
    }

    public void Update()
    {
        if (CheckPlayer(player.transform))
        {
            //player.SendMessage(nameof(player.ApproachedHouse));
            if (Input.GetButtonDown("Submit") && PlayerHasResources())
            {
                for (int i = 0; i < requests.Length; ++i)
                {
                    player.SendMessage(nameof(player.RemoveResource), requests[i]);
                }
                Destroy(this);
                Destroy(transform.GetChild(0));
            }
        }
    }

    public bool PlayerHasResources()
    {
        for (int i = 0; i < requests.Length; ++i)
        {
            var currentBundle = player.stash.Find(x => x.type == requests[i].type);
            if (currentBundle == null) return false;
            if (currentBundle.count < requests[i].count) return false;
        }

        return true;
    }

    public bool CheckPlayer(Transform player)
    {
        if (player == null) return false;
        return Vector3.Distance(player.position, transform.position) <= GlobalConstants.PlayerHouseDetectRadius;
    }

    public void OnDrawGizmos()
    {
        if (GlobalConstants.Singleton == null) return;
        if (CheckPlayer(GlobalConstants.Player))
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GlobalConstants.PlayerHouseDetectRadius);
    }
}
