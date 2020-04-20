using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text buttonPlay;
    public TMP_Text buttonExit;

    private int _selectedButton = 0;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_selectedButton == 0)
        {
            buttonPlay.color = new Color(226 / 255.0f, 168 / 255.0f, 50 / 255.0f);
            buttonExit.color = new Color(156 / 255.0f, 71 / 255.0f, 14 / 255.0f);
        }
        else
        {
            buttonPlay.color = new Color(156 / 255.0f, 71 / 255.0f, 14 / 255.0f);
            buttonExit.color = new Color(226 / 255.0f, 168 / 255.0f, 50 / 255.0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _selectedButton++;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _selectedButton--;
        }

        if (_selectedButton < 0)
        {
            _selectedButton = 1;
        }

        if (_selectedButton > 1)
        {
            _selectedButton = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_selectedButton == 0)
            {
                GameState.Instance.Reset();
                SceneManager.LoadScene("Scene01");
            }
            else
            {
                Application.Quit();
            }
        }        
    }
}
