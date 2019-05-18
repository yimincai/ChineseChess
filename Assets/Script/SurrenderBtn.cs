using UnityEngine;
using UnityEngine.UI;

public class SurrenderBtn : MonoBehaviour
{
    public Image _message;
    public Image _playAgainMessage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SurrenderBtnOnClick()
    {
        _message.gameObject.SetActive(true);
    }

    public void SurrenderCancel()
    {
        _message.gameObject.SetActive(false);
    }

    public void Surrender()
    {
        _message.gameObject.SetActive(false);
        _playAgainMessage.gameObject.SetActive(true);
    }
}
