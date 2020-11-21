using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillSpot : MonoBehaviour
{
    public ResourceBundle[] refillBundles;

    private PlayerStash player;

    public void Start()
    {
        player = GlobalConstants.Player.GetComponent<PlayerStash>();
        InvokeRepeating(nameof(RefillPlayer), GlobalConstants.ResourceIncreaseTimer, GlobalConstants.ResourceIncreaseTimer);
    }

    public void OnDestroy()
    {
        CancelInvoke(nameof(RefillPlayer));
    }

    public void RefillPlayer()
    {
        if (refillBundles == null) return;
        if (player == null)
            player = GlobalConstants.Player.GetComponent<PlayerStash>();

        if (CheckPlayer(player.transform))
        {
            for (int i = 0; i < refillBundles.Length; ++i)
            {
                player.SendMessage(nameof(player.RefillResource), refillBundles[i]);
            }
        }
    }

    public bool CheckPlayer(Transform player)
    {
        if (player == null) return false;
        return Vector3.Distance(player.position, transform.position) <= GlobalConstants.PlayerRefillDetectRadius;
    }

    public void OnDrawGizmos()
    {
        if (GlobalConstants.Singleton == null) return;
        if (CheckPlayer(GlobalConstants.Player))
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GlobalConstants.PlayerRefillDetectRadius);
    }
}
