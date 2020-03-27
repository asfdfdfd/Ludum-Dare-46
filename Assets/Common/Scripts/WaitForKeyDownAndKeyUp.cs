using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForKeyDownAndKeyUp : IEnumerator
{
    private bool isAwaitingKeyDown = true;
    private bool isAwaitingKeyUp = false;

    private CustomYieldInstruction current;

    private WaitForKeyDown waitForKeyDown;
    
    private WaitForKeyUp waitForKeyUp;

    private KeyCode keyCode;

    public WaitForKeyDownAndKeyUp(KeyCode keyCode)
    {
        waitForKeyDown = new WaitForKeyDown(keyCode);
        current = waitForKeyDown;

        waitForKeyUp = new WaitForKeyUp(keyCode);
    }

    public object Current { get { return current; } }

    public bool MoveNext() 
    { 
        if (current.keepWaiting)
        {
            return true;
        }

        if (current == waitForKeyDown)
        {
            current = waitForKeyUp;
            return true;
        }

        return false;
    }

    public void Reset() {}      
}
