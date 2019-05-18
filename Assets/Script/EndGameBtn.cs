using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameBtn : MonoBehaviour
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

    private void OnClick()
    {
        Debug.Log("EndGameBtn Clicked!!");
        SceneManager.LoadScene("MainScene");
    }
}