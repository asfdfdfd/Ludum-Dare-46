using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject dialogPanel;
    
    public TMP_Text textMeshTitle;
    public TMP_Text textMeshText;

    public Image imageAvatar;
    
    private bool isDialogInProgress;

    void Awake()
    {
        Hide();
    }

    private Sprite DialogLineAvatarToSprite(DialogLineAvatar dialogLineAvatar)
    {
        switch (dialogLineAvatar)
        {
            case DialogLineAvatar.None:
                return null;
            case DialogLineAvatar.Arthur:
                return Resources.Load<Sprite>("Avatar Arthur");
            case DialogLineAvatar.Lancelot:
                return Resources.Load<Sprite>("Avatar Lancelot");
            case DialogLineAvatar.Ghost:
                return Resources.Load<Sprite>("Avatar Ghost");
        }
        
        throw new Exception("Please handle new DialogLineAvatar");
    }
    
    public IEnumerator Show(List<DialogLine> dialogLines)
    {
        Show();

        isDialogInProgress = true;

        // TODO: Hack to force skip to the next frame to not register keyboard press.
        yield return null;
        foreach (DialogLine dialogLine in dialogLines)
        {
            var sprite = DialogLineAvatarToSprite(dialogLine.Avatar);
            
            if (sprite != null)
            {
                imageAvatar.sprite = sprite;
                imageAvatar.gameObject.SetActive(true);
            }
            else
            {
                imageAvatar.gameObject.SetActive(false);
            }

            textMeshTitle.SetText(dialogLine.Name);
            textMeshText.SetText(dialogLine.Message);
            yield return new WaitForKeyDownAndKeyUp(KeyCode.Space);
        }

        isDialogInProgress = false;

        Hide();
    }

    public IEnumerator Show(DialogLine dialogLine)
    {
        return Show(new List<DialogLine>() { dialogLine });
    }

    public IEnumerator Show(string name, string message)
    {
        return Show(new DialogLine(name, message));
    }
    
    public IEnumerator Show(DialogLineAvatar dialogLineAvatar, string name, string message)
    {
        return Show(new DialogLine(dialogLineAvatar, name, message));
    }    

    private void Show()
    {
        dialogPanel.SetActive(true);
    }

    private void Hide()
    {
        dialogPanel.SetActive(false);
    }

    public bool IsDialogInProgress
    {
        get
        {
            return isDialogInProgress;
        }
    }
}
