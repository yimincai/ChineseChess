﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mgr : MonoBehaviour
{
    public static int ind = -1;
    // flag
    public static bool flag = true;
    private string[] res = { "none", //null
                             "black_jiang", //1_黑棋_將
                             "black_shr", //2_黑棋_士_左
                             "black_shr", //3_黑棋_士_右
                             "black_shiang", //4_黑棋_象_左
                             "black_shiang", //5_黑棋_象_右
                             "black_ma", //6_黑棋_馬_左
                             "black_ma", //7_黑棋_馬_右
                             "black_che", //8_黑棋_車_左
                             "black_che", //9_黑棋_車_右
                             "black_pau", //10_黑棋_砲_左
                             "black_pau", //11_黑棋_砲_右
                             "black_tzu", //12_黑棋_卒_1
                             "black_tzu", //13_黑棋_卒_2
                             "black_tzu", //14_黑棋_卒_3
                             "black_tzu", //15_黑棋_卒_4
                             "black_tzu", //16_黑棋_卒_5
                             "red_shuo", //17_紅棋_帥
                             "red_shr", //18_紅棋_士
                             "red_shr", //19_紅棋_士
                             "red_shiang", //20_紅棋_象
                             "red_shiang", //21_紅棋_象
                             "red_ma", //22_紅棋_馬
                             "red_ma", //23_紅棋_馬
                             "red_che", //24_紅棋_車
                             "red_che", //25_紅棋_車
                             "red_pau", //26_紅棋_砲
                             "red_pau", //27_紅棋_砲
                             "red_bing", //28_紅棋_兵_1
                             "red_bing", //29_紅棋_兵_2
                             "red_bing", //30_紅棋_兵_3
                             "red_bing", //31_紅棋_兵_4
                             "red_bing", //32_紅棋_兵_5
                           };
    public static int[] arrPos = { 0,
                                   8, 6, 4, 2, 1, 3, 5, 7, 9,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   0, 10, 0, 0, 0, 0, 0, 11, 0,
                                   12, 0, 13, 0, 14, 0, 15, 0, 16,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   28, 0, 29, 0, 30, 0, 31, 0, 32,
                                   0, 26, 0, 0, 0, 0, 0, 26, 0,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   24, 22, 20, 18, 17, 19, 21, 23, 25};
    void Start()
    {
        Debug.Log("mgr.cs");
        Debug.Log("==========");
        //GameObject.Instantiate(checkerboard);


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < arrPos.Length; i++)
        {
            GameObject gameObject = GameObject.Find(i + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            if (arrPos[i] == 0)
            {
                spr.sprite = null;
            }
            else
            {
                Sprite[] playerSprite = Resources.LoadAll<Sprite>(res[arrPos[i]]);
                spr.sprite = playerSprite[0];
            }
        }
    }
    static public void move(int target)
    {
        Debug.Log("mgr.cs ind:" + ind + " target:" + target);
        arrPos[target] = arrPos[ind];
        arrPos[ind] = 0;
        flag = true;
    }
}
