using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    private Transform originalParent;
    public inventory myBag;

    private int currentID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        originalParent = transform.parent;
        currentID = originalParent.GetComponent<slot>().slotID;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //throw new System.NotImplementedException();
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        //throw new System.NotImplementedException();
        transform.position = eventData.position;
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject != null) {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Image")
            {
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                var temp = myBag.itemList[currentID];
                myBag.itemList[currentID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<slot>().slotID] = temp;

                //transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                //transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
                //Debug.Log("1");
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                var temp = myBag.itemList[currentID];
                myBag.itemList[currentID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInParent<slot>().slotID] = temp;
                //myBag.itemList[currentID] = null;
                //if (eventData.pointerPressRaycast.gameObject.GetComponent<slot>().slotID != currentID)
                //{
                //    myBag.itemList[currentID] = null;
                //}
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }

        //else
        //{

        //}
        transform.position = originalParent.position;
        transform.SetParent(originalParent);
        //transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        //transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        //throw new System.NotImplementedException();
        //myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<slot>().slotID] = myBag.itemList[currentID];
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.transform.parent);
        //if (eventData.pointerPressRaycast.gameObject.GetComponent<slot>().slotID != currentID)
        //{
        //    myBag.itemList[currentID] = null;
        //}
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log("1");


    }


}
