using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public CinematicManager cinematicManager;

    public TMP_Text textMeshTitle;
    public TMP_Text textMeshText;

    private bool isDialogInProgress;

    void Awake()
    {
        Hide();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator Show(List<DialogLine> dialogLines)
    {
        cinematicManager.StartCinematic();

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

        cinematicManager.StopCinematic();
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
