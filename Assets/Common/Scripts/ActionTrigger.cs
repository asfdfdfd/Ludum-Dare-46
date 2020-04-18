using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;
    
    public void Trigger()
    {
        if (triggerEvent != null) {
            triggerEvent.Invoke();
        }
    }
}
