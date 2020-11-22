using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStash : MonoBehaviour, IPlayerStashMessages
{
    public List<ResourceBundle> stash;
    public ResourceBundle[] maxResources;

    void Start()
    {
        
    }

    public void ApproachedHouse()
    {

    }

    public void LeftHouse()
    {

    }

    public bool RefillResource(ResourceBundle putPackage)
    {
        int maxItemsToFill = GlobalConstants.MaxOfResource;
        // Get possible max items in stash
        ResourceBundle filled = maxResources.FirstOrDefault(x => x.type == putPackage.type);
        if (filled != null) maxItemsToFill = filled.count;

        ResourceBundle current = stash.FirstOrDefault(x => x.type == putPackage.type);
        if (current == null)
        {
            current = new ResourceBundle()
            {
                count = 0,
                type = putPackage.type,
            };
            stash.Add(current);
        }
        // Refill resource in stash
        if (current.count < maxItemsToFill)
        {
            current.count += putPackage.count;
            return true;
        }
        return false;
    }

    public bool RemoveResource(ResourceBundle takePackage)
    {
        ResourceBundle current = stash.FirstOrDefault(x => x.type == takePackage.type);
        if (current == null)
        {
            current = new ResourceBundle()
            {
                count = 0,
                type = takePackage.type,
            };
            stash.Add(current);
        }

        if (current.count >= takePackage.count)
        {
            current.count -= takePackage.count;
            return true;
        }
        return false;
    }

    public void ContinueStory()
    {
        
    }
}
