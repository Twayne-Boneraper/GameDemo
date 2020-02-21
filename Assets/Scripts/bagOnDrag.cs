using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bagOnDrag : MonoBehaviour, IDragHandler
{

    public RectTransform currentTransform;

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //transform.position = eventData.position;
        currentTransform.anchoredPosition += eventData.delta;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        limitBagWindow();
    }

    void limitBagWindow()
    {
        if (currentTransform.anchoredPosition.x < -240)
        {
            currentTransform.anchoredPosition = new Vector3(-240, currentTransform.anchoredPosition.y);
        }
        if (currentTransform.anchoredPosition.x > 240)
        {
            currentTransform.anchoredPosition = new Vector3(240, currentTransform.anchoredPosition.y);
        }
        if (currentTransform.anchoredPosition.y > 110)
        {
            currentTransform.anchoredPosition = new Vector3(currentTransform.anchoredPosition.x, 110);
        }
        if (currentTransform.anchoredPosition.y < -110)
        {
            currentTransform.anchoredPosition = new Vector3(currentTransform.anchoredPosition.x, -110);
        }

    }

}


