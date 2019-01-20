using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pos : MonoBehaviour
{
    public int ind;
    public static string selectedName;
    public static int selectedPos;
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

        selectedPos = ind;
        selectedName = SelectedChessName(ind);
        Debug.Log("selected Name : " + selectedName);

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
                GameLogic(selectedName, ind);
                mgr.move(ind); //target
                Debug.Log("chess move");
            }
        }

    }

    public static void GameLogic(string selectedChessName, int target)
    {
        if (selectedChessName.Equals("black_tzu"))
        {
        }
    }

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

