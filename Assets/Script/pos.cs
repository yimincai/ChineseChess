using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pos : MonoBehaviour
{
    public int ind;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseUp()
    {
        Debug.Log("pos.cs :" + ind);
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
                mgr.move(ind);
            }
        }

    }
}
