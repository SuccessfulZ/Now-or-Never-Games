using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStashMessages
{
    void ApproachedHouse();
    void LeftHouse();
    bool RefillResource(ResourceBundle putPackage);
    bool RemoveResource(ResourceBundle takePackage);
    void ContinueStory();
}
