using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    //public UnityEvent OnTriggerStayEvent;
    //public UnityEvent OnTriggerEnterEvent;
    //public static float giveTime = 0.1f;

    public event Action OnPlayerEnterTrigger;
    public event Action OnPlayerStayTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke();
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerStayTrigger?.Invoke();
        }
    }

}
