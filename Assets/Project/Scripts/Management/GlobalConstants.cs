﻿using System.Collections;
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
    public StoryPicker storyPicker;
    public SceneManager sceneManager;
    public CountdownControl countdownControl;

    public bool spawnHouseAfterStory = true;
    public bool spawnHouseTimer = true;
    public float spawnHouseTimerMin = 15f;
    public float spawnHouseTimerMax = 30f;

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
    public static StoryPicker StoryPicker => Singleton?.storyPicker;
    public static SceneManager SceneManager => Singleton?.sceneManager;
    public static CountdownControl CountdownControl => Singleton?.countdownControl;

    public static bool SpawnHouseAfterStory => Singleton.spawnHouseAfterStory;
    public static bool SpawnHouseTimer => Singleton.spawnHouseTimer;
    public static float SpawnHouseTimerMin => Singleton.spawnHouseTimerMin;
    public static float SpawnHouseTimerMax => Singleton.spawnHouseTimerMax;

    public static int ResourceCountTotalChance => Singleton.resourceCountTotalChance;
    public static int[] ResourceCountChances => Singleton.resourceCountChances;

    public void Awake()
    {
        Singleton = this;
        if (storyPicker == null)
            storyPicker = new StoryPicker();
    }

    public void Reset()
    {
        Singleton = this;
    }

    public void Start()
    {
        Singleton = this;
        if (storyPicker == null)
            storyPicker = new StoryPicker();
    }

    public void OnDestroy()
    {
        Singleton = null;
    }
}
