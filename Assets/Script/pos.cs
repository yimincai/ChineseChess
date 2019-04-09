using System;
using System.Linq;
using UnityEngine;

public class pos : MonoBehaviour
{
    // 要移動的物件位置(在arrPos中的代號)
    public int ind;

    // 第一個選擇的物件名稱
    public static string _firstSelectedName;

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

        // Debug.Log("pos.cs :" + ind);
        if (mgr._flag)
        {
            if (mgr._arrPos[ind] > 0)
            {
                if (PlayerControlTheCorrespondingChess(_selectedName))
                {
                    _firstSelectedName = SelectedChessName(ind);
                }

                // 點自己不移動
                mgr._ind = ind; 

                // 設回false 等待下次點擊mgr._flag變成true的時候，進到下面else的回條件式中
                mgr._flag = false; 
            }
        }
        else
        {
            if (mgr._arrPos[ind] > 0)
            {
                // 判斷現在操作者是哪一方，並操作相應的棋子
                if (PlayerControlTheCorrespondingChess(_selectedName))
                {
                    _firstSelectedName = SelectedChessName(ind);
                }

                if (MoveLogic(_firstSelectedName, ind, mgr._ind) 
                    && KillChessLogic(ind, mgr._ind) 
                    && PlayerControlTheCorrespondingFirstChess(_firstSelectedName)
                    && (SelectedChessChange(ind) == true))
                {
                    // 殺棋
                    mgr._arrPos[ind] = 0;

                    SwitchPlayer(_teamFlag);
                    mgr.move(ind);
                }
                else
                {
                    mgr._ind = ind;
                }
            }
            else
            {

                if (PlayerControlTheCorrespondingChess(_selectedName))
                {
                    MoveLogic(_selectedName, ind, mgr._ind);

                    if (mgr._ind != ind)
                    {
                        SwitchPlayer(_teamFlag);
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
    private static bool MoveLogic(string selectedChessName, int target, int ind)
    {
        // Debug.Log("target = " + target + ", ind = " + ind);
        if (selectedChessName.Contains("bing"))
        {
            if (ind > 45 && Math.Abs(target - ind) == 1 || target - ind == 9)
            {
                //Debug.Log("要移動的目標在ind = " + ind);
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                //Debug.Log("target - ind == " + target + " - " + ind + " =" + (target - ind));
                if (target - ind == 9)
                {
                    //Debug.Log("要移動的目標在ind = " + ind);
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    //Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr._ind = target; // don't move
                    return false;
                }
            }
        }

        if (selectedChessName.Contains("tzu"))
        {
            if (ind <= 45 && Math.Abs(ind - target) == 1 || ind - target == 9)
            {
                //Debug.Log("要移動的目標在ind = " + ind);
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                //Debug.Log(" ind -target == " + target + " - " + ind + " =" + (ind - target));
                if (ind - target == 9)
                {
                    //Debug.Log("要移動的目標在ind = " + ind);
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    //Debug.Log("留在原點則設定 target = ind 讓它認為那個點有棋子不能移動 target = " + target);
                    mgr._ind = target; // don't move
                    return false;
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
                mgr._ind = ind; // move
                return true;
            }
            else if (ind % 9 == 0 && Math.Abs(target - ind) < 9)
            {
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                mgr._ind = target;
                return false;
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
                    if (mgr._arrPos[i] != 0)
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
                if ((target - ind) % 9 == 0 || (indLeftRowEdge && indRightRowEdge))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if (ind % 9 == 0 && Math.Abs(target - ind) < 9)
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    mgr._ind = target;
                    return false;
                    // don't move
                }
            }
            else
            {
                mgr._ind = target;
                return false;
                // don't move
            }
        }

        if (selectedChessName.Contains("ma"))
        {
            var gap = target - ind;

            if (((gap == 17 || gap == 19)) && (mgr._arrPos[ind + 9] == 0))
            {
                mgr._ind = ind; // move
                return true;
            }
            else if (((gap == -17) || (gap == -19)) && (mgr._arrPos[ind - 9] == 0))
            {
                mgr._ind = ind; // move
                return true;
            }
            else if (((gap == -7) || (gap == 11)) && (mgr._arrPos[ind + 1] == 0))
            {
                mgr._ind = ind; // move
                return true;
            }
            else if (((gap == 7) || (gap == -11)) && (mgr._arrPos[ind - 1] == 0))
            {
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                mgr._ind = target; // dont move
                return false;
            }
        }

        if (selectedChessName.Contains("shiang"))
        {
            var gap = target - ind;
            if ((target < 45) && selectedChessName.Contains("red"))
            {
                if ((gap == 20) && (mgr._arrPos[ind + 10] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == 16) && (mgr._arrPos[ind + 8] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == -20) && (mgr._arrPos[ind - 10] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == -16) && (mgr._arrPos[ind - 8] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    mgr._ind = target; // dont move
                    return false;
                }
            }
            else if ((target > 45) && selectedChessName.Contains("black"))
            {
                if ((gap == 20) && (mgr._arrPos[ind + 10] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == 16) && (mgr._arrPos[ind + 8] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == -20) && (mgr._arrPos[ind - 10] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((gap == -16) && (mgr._arrPos[ind - 8] == 0))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    mgr._ind = target; // dont move
                    return false;
                }
            }
            else
            {
                mgr._ind = target; // dont move
                return false;
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
            {
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                mgr._ind = target; // dont move
                return false;
            }
        }

        if (selectedChessName.Contains("jiang"))
        {
            int[] jiangArray = { 67, 68, 69, 76, 77, 78, 85, 86, 87 };
            var gap = Math.Abs(target - ind);

            bool movable = jiangArray.Contains(target) && jiangArray.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
            {
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                mgr._ind = target; // dont move
                return false;
            }
        }

        if (selectedChessName.Contains("shuo"))
        {
            int[] shuoArray = { 4, 5, 6, 13, 14, 15, 22, 23, 24 };
            var gap = Math.Abs(target - ind);

            bool movable = shuoArray.Contains(target) && shuoArray.Contains(ind);
            if (movable && (gap == 9 || gap == 1))
            {
                mgr._ind = ind; // move
                return true;
            }
            else
            {
                mgr._ind = target; // dont move
                return false;
            }
        }
        return false;
    }

    // 回傳點選的棋子名稱
    private static string SelectedChessName(int index)
    {
        int boardPosNum = mgr._arrPos[index];

        if (boardPosNum == 0)
        {
            return _selectedName;
        }
        String chessName = mgr._res[boardPosNum];
        return chessName;
    }

    private static void SwitchPlayer(bool _teamFlag)
    {
        // 如果紅色走完了
        if (_teamFlag == false)
        {
            // 換成黑的走
            pos._teamFlag = true;
        }

        // 如果黑色走完了
        if (_teamFlag == true)
        {
            // 換成紅的走
            pos._teamFlag = false;
        }
    }

    /*
    private static string GamerIs(bool teamFlag)
    {
        if (_teamFlag == false)
        {
            return "red";
        }
        else
        {
            return "black";
        }
    }
    */

    private static bool SelectedChessChange(int index)
    {
        if (_selectedName == _firstSelectedName)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static bool KillChessLogic(int target, int ind)
    {
        bool killchess = false;
        //是否能吃子的參數 預設為不能吃
        string ind_string = Convert.ToString(mgr._arrPos[ind]); //將第一次點到的位置型態轉成字串
        string target_string = Convert.ToString(mgr._arrPos[target]);
        //兵卒
        string[] tzu = { "12", "13", "14", "15", "16" };
        string[] bing = { "28", "29", "30", "31", "32" };
        //炮 
        string[] black_pau = { "10", "11" };
        string[] red_pau = { "26", "27" };
        //馬 
        string[] black_ma = { "6", "7" };
        string[] red_ma = { "22", "23" };
        //車 
        string[] black_che = { "8", "9" };
        string[] red_che = { "24", "25" };
        //象 
        string[] black_shiang = { "4", "5" };
        string[] red_shiang = { "20", "21" };
        //士
        string[] black_shr = { "2", "3" };
        string[] red_shr = { "18", "19" };
        //將帥
        string[] jiang = { "1" };
        string[] shuo = { "17" };
        //卒吃兵,帥
        if (Array.Exists(tzu, element => element == ind_string) && (Array.Exists(bing, element => element == target_string)
        || Array.Exists(shuo, element => element == target_string)))
        {
            //Debug.Log("tzu " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //兵吃卒,將
        if (Array.Exists(bing, element => element == ind_string) && (Array.Exists(tzu, element => element == target_string)
       || Array.Exists(jiang, element => element == target_string)))
        {
            //Debug.Log("bing " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //炮的規則
        //黑炮吃所有 帥士象
        if (Array.Exists(black_pau, element => element == ind_string) && (Array.Exists(shuo, element => element == target_string)
            || Array.Exists(red_shr, element => element == target_string) || Array.Exists(red_shiang, element => element == target_string)
            || Array.Exists(red_che, element => element == target_string) || Array.Exists(red_ma, element => element == target_string)
            || Array.Exists(red_pau, element => element == target_string) || Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("black_pau " +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
       

        //紅炮吃所有
        if (Array.Exists(red_pau, element => element == ind_string) && (Array.Exists(shuo, element => element == target_string)
           || Array.Exists(black_shr, element => element == target_string) || Array.Exists(black_shiang, element => element == target_string)
           || Array.Exists(black_che, element => element == target_string) || Array.Exists(black_ma, element => element == target_string)
           || Array.Exists(black_pau, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("red_pau " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //馬的規則
        //黑馬吃紅馬,紅兵
        if (Array.Exists(black_ma, element => element == ind_string) && (Array.Exists(red_ma, element => element == target_string)
        && Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("black_ma " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //紅馬吃黑馬,紅兵
        if (Array.Exists(red_ma, element => element == ind_string) && (Array.Exists(tzu, element => element == target_string)
        || Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("red_ma " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //車的規則
        //黑車全吃        
        if (Array.Exists(black_che, element => element == ind_string) && (Array.Exists(shuo, element => element == target_string)
            || Array.Exists(red_shr, element => element == target_string) || Array.Exists(red_shiang, element => element == target_string)
            || Array.Exists(red_che, element => element == target_string) || Array.Exists(red_ma, element => element == target_string)
            || Array.Exists(red_pau, element => element == target_string) || Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("black_pau " +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //車的規則
        //紅車全吃
        //炮的規則
        //黑炮吃所有 帥士象
        if (Array.Exists(red_che, element => element == ind_string) && (Array.Exists(shuo, element => element == target_string)
            || Array.Exists(black_shr, element => element == target_string) || Array.Exists(black_shiang, element => element == target_string)
            || Array.Exists(black_che, element => element == target_string) || Array.Exists(black_ma, element => element == target_string)
            || Array.Exists(black_pau, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("black_pau " +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //象的規則
        //黑象吃紅象,紅車,紅馬,紅兵
        if (Array.Exists(black_shiang, element => element == ind_string) && (Array.Exists(red_shiang, element => element == target_string)
        || Array.Exists(red_che, element => element == target_string) || Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("black_shiang " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //紅象吃黑象,黑車,黑馬,黑卒
        if (Array.Exists(red_shiang, element => element == ind_string) && (Array.Exists(black_che, element => element == target_string)
        || Array.Exists(black_ma, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("red_shiang " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }

        //士的規則(不能走出九宮格所以不包含士)
        //黑士吃紅象,紅車,紅馬,紅兵
        if (Array.Exists(black_shr, element => element == ind_string) && (Array.Exists(red_shiang, element => element == target_string)
        || Array.Exists(red_ma, element => element == target_string) || Array.Exists(bing, element => element == target_string)))
        {
            //Debug.Log("black_shr " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }
        //紅士吃黑象,黑車,黑馬,黑卒
        if (Array.Exists(red_shr, element => element == ind_string) && (Array.Exists(black_che, element => element == target_string)
        || Array.Exists(black_ma, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("red_shr " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }
        //將帥規則(不能走出九宮格所以不包含士)
        //將吃黑象,黑車,黑馬,黑卒
        if (Array.Exists(jiang, element => element == ind_string) && (Array.Exists(black_shiang, element => element == target_string)
        || Array.Exists(black_che, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("jiang "+ "mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }
        //帥吃黑象,黑車,黑馬,黑卒
        if (Array.Exists(shuo, element => element == ind_string) && (Array.Exists(black_che, element => element == target_string)
        || Array.Exists(black_ma, element => element == target_string) || Array.Exists(tzu, element => element == target_string)))
        {
            //Debug.Log("shuo" +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            killchess = true;
            return killchess;
        }
        else
        {
            killchess = false;
        }
        return killchess;
    }

    public static bool PlayerControlTheCorrespondingChess(string selectedName)
    {
        if ((selectedName.Contains("red") && _teamFlag == false)
                    || (selectedName.Contains("black") && _teamFlag == true))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PlayerControlTheCorrespondingFirstChess(string firstSelectedName)
    {
        if ((firstSelectedName.Contains("red") && _teamFlag == false)
                    || (firstSelectedName.Contains("black") && _teamFlag == true))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
