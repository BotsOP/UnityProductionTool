using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform currentTransform;
    private GameObject mainContent;
    private Vector3 currentPossition;

    private int totalChild;

    public void OnPointerDown(PointerEventData eventData)
    {
        //transform.SetParent(currentTransform.parent.parent.transform);
        currentPossition = currentTransform.position;
        mainContent = currentTransform.parent.gameObject;
        totalChild = mainContent.transform.childCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentTransform.position =
            new Vector3(currentTransform.position.x, eventData.position.y, currentTransform.position.z);

        for (int i = 0; i < totalChild; i++)
        {
            if (i != currentTransform.GetSiblingIndex())
            {
                Transform otherTransform = mainContent.transform.GetChild(i);
                int distance = (int) Vector3.Distance(currentTransform.position,
                    otherTransform.position);
                if (distance <= 20 && otherTransform.CompareTag("Mask"))
                {
                    Debug.Log("doing nothing   " + distance);
                }
                
                if (distance <= 1)
                {
                    Vector3 otherTransformOldPosition = otherTransform.position;
                    otherTransform.position = new Vector3(otherTransform.position.x, currentPossition.y,
                        otherTransform.position.z);
                    currentTransform.position = new Vector3(currentTransform.position.x, otherTransformOldPosition.y,
                        currentTransform.position.z);
                    currentTransform.SetSiblingIndex(otherTransform.GetSiblingIndex());
                    currentPossition = currentTransform.position;
                }
            }
        }
        
        //updates the vertical layout group incase there are different sized elements
        // mainContent.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        // mainContent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //UI doesnt get properly updated and this is the only the fix that works so ¯\_(ツ)_/¯
        RectTransform rectTranform = GetComponent<RectTransform>();
        rectTranform.sizeDelta = new Vector2(rectTranform.sizeDelta.x, rectTranform.sizeDelta.y - 0.001f);
        rectTranform.sizeDelta = new Vector2(rectTranform.sizeDelta.x, rectTranform.sizeDelta.y + 0.001f);

        currentTransform.position = currentPossition;
        EventSystem.RaiseEvent(EventType.UPDATE_RENDERER);
    }
}