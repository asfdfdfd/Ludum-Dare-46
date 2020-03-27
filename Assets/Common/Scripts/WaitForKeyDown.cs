using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForKeyDown : CustomYieldInstruction
{
    private KeyCode keyCode;

    public WaitForKeyDown(KeyCode keyCode)
    {
        this.keyCode = keyCode;
    }

    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKeyDown(keyCode);
        }
    }
}
