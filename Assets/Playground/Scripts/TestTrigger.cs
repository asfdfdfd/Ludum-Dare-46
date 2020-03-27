using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public DialogController dialogController;

    void OnTriggerEnter2D(Collider2D other) {
        var dialogLines = new List<DialogLine>();
        dialogLines.Add(new DialogLine() { Name = "Хуй", Message = "Хуй" });
    
        StartCoroutine(dialogController.Show(dialogLines));
    }    
}
