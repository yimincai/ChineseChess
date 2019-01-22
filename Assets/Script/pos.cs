using System;
using System.Collections;
using System.Collections.Generic;
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
                Debug.Log("chess dont move");
            }
            else
            {
                //Debug.Log("================ind 要移動的個體位置 : " + mgr.ind);
                //Debug.Log("================mgr 要移動到的目標點 : " + ind);
                GameLogic(selectedName, ind, mgr.ind);
                mgr.move(ind); //target
                Debug.Log("chess move");
                Debug.Log("============================================");
            }
        }

    }

    // GameLogic
    // mgr.ind --> 要移動的物件
    // target  --> 移動的目標點
    public static void GameLogic(string selectedChessName, int target, int ind)
    {
        Debug.Log("執行GL前target = " + target + "ind = " + ind);
        if (selectedChessName.Equals("black_tzu"))
        {
            if (ind > 45 && Math.Abs(target - ind) == 1 || target - ind == 9)
            {
                Debug.Log("要移動的目標在ind = " + ind);
                mgr.ind = ind; // move
            }
            else
            {
                Debug.Log("target - ind == " + target + " - " + ind + " =" + (target - ind));
                if (target - ind == 9)
                {
                    Debug.Log("要移動的目標在ind = " + ind);
                    mgr.ind = ind; // move
                }
                else
                {
                    Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr.ind = target; // don't move
                }
            }
        }

        if (selectedChessName.Equals("red_bing"))
        {
            if (ind < 45 && Math.Abs(ind - target) == 1 || ind - target == 9)
            {
                Debug.Log("要移動的目標在ind = " + ind);
                mgr.ind = ind; // move
            }
            else
            {
                Debug.Log(" ind -target == " + target + " - " + ind + " =" + (ind - target));
                if (ind - target == 9)
                {
                    Debug.Log("要移動的目標在ind = " + ind);
                    mgr.ind = ind; // move
                }
                else
                {
                    Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr.ind = target; // don't move
                }
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

