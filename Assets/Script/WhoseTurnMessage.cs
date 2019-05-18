using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhoseTurnMessage : MonoBehaviour
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
            this.gameObject.GetComponent<Text>().text = "<color=#000000>換" + team + "方陣營</color>";
        }
        else
        {
            team = "紅";
            this.gameObject.GetComponent<Text>().text = "<color=#FF2D00>換" + team + "方陣營</color>";
        }
    }
}
