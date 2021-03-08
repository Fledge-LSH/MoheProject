using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
{

    //触发长按事件持续时间
    public float durationThreshold = 1.5f;
    //
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onClick = new UnityEvent();

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
         {
            if (Time.time - timePressStarted > durationThreshold)
             {
                    longPressTriggered = true;
                    onLongPress.Invoke();
              }
          }
    }

        public void OnPointerDown(PointerEventData eventData)
        {
            timePressStarted = Time.time;
            isPointerDown = true;
            longPressTriggered = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isPointerDown = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!longPressTriggered)
            {
                onClick.Invoke();
            }
        }
 }
