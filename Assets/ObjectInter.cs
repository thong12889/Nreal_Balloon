using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NRKernal;

public class ObjectInter : MonoBehaviour, IPointerClickHandler
{
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        /*if(NRInput.GetButtonDown(ControllerButton.TRIGGER))
        {
            transform.localScale *= 2;
        }*/
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale *= 0.5f;
    }
}
