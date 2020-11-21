using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpot : MonoBehaviour
{
    public ResourceBundle[] requests;

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
