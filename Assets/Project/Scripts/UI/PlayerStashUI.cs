using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStashUI : MonoBehaviour, IPlayerStashMessages
{
    public Text housePromptText;

    private Transform player;

    public void ApproachedHouse()
    {
        if (housePromptText != null)
            housePromptText.gameObject.SetActive(true);
    }

    public void LeftHouse()
    {
        if (housePromptText != null)
            housePromptText.gameObject.SetActive(false);
    }

    public bool RefillResource(ResourceBundle putPackage)
    {
        UpdateCounter(putPackage);
        return true;
    }

    public bool RemoveResource(ResourceBundle takePackage)
    {
        UpdateCounter(takePackage);
        var picker = GlobalConstants.StoryPicker;
        if (picker != null)
        {
            Story story = picker.PickStory();
            GlobalConstants.SceneManager.DialogueShow(story);
        }
        return true;
    }

    private void UpdateCounter(ResourceBundle bundle)
    {
        var stashScript = player.GetComponent<PlayerStash>();
        
        int currentCount = 0;
        if (stashScript.stash != null && stashScript.stash.Count != 0)
        {
            var res = stashScript.stash.Find(x => x.type == bundle.type);
            if (res != null) currentCount = res.count;
        }

        int maxCount = GlobalConstants.MaxOfResource;
        if (stashScript.maxResources != null && stashScript.maxResources.Length != 0)
        {
            var res = stashScript.maxResources.FirstOrDefault(x => x.type == bundle.type);
            if (res != null) maxCount = res.count;
        }

        switch (bundle.type)
        {
            case ResourceType.Mail:
                if (GlobalConstants.SceneManager?.mailFiller != null)
                    GlobalConstants.SceneManager.mailFiller.fillAmount = (float)currentCount / maxCount;
                break;
            case ResourceType.Food:
                if (GlobalConstants.SceneManager?.foodFiller != null)
                    GlobalConstants.SceneManager.foodFiller.fillAmount = (float)currentCount / maxCount;
                break;
            case ResourceType.VideoGames:
                if (GlobalConstants.SceneManager?.vgFiller != null)
                    GlobalConstants.SceneManager.vgFiller.fillAmount = (float)currentCount / maxCount;
                break;
        }
    }

    public void Start()
    {
        player = GlobalConstants.Player;
        UpdateCounter(new ResourceBundle() { type = ResourceType.Mail });
        UpdateCounter(new ResourceBundle() { type = ResourceType.Food });
        UpdateCounter(new ResourceBundle() { type = ResourceType.VideoGames });
    }

    void Update()
    {
        if (HousesWithinRadius())
            player.SendMessage(nameof(ApproachedHouse));
        else
            player.SendMessage(nameof(LeftHouse));
    }

    private bool HousesWithinRadius()
    {
        if (player == null) return false;
        var resourceRandomizer = GlobalConstants.ResourceRandomizer;
        if (resourceRandomizer == null) return false;

        IEnumerable<GameObject> houses = resourceRandomizer.housePoints;
        if (houses == null) return false;
        houses = houses.Where(x => x.GetComponent<HouseSpot>() != null);

        return houses.Select(x => x.GetComponent<HouseSpot>()).Any(x => x.CheckPlayer(player));
    }

    public void ContinueStory()
    {
        if (GlobalConstants.SpawnHouseAfterStory)
        {
            int availableHouses = GlobalConstants.ResourceRandomizer.housePoints
                .Count(x => x.GetComponent<HouseSpot>() != null);
            if (availableHouses == 0)
                GlobalConstants.ResourceRandomizer.GenerateSpots(player.position, 1);
        }
    }
}
