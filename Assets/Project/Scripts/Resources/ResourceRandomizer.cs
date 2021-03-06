﻿using System.Collections;
using System.Linq;
using UnityEngine;

public class ResourceRandomizer : MonoBehaviour
{
    public GameObject[] housePoints;
    public GameObject housePointPrefab;
    public int housesPerRound = 5;

    private static ResourceType[] _allResourceTypes = new ResourceType[]
    {
        ResourceType.Food,
        ResourceType.Mail,
        ResourceType.VideoGames,
    };

    public void Start()
    {
        StartCoroutine(nameof(SpawnContinuous));
    }

    IEnumerator SpawnContinuous()
    {
        GenerateSpots(GlobalConstants.Player.position, housesPerRound);
        float nextWait = Random.Range(GlobalConstants.SpawnHouseTimerMin, GlobalConstants.SpawnHouseTimerMax);
        Debug.Log($"Spawned {housesPerRound}. Next spawn in {nextWait}s");
        yield return new WaitForSeconds(nextWait);

        while (GlobalConstants.CountdownControl.TimeLeft > 0f)
        {
            nextWait = Random.Range(GlobalConstants.SpawnHouseTimerMin, GlobalConstants.SpawnHouseTimerMax);
            if (GlobalConstants.SpawnHouseTimer)
            {
                GenerateSpots(GlobalConstants.Player.position, 1);
                Debug.Log($"Spawned 1. Next spawn in {nextWait}s");
            }
            yield return new WaitForSeconds(nextWait);
        }
    }

    public void GenerateSpots(Vector3 playerPos, int count)
    {
        var missingHouses = housePoints
            .Where(x => x.GetComponent<HouseSpot>() == null)
            .Where(x => Vector3.Distance(x.transform.position, playerPos) <= GlobalConstants.HouseSpawnRadius)
            .OrderBy(x => Random.value)
            .Take(count)
            .ToList();

        foreach (var go in missingHouses) GenerateHouseSpot(go);
    }

    public void GenerateHouseSpot(GameObject house)
    {
        HouseSpot spot = house.GetComponent<HouseSpot>();
        if (spot != null)
        {
            Destroy(spot);
            if (house.transform.GetChild(0).name.IndexOf(housePointPrefab.name) != -1)
            {
                Destroy(house.transform.GetChild(0).gameObject);
            }
        }
        spot = house.AddComponent<HouseSpot>();
        spot.requests = GenerateRandomResourceTypes();
        GameObject pref = Instantiate(housePointPrefab, house.transform, false);
    }

    public ResourceBundle[] GenerateRandomResourceTypes()
    {
        int chance = Random.Range(0, GlobalConstants.ResourceCountTotalChance) + 1;
        int count = 1;
        for (int i = GlobalConstants.ResourceCountChances.Length - 1; i >= 0; --i)
        {
            if (chance >= GlobalConstants.ResourceCountChances[i])
            {
                count = i + 1;
                break;
            }
        }

        return _allResourceTypes
            .OrderBy(x => Random.value)
            .Take(count)
            .Select(x => new ResourceBundle()
            {
                type = x,
                count = GlobalConstants.ResourceRemove,
            })
            .ToArray();
    }
}
