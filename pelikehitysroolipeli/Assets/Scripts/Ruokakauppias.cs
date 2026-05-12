using UnityEngine;

public class RuokaKauppias : MonoBehaviour
{
    public ShopUIManager ui;
    public AudioClip coinSound;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (ui.shopUI.activeSelf)
                ui.Close();
            else
                ui.Open("Ruokakauppa");

            SetupButtons();
        }
    }

    void SetupButtons()
    {
        ui.button1.GetComponentInChildren<TMPro.TMP_Text>().text = "Nauta";
        ui.button2.GetComponentInChildren<TMPro.TMP_Text>().text = "Kana";
        ui.button3.GetComponentInChildren<TMPro.TMP_Text>().text = "Kasvis";

        ui.button1.onClick.RemoveAllListeners();
        ui.button2.onClick.RemoveAllListeners();
        ui.button3.onClick.RemoveAllListeners();

        ui.button1.onClick.AddListener(() => OstaRuoka("Nauta", 15, 30));
        ui.button2.onClick.AddListener(() => OstaRuoka("Kana", 10, 20));
        ui.button3.onClick.AddListener(() => OstaRuoka("Kasvis", 8, 15));
    }

    void OstaRuoka(string nimi, int hinta, int hp)
    {
        if (PlayerDataManager.Instance.TakeMoney(hinta))
        {
            PlayerDataManager.Instance.AddHealth(hp);

            ui.infoText.text = "Ostit: " + nimi;

            AudioManager.Instance.PlaySound(coinSound);
        }
        else
        {
            ui.infoText.text = "Ei tarpeeksi rahaa!";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            ui.Close();
        }
    }
}