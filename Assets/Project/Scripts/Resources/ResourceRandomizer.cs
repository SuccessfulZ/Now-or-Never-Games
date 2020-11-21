using System.Linq;
using UnityEngine;

public class ResourceRandomizer : MonoBehaviour
{
    public GameObject[] housePoints;
    public int housesPerRound = 5;

    private static ResourceType[] _allResourceTypes = new ResourceType[]
    {
        ResourceType.Food,
        ResourceType.Mail,
        ResourceType.VideoGames,
    };

    public void Start()
    {
        GenerateSpots(GlobalConstants.Player.position, housesPerRound);
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
        if (spot != null) Destroy(spot);
        spot = house.AddComponent<HouseSpot>();
        spot.requests = GenerateRandomResourceTypes();
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
