using UnityEngine;

public class NuoliKauppias : MonoBehaviour
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
                ui.Open("Nuolikauppa");

            SetupButtons();
        }
    }

    void SetupButtons()
    {
        ui.button1.GetComponentInChildren<TMPro.TMP_Text>().text = "Puu nuoli";
        ui.button2.GetComponentInChildren<TMPro.TMP_Text>().text = "Teräs nuoli";
        ui.button3.GetComponentInChildren<TMPro.TMP_Text>().text = "Timantti nuoli";

        ui.button1.onClick.RemoveAllListeners();
        ui.button2.onClick.RemoveAllListeners();
        ui.button3.onClick.RemoveAllListeners();

        ui.button1.onClick.AddListener(() => OstaNuoli("Puu", 3));
        ui.button2.onClick.AddListener(() => OstaNuoli("Teras", 5));
        ui.button3.onClick.AddListener(() => OstaNuoli("Timantti", 50));
    }

    void OstaNuoli(string nimi, int hinta)
    {
        if (PlayerDataManager.Instance.TakeMoney(hinta))
        {
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