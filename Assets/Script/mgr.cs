using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mgr : MonoBehaviour
{
    public static int _ind = -1;
    // flag
    // 判斷你是要選棋子還是要讓棋子移動
    // flag=true 點sprite(選棋子)
    public static bool _flag = true;

    // model
    // 棋子圖片陣列，使用名稱對應resources裡的檔案名稱
    public static string[] _res = { "none", //null
                             "black_jiang", //1_黑棋_將
                             "black_shr_1", //2_黑棋_士_左
                             "black_shr_2", //3_黑棋_士_右
                             "black_shiang_1", //4_黑棋_象_左
                             "black_shiang_2", //5_黑棋_象_右
                             "black_ma_1", //6_黑棋_馬_左
                             "black_ma_2", //7_黑棋_馬_右
                             "black_che_1", //8_黑棋_車_左
                             "black_che_2", //9_黑棋_車_右
                             "black_pau_1", //10_黑棋_砲_左
                             "black_pau_2", //11_黑棋_砲_右
                             "black_tzu_1", //12_黑棋_卒_1
                             "black_tzu_2", //13_黑棋_卒_2
                             "black_tzu_3", //14_黑棋_卒_3
                             "black_tzu_4", //15_黑棋_卒_4
                             "black_tzu_5", //16_黑棋_卒_5
                             "red_shuo", //17_紅棋_帥
                             "red_shr_1", //18_紅棋_士
                             "red_shr_2", //19_紅棋_士
                             "red_shiang_1", //20_紅棋_象
                             "red_shiang_2", //21_紅棋_象
                             "red_ma_1", //22_紅棋_馬
                             "red_ma_2", //23_紅棋_馬
                             "red_che_1", //24_紅棋_車
                             "red_che_2", //25_紅棋_車
                             "red_pau_1", //26_紅棋_砲
                             "red_pau_2", //27_紅棋_砲
                             "red_bing_1", //28_紅棋_兵_1
                             "red_bing_2", //29_紅棋_兵_2
                             "red_bing_3", //30_紅棋_兵_3
                             "red_bing_4", //31_紅棋_兵_4
                             "red_bing_5", //32_紅棋_兵_5
                           };

    // 棋盤model，每個棋子都有自己的index，對應res陣列
    public static int[] _arrPos = { 0,
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

    public static List<int> _redKilledChess = new List<int>();
    public static List<int> _blackKilledChess = new List<int>();

    void Start()
    {
        //Debug.Log("mgr.cs");
        //Debug.Log("==========");
        //GameObject.Instantiate(checkerboard);



    }

    // View 
    // 棋子顯示的方法，每一偵檢查陣列，決定該位置要顯示什麼圖片
    // 陣列的內容是0的時候 sprite=null 就移除棋子的圖案
    void Update()
    {
        //Debug.Log("Play again value : " + PlayAgainPanel._playAgainFlag);
        // display chessboard chess
        for (int i = 1; i < _arrPos.Length; i++)
        {
            GameObject gameObject = GameObject.Find(i + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            if (_arrPos[i] == 0)
            {
                spr.sprite = null;
            }
            else
            {
                Sprite[] playerSprite = Resources.LoadAll<Sprite>(_res[_arrPos[i]]);
                spr.sprite = playerSprite[0];
            }
        }

        // display red killed chess board
        for (int i = 0; i <= _redKilledChess.Count - 1; i++)
        {
            GameObject gameObject = GameObject.Find((i + 116) + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            if (PlayAgainPanel._playAgainFlag > 0)
            {
                spr.sprite = null;
            }
            else
            {
                Sprite[] playerSprite = Resources.LoadAll<Sprite>(_res[(int)_redKilledChess[i]]);
                spr.sprite = playerSprite[0];
            }
        }

        // display black killed chess board
        for (int i = 0; i <= _blackKilledChess.Count - 1; i++)
        {
            GameObject gameObject = GameObject.Find((i + 100) + "");
            SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
            if (PlayAgainPanel._playAgainFlag > 0)
            {
                spr.sprite = null;
            }
            else
            {
                Sprite[] playerSprite = Resources.LoadAll<Sprite>(_res[(int)_blackKilledChess[i]]);
                spr.sprite = playerSprite[0];
            }
        }

        PlayAgainPanel._playAgainFlag--;

        if (PlayAgainPanel._playAgainFlag == 0)
        {
            _redKilledChess.Clear();
            _blackKilledChess.Clear();
        }
    }

    // Contorller
    public static void move(int target)
    {
        //Debug.Log("mgr.cs ind:" + ind + " target:" + target);
        _arrPos[target] = _arrPos[_ind];
        _arrPos[_ind] = 0;
        _flag = true;
    }
}