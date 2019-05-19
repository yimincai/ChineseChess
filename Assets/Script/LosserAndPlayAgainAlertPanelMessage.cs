using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosserAndPlayAgainAlertPanelMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string team = null;
        if (pos._teamFlag)
        {
            team = "黑";
        }
        else
        {
            team = "紅";
        }

        this.gameObject.GetComponent<Text>().text = team + "方投降輸了\n是否再來一局?";
    }
}
