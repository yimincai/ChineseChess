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

    // fu

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
            int indLeftRowEdge;
            int indRightRowEdge;

            // fix chess pos 在右邊界的bugs
            if (ind % 9 == 0)
            {
                indLeftRowEdge = (((1 - (ind % 9)) + ind) - 9);
                indRightRowEdge = ind;
                //Debug.Log("indL: " + (((1 - (ind % 9)) + ind) - 9));
                //Debug.Log("indR: " + ind);
            }
            else
            {
                indLeftRowEdge = ((1 - (ind % 9)) + ind);
                indRightRowEdge = ((9 - (ind % 9)) + ind);
                //Debug.Log("indL: " + ((1 - (ind % 9)) + ind));
                //Debug.Log("indR: " + ((9 - (ind % 9)) + ind));
            }

            bool targetSmallerThenindLeftRowEdge = indLeftRowEdge <= target;
            bool targetBiggerThenindRightRowEdge = indRightRowEdge >= target;


            if ((target - ind) % 9 == 0 || (targetSmallerThenindLeftRowEdge && targetBiggerThenindRightRowEdge))
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
            int indLeftRowEdge;
            int indRightRowEdge;

            int blockFlag = 0;
            int upBlockPos = 0;
            int downBlockPos = 0;
            int rightBlockPos = 0;
            int leftBlockPos = 0;

            // fix chess pos 在右邊界的bugs
            if (ind % 9 == 0)
            {
                indLeftRowEdge = (((1 - (ind % 9)) + ind) - 9);
                indRightRowEdge = ind;
            }
            else
            {
                indLeftRowEdge = ((1 - (ind % 9)) + ind);
                indRightRowEdge = ((9 - (ind % 9)) + ind);
            }

            // 目標點大於左邊界
            bool targetBiggerThenIndLeftRowEdge = indLeftRowEdge <= target;
            // 目標點小於右邊界
            bool targetSmallerThenIndRightRowEdge = indRightRowEdge >= target;

            // 計算往上走的第一個阻擋棋子
            for (int i = ind + 9; i <= 90; i = i + 9)
            {
                if (mgr._arrPos[i] != 0)
                {
                    blockFlag++;
                    //Debug.Log("upBlockPos = " + i + 9);

                    if (blockFlag != 0)
                    {
                        upBlockPos = i + 9;
                        break;
                    }
                }
            }

            // 計算往下走的第一個阻擋棋子
            for (int i = ind - 9; i >= 0; i = i - 9)
            {
                if (mgr._arrPos[i] != 0)
                {
                    blockFlag++;
                    //Debug.Log("downBlockPos = " + i - 9);

                    if (blockFlag != 0)
                    {
                        downBlockPos = i - 9;
                        break;
                    }
                }
            }

            // 計算往左走的第一個阻擋棋子
            for (int i = ind - 1; i >= indLeftRowEdge; i--)
            {
                if (mgr._arrPos[i] != 0)
                {
                    blockFlag++;
                    //Debug.Log("leftBlockPos = " + (i-1));

                    if (blockFlag != 0)
                    {
                        leftBlockPos = i - 1;
                        break;
                    }
                }
            }

            // 計算往右走的第一個阻擋棋子
            for (int i = ind + 1; i <= indRightRowEdge; i++)
            {
                if (mgr._arrPos[i] != 0)
                {
                    blockFlag++;
                    //Debug.Log("rightBlockPos = " + (i+1));

                    if (blockFlag != 0)
                    {
                        rightBlockPos = i + 1;
                        break;
                    }
                }
            }

            if ((target - ind) % 9 == 0)
            {
                // 移動的目標點小於上方的阻擋棋子，或移動的目標點大於下方的阻擋棋子 -> 移動
                if ((target < ind) && (target > downBlockPos))
                {
                    //Debug.Log("target > downBlockPos : " + target + " > " + downBlockPos);
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((target > ind) && (target < upBlockPos))
                {
                    //Debug.Log("target < upBlockPos : " + target + " < " + upBlockPos);
                    mgr._ind = ind; // move
                    return true;
                }
                // 未達成移動條件，不移動
                else
                {
                    mgr._ind = target; // don't move
                    return false;

                }
            }
            else if (targetBiggerThenIndLeftRowEdge && targetSmallerThenIndRightRowEdge)
            {
                if (rightBlockPos == 0)
                {
                    rightBlockPos = indRightRowEdge;
                }

                if (leftBlockPos == 0)
                {
                    leftBlockPos = indLeftRowEdge;
                }

                if ((target > ind) && (target < rightBlockPos))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else if ((target < ind) && (target > leftBlockPos))
                {
                    mgr._ind = ind; // move
                    return true;
                }
                else
                {
                    mgr._ind = target; // don't move
                    return false;
                }
            }
            else
            {
                mgr._ind = target; // don't move
                return false;
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
        string ind_string = Convert.ToString(mgr._arrPos[ind]);
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


        bool blackCanEat_flag = Array.Exists(shuo, element => element == target_string) || Array.Exists(red_shr, element => element == target_string) || Array.Exists(red_shiang, element => element == target_string)
           || Array.Exists(red_che, element => element == target_string) || Array.Exists(red_ma, element => element == target_string)
           || Array.Exists(red_pau, element => element == target_string) || Array.Exists(bing, element => element == target_string);

        bool redCanEat_flag = Array.Exists(jiang, element => element == target_string)
           || Array.Exists(black_shr, element => element == target_string) || Array.Exists(black_shiang, element => element == target_string)
           || Array.Exists(black_che, element => element == target_string) || Array.Exists(black_ma, element => element == target_string)
           || Array.Exists(black_pau, element => element == target_string) || Array.Exists(tzu, element => element == target_string);

        //兵卒
        if (Array.Exists(tzu, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("bing " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        if (Array.Exists(bing, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("bing " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }


        //炮
        if (Array.Exists(black_pau, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("black_pau " +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        if (Array.Exists(red_pau, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("red_pau " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        //馬
        if (Array.Exists(black_ma, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("black_ma " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        if (Array.Exists(red_ma, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("red_ma " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        //車
        if (Array.Exists(black_che, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("black_che " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }
        if (Array.Exists(red_che, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("red_che " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }
        //象
        if (Array.Exists(black_shiang, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("black_shiang " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        if (Array.Exists(red_shiang, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("red_shiang " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        //士
        if (Array.Exists(black_shr, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("black_shr " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        if (Array.Exists(red_shr, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("red_shr " + "mgr._arrPos[ind]:" + mgr._arrPos[ind]+ "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }
        //將帥
        if (Array.Exists(jiang, element => element == ind_string) && blackCanEat_flag)
        {
            //Debug.Log("jiang "+ "mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }
        if (Array.Exists(shuo, element => element == ind_string) && redCanEat_flag)
        {
            //Debug.Log("shuo" +"mgr._arrPos[ind]:" + mgr._arrPos[ind] + "mgr._arrPos[target] " + mgr._arrPos[target]);
            return true;
        }

        return false;
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