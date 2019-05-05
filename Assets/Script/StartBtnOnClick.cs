using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtnOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Button btn = this.GetComponent<Button>(); 
       btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick(){
        Debug.Log("Btn Clicked. !!");
    }
}
