using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLine
{
    public DialogLineAvatar Avatar;
    public string Name;
    public string Message;

    public DialogLine(string name, string message)
    {
        Avatar = DialogLineAvatar.None;
        Name = name;
        Message = message;
    }
    
    public DialogLine(DialogLineAvatar avatar, string name, string message)
    {
        Avatar = avatar;
        Name = name;
        Message = message;
    }    
}
