using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Jos instanssi on jo olemassa ja se ei ole tämä
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate GameManager destroyed: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("GameManager initialized: " + gameObject.name);
    }
}