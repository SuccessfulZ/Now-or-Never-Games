using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GlobalConstants : MonoBehaviour
{
    public float resourceIncreaseTimer = 1f;
    public int maxOfResource = 10;
    public int resourceRemove = 1;

    public float playerHouseDetectRadius = 5f;
    public float playerRefillDetectRadius = 5f;
    public float houseSpawnRadius = 50f;

    public Transform player;
    public ResourceRandomizer resourceRandomizer;

    public int resourceCountTotalChance = 10;
    public int[] resourceCountChances = new int[]
    {
        1, 7, 10
    };

    public static GlobalConstants Singleton { get; private set; }

    public static float ResourceIncreaseTimer => Singleton.resourceIncreaseTimer;
    public static int MaxOfResource => Singleton.maxOfResource;
    public static int ResourceRemove => Singleton.resourceRemove;

    public static float PlayerHouseDetectRadius => Singleton.playerHouseDetectRadius;
    public static float PlayerRefillDetectRadius => Singleton.playerRefillDetectRadius;
    public static float HouseSpawnRadius => Singleton.houseSpawnRadius;

    public static Transform Player => Singleton?.player;
    public static ResourceRandomizer ResourceRandomizer => Singleton?.resourceRandomizer;

    public static int ResourceCountTotalChance => Singleton.resourceCountTotalChance;
    public static int[] ResourceCountChances => Singleton.resourceCountChances;

    public void Awake()
    {
        Singleton = this;
    }

    public void Reset()
    {
        Singleton = this;
    }

    public void Start()
    {
        Singleton = this;
    }

    public void OnDestroy()
    {
        Singleton = null;
    }
}
