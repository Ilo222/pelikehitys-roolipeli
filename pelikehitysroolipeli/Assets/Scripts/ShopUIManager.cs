using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shopUI;
    public TMP_Text titleText;
    public TMP_Text infoText;

    public Button button1;
    public Button button2;
    public Button button3;

    public void Open(string title)
    {
        shopUI.SetActive(true);
        titleText.text = title;
        infoText.text = "Valitse tuote";
    }

    public void Close()
    {
        shopUI.SetActive(false);
    }
}