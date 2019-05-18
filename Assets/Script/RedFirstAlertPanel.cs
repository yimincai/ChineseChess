using UnityEngine;
using UnityEngine.UI;

public class RedFirstAlertPanel : MonoBehaviour
{
    public Image _redFirstAlertPanel;
    public Text _whoseTurnMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideRedFirstAlertOnClick()
    {
        _redFirstAlertPanel.gameObject.SetActive(false);
        _whoseTurnMessage.gameObject.SetActive(true);
    }
}
