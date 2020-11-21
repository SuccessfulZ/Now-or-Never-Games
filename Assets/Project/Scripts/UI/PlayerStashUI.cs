using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStashUI : MonoBehaviour, IPlayerStashMessages
{
    public Text housePromptText;

    private Transform player;

    public void Start()
    {
        player = GlobalConstants.Player;
    }

    public void ApproachedHouse()
    {
        housePromptText.gameObject.SetActive(true);
    }

    public void LeftHouse()
    {
        housePromptText.gameObject.SetActive(false);
    }

    public bool RefillResource(ResourceBundle putPackage)
    {
        // TODO: update counter
        return true;
    }

    public bool RemoveResource(ResourceBundle takePackage)
    {
        // TODO: update counter
        // TODO: show story from StoryPicker
        return true;
    }

    void Update()
    {
        if (HousesWithinRadius())
            player.SendMessage(nameof(ApproachedHouse));
        else
            player.SendMessage(nameof(LeftHouse));
    }

    bool HousesWithinRadius()
    {
        if (player == null) return false;
        var resourceRandomizer = GlobalConstants.ResourceRandomizer;
        if (resourceRandomizer == null) return false;

        IEnumerable<GameObject> houses = resourceRandomizer.housePoints;
        if (houses == null) return false;
        houses = houses.Where(x => x.GetComponent<HouseSpot>() != null);

        return houses.Select(x => x.GetComponent<HouseSpot>()).Any(x => x.CheckPlayer(player));
    }
}
