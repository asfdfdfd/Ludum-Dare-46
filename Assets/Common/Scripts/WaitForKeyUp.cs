using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForKeyUp : CustomYieldInstruction
{
    private KeyCode keyCode;

    public WaitForKeyUp(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }

    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKeyUp(keyCode);
        }
    }
}
