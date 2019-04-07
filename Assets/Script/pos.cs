using System;
using System.Linq;
using UnityEngine;

public class pos : MonoBehaviour
{
    // 要移動的物件位置(在arrPos中的代號)
    public int ind;

    // 移動的物件名稱
    public static string _selectedName;

    // red team first, false means red turn.
    public static bool _teamFlag = false;

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
        _selectedName = SelectedChessName(ind);
        // Debug.Log("selected Name : " + selectedName);

        // Debug.Log("pos.cs :" + ind);
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
            else // move
            {
                if(SwitchGamer(_selectedName, _teamFlag) == true)
                {
                    GameLogic(_selectedName, ind, mgr.ind);

                    if(mgr.ind != ind)
                    {
                        mgr.move(ind); //target 
                    }
                }
            }
        }

        Debug.Log("=============================================");

    }

    // GameLogic
    // mgr.ind --> 要移動的物件
    // target  --> 移動的目標點
    public static void GameLogic(string selectedChessName, int target, int ind)
    {
        // Debug.Log("target = " + target + ", ind = " + ind);
        if (selectedChessName.Contains("bing"))
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

        if (selectedChessName.Contains("tzu"))
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
        if (selectedChessName.Contains("pau"))
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

        //先做十字移動，之後再考慮碰撞吃子
        //TODO: 若有障礙物，要擋住。
        if (selectedChessName.Contains("che"))
        {
            bool movable = false;
            int gap = target - ind;
            int flag = 0;
            if (gap > 9 && gap % 9 == 0) // 向上移動
            {
                for (int i = ind + 9; i < target; i = i + 9)
                {
                    if (mgr.arrPos[i] != 0)
                    {
                        flag++;
                    }
                }
            }
            /* else if(gap < 9 && gap % 9 == 0) //往下移動
             {
                 for (int i = ind; i < target; i = i + 9)
                 {
                     if (mgr.arrPos[i] != 0)
                     {
                         flag++;
                     }
                 }
             }*/

            /*if (gap > 9 && gap % 9 == 0) // 向左移動
            {
                for (int i = ind + 9; i < target; i = i + 9)
                {
                    if (mgr.arrPos[i] != 0)
                    {
                        flag++;
                    }
                }
            }
            */

            if (flag > 0)
                movable = false;
            else
                movable = true;

            bool indLeftRowEdge = ((1 - (ind % 9)) + ind) <= target;
            bool indRightRowEdge = target <= ((9 - (ind % 9)) + ind);

            if (movable)
            {
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
            else
            {
                mgr.ind = target;
                // don't move
            }
        }

        if (selectedChessName.Contains("ma"))
        {
            var gap = target - ind;

            if (((gap == 17 || gap == 19)) && (mgr.arrPos[ind + 9] == 0))
            {
                mgr.ind = ind; // move
            }
            else if (((gap == -17) || (gap == -19)) && (mgr.arrPos[ind - 9] == 0))
            {
                mgr.ind = ind; // move
            }
            else if (((gap == -7) || (gap == 11)) && (mgr.arrPos[ind + 1] == 0))
            {
                mgr.ind = ind; // move
            }
            else if (((gap == 7) || (gap == -11)) && (mgr.arrPos[ind - 1] == 0))
            {
                mgr.ind = ind; // move
            }
            else
            {
                mgr.ind = target; // dont move
            }
        }

        if (selectedChessName.Contains("shiang"))
        {
            var gap = target - ind;
            if ((target < 45) && selectedChessName.Contains("red"))
            {
                if ((gap == 20) && (mgr.arrPos[ind + 10] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == 16) && (mgr.arrPos[ind + 8] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == -20) && (mgr.arrPos[ind - 10] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == -16) && (mgr.arrPos[ind - 8] == 0))
                {
                    mgr.ind = ind; // move
                }
                else
                {
                    mgr.ind = target; // dont move
                }
            }
            else if ((target > 45) && selectedChessName.Contains("black"))
            {
                if ((gap == 20) && (mgr.arrPos[ind + 10] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == 16) && (mgr.arrPos[ind + 8] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == -20) && (mgr.arrPos[ind - 10] == 0))
                {
                    mgr.ind = ind; // move
                }
                else if ((gap == -16) && (mgr.arrPos[ind - 8] == 0))
                {
                    mgr.ind = ind; // move
                }
                else
                {
                    mgr.ind = target; // dont move
                }
            }
            else
            {
                mgr.ind = target; // dont move
            }
        }

        if (selectedChessName.Contains("shr"))
        {
            var gap = Math.Abs(target - ind);
            int[] redShrArray = { 4, 6, 14, 22, 24 };
            int[] blackShrArray = { 67, 69, 77, 85, 87 };
            bool movable = false;

            if (selectedChessName.Contains("red"))
            {
                movable = redShrArray.Contains(target) && redShrArray.Contains(ind);
            }
            else
            {
                movable = blackShrArray.Contains(target) && blackShrArray.Contains(ind);
            }

            if (movable && (gap == 10 || gap == 8))
                mgr.ind = ind; // move
            else
                mgr.ind = target; // dont move
        }

        if (selectedChessName.Contains("jiang"))
        {
            int[] jiangArray = { 67, 68, 69, 76, 77, 78, 85, 86, 87 };
            var gap = Math.Abs(target - ind);

            bool movable = jiangArray.Contains(target) && jiangArray.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
                mgr.ind = ind; // move
            else
                mgr.ind = target; // dont move
        }

        if (selectedChessName.Contains("shuo"))
        {
            int[] shuoArray = { 4, 5, 6, 13, 14, 15, 22, 23, 24 };
            var gap = Math.Abs(target - ind);

            bool movable = shuoArray.Contains(target) && shuoArray.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
                mgr.ind = ind; // move
            else
                mgr.ind = target; // dont move
        }
    }

    // 回傳點選的棋子名稱
    public static string SelectedChessName(int index)
    {
        int boardPosNum = mgr.arrPos[index];

        if (boardPosNum == 0)
        {
            return _selectedName;
        }
        String chessName = mgr.res[boardPosNum];
        return chessName;
    }

    public static bool SwitchGamer(string selectedName, bool teamFlagk)
    {
        if ((_selectedName.Contains("red") && _teamFlag == false) || (_selectedName.Contains("black") && _teamFlag == true))
        {
            if (_teamFlag == false)
            {
                Debug.Log("Change to black turn");
                return true;
            }
            else if (_teamFlag == true)
            {
                Debug.Log("Change to red turn");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}






