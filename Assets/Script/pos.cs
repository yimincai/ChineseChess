﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pos : MonoBehaviour
{
    // 要移動的物件位置(在arrPos中的代號)
    public int ind;
    // 移動的物件名稱
    public static string selectedName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Controller
    // 判斷你是要選棋子 還是要讓棋子移flag=true 點sprite(棋子)
    // 進else如果第二次點的地方還是棋子，就換選另一個 
    // 這裡應該還要把陣營考慮進去，黑不能到黑，但黑可以到紅(後續工作)
    // 如果第二次點的位置是空的就走move()
    void OnMouseUp()
    {

        // 取得物件名稱
        selectedName = SelectedChessName(ind);
        //Debug.Log("selected Name : " + selectedName);

        //Debug.Log("pos.cs :" + ind);
        if (mgr.flag)
        {
            if (mgr.arrPos[ind] > 0)
            {
                mgr.ind = ind;
                mgr.flag = false;
            }
        }
        else
        {
            if (mgr.arrPos[ind] > 0)
            {
                mgr.ind = ind;
            }
            else
            {
                //Debug.Log("================ind 要移動的個體位置 : " + mgr.ind);
                //Debug.Log("================mgr 要移動到的目標點 : " + ind);
                GameLogic(selectedName, ind, mgr.ind);
                mgr.move(ind); //target
            }
        }

        Debug.Log("=============================================");

    }

    // GameLogic
    // mgr.ind --> 要移動的物件
    // target  --> 移動的目標點
    public static void GameLogic(string selectedChessName, int target, int ind)
    {
        Debug.Log("target = " + target + ", ind = " + ind);
        if (selectedChessName.Contains("black_tzu"))
        {
            if (ind > 45 && Math.Abs(target - ind) == 1 || target - ind == 9)
            {
                //Debug.Log("要移動的目標在ind = " + ind);
                mgr.ind = ind; // move
            }
            else
            {
                //Debug.Log("target - ind == " + target + " - " + ind + " =" + (target - ind));
                if (target - ind == 9)
                {
                    //Debug.Log("要移動的目標在ind = " + ind);
                    mgr.ind = ind; // move
                }
                else
                {
                    //Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr.ind = target; // don't move
                }
            }
        }

        if (selectedChessName.Contains("red_bing"))
        {
            if (ind <= 45 && Math.Abs(ind - target) == 1 || ind - target == 9)
            {
                //Debug.Log("要移動的目標在ind = " + ind);
                mgr.ind = ind; // move
            }
            else
            {
                //Debug.Log(" ind -target == " + target + " - " + ind + " =" + (ind - target));
                if (ind - target == 9)
                {
                    //Debug.Log("要移動的目標在ind = " + ind);
                    mgr.ind = ind; // move
                }
                else
                {
                    //Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr.ind = target; // don't move
                }
            }
        }

        //先做十字移動，之後再考慮碰撞吃子
        if (selectedChessName.Contains("black_pau"))
        {
            bool indLeftRowEdge = ((1 - (ind % 9)) + ind) <= target;
            bool indRightRowEdge = target <= ((9 - (ind % 9)) + ind);

            if ((target - ind) % 9 == 0 || indLeftRowEdge && indRightRowEdge)
            {
                mgr.ind = ind; // move
            }
            else if (ind % 9 == 0 && Math.Abs(target - ind) < 9)
            {
                mgr.ind = ind; // move
            }
            else
            {
                mgr.ind = target;
                // don't move
            }
        }

        if (selectedChessName.Contains("red_pau"))
        {
            bool indLeftRowEdge = ((1 - (ind % 9)) + ind) <= target;
            bool indRightRowEdge = target <= ((9 - (ind % 9)) + ind);

            if ((target - ind) % 9 == 0 || indLeftRowEdge && indRightRowEdge)
            {
                mgr.ind = ind; // move
            }
            else if (ind % 9 == 0 && Math.Abs(target - ind) < 9)
            {
                mgr.ind = ind; // move
            }
            else
            {
                mgr.ind = target;
                // don't move
            }
        }

        if (selectedChessName.Contains("shuo"))
        {
            int[] shuo_array = { 67, 68, 69, 76, 77, 78, 85, 86, 87 };
            var gap = Math.Abs(target - ind);

            Boolean movable = shuo_array.Contains(target) && shuo_array.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
                mgr.ind = ind; // move
            else
                mgr.ind = target; // dont move
        }

        if (selectedChessName.Contains("jiang"))
        {
            int[] jiang_array = { 4, 5, 6, 13, 14, 15, 22, 23, 24 };
            var gap = Math.Abs(target - ind);

            Boolean movable = jiang_array.Contains(target) && jiang_array.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
                mgr.ind = ind; // move
            else
                mgr.ind = target; // dont move
        }

        if (selectedChessName.Contains("ma"))
        {
            var gap = target - ind;
            //Debug.Log("@tag - ind = " + gap);

            if (((gap == 17 || gap == 19)) && (mgr.arrPos[ind + 9] == 0))
            {
                Debug.Log("@arrPos + 9 = " + mgr.arrPos[ind + 9]);
                mgr.ind = ind; // move

            }
            else if (((gap == -17) || (gap == -19)) && (mgr.arrPos[ind - 9] == 0))
            {
                Debug.Log("@arrPos - 9 = " + mgr.arrPos[ind - 9]);
                mgr.ind = ind; // move

            }
            else if (((gap == -7) || (gap == 11)) && (mgr.arrPos[ind + 1] == 0))
            {
                Debug.Log("@arrPos - 1 = " + mgr.arrPos[ind + 1]);
                mgr.ind = ind; // move

            }
            else if (((gap == 7) || (gap == -11)) && (mgr.arrPos[ind - 1] == 0))
            {
                Debug.Log("@arrPos + 1 = " + mgr.arrPos[ind - 1]);
                mgr.ind = ind; // move

            }
            else
            {
                mgr.ind = target; // dont move
            }
        }
    }
    // 回傳點選的棋子名稱
    public static string SelectedChessName(int index)
    {
        int boardPosNum = mgr.arrPos[index];

        if (boardPosNum == 0)
        {
            return selectedName;
        }
        String chessName = mgr.res[boardPosNum];
        return chessName;
    }
}

