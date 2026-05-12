using UnityEngine;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    public int experience;
    public int money;
    public int health;

    [Header("UI")]
    public TMP_Text experienceText;
    public TMP_Text moneyText;
    public TMP_Text healthText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    // ======================
    // LISÄYSFUNKTIOT
    // ======================

    public void AddExperience(int amount)
    {
        experience += amount;
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void AddHealth(int amount)
    {
        health += amount;
        UpdateUI();
    }

    // ======================
    // KAUPPA (TÄRKEÄ)
    // ======================

    public bool TakeMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    public bool TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
            health = 0;

        UpdateUI();
        return true;
    }

    // ======================
    // UI
    // ======================

    void UpdateUI()
    {
        if (experienceText != null)
            experienceText.text = "XP: " + experience;

        if (moneyText != null)
            moneyText.text = "Coins: " + money;

        if (healthText != null)
            healthText.text = "HP: " + health;
    }
}