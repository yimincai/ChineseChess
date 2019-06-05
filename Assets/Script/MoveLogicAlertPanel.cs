using UnityEngine;
using UnityEngine.UI;

public class MoveLogicAlertPanel : MonoBehaviour
{
    public Image _moveLogicAlertPanel;
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
        _moveLogicAlertPanel.gameObject.SetActive(false);
        _whoseTurnMessage.gameObject.SetActive(true);
    }
}
