using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayAgainPanel : MonoBehaviour
{
    public Image _losserAndPlayAgainPanel;
    public Image _MoveLogicAlertPanel;

    public static int _playAgainFlag = 0;
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
        _MoveLogicAlertPanel.gameObject.SetActive(true);

        _playAgainFlag = mgr._redKilledChess.Count + 1;

/*
        for (int i = 0; i < 16; i++)
        {
            mgr._redKilledChess.Add(0);
            mgr._blackKilledChess.Add(0);
        }


        // clear killChess board
        // display red killed chess board
        /*
        for (int i = 0; i < mgr._redKilledChess.Count; i++)
        {
            GameObject gameObject = GameObject.Find((i + 116) + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            spr.sprite = null;
       }

        // display black killed chess board
        for (int i = 0; i < mgr._blackKilledChess.Count; i++)
        {
            GameObject gameObject = GameObject.Find((i + 100) + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            spr.sprite = null;
       }
       */
    }

    public void DontPlayAgainBtnOnClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}



















