using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public TMP_Text textMeshTitle;
    public TMP_Text textMeshText;

    private bool isDialogInProgress;

    void Awake()
    {
        Hide();
    }

    public IEnumerator Show(List<DialogLine> dialogLines)
    {
        gameObject.SetActive(true);

        isDialogInProgress = true;

        // TODO: Hack to force skip to the next frame to not register keyboard press.
        yield return null;
        foreach (DialogLine dialogLine in dialogLines)
        {
            textMeshTitle.SetText(dialogLine.Name);
            textMeshText.SetText(dialogLine.Message);
            yield return new WaitForKeyDownAndKeyUp(KeyCode.Space);
        }

        isDialogInProgress = false;

        gameObject.SetActive(false);
    }

    public IEnumerator Show(DialogLine dialogLine)
    {
        return Show(new List<DialogLine>() { dialogLine });
    }

    public IEnumerator Show(string name, string message)
    {
        return Show(new DialogLine(name, message));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsDialogInProgress
    {
        get
        {
            return isDialogInProgress;
        }
    }
}
