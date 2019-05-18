using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainPanel : MonoBehaviour
{
    public Image _losserAndPlayAgainPanel;
    public Image _redFirstAlertPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgainBtnOnClick()
    {
        int[] arrPosDefault = { 0,
                                25, 23, 21, 19, 17, 18, 20, 22, 24,
                                0, 0, 0, 0, 0, 0, 0, 0, 0,
                                0, 27, 0, 0, 0, 0, 0, 26, 0,
                                32, 0, 31, 0, 30, 0, 29, 0,
                                28, 0, 0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0, 0, 0, 0, 0, 0, 0,
                                0, 16, 0, 15, 0, 14, 0, 13, 0,
                                12, 0, 11, 0, 0, 0, 0, 0, 10,
                                0, 0, 0, 0, 0, 0, 0, 0, 0,
                                0, 9, 7, 5, 3, 1, 2, 4, 6, 8};

        mgr._arrPos = arrPosDefault;
        pos._teamFlag = false;
        _losserAndPlayAgainPanel.gameObject.SetActive(false);
        _redFirstAlertPanel.gameObject.SetActive(true);
    }

    public void DontPlayAgainBtnOnClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
