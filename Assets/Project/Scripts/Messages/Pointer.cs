using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;

    private Vector3 targetPosition;
    private RectTransform pointerRecTransform;

    private void Awake()
    {
        targetPosition = new Vector3(200, 0, 45);
        pointerRecTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRecTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width-borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        //Debug.Log(isOffScreen + "  " + targetPositionScreenPoint);

        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRecTransform.position = pointerWorldPosition;
            pointerRecTransform.localPosition = new Vector3(pointerRecTransform.localPosition.x, pointerRecTransform.localPosition.y, pointerRecTransform.localPosition.z);
        
        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRecTransform.position = pointerWorldPosition;
            pointerRecTransform.localPosition = new Vector3(pointerRecTransform.localPosition.x, pointerRecTransform.localPosition.y, pointerRecTransform.localPosition.z);
        }
    
        
    }
}
