using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Liikkuminen")]
    [SerializeField] private float speed = 5f;

    [Header("Oven UI")]
    [SerializeField] private GameObject doorPanel;

    private DoorController nearbyDoor;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (doorPanel != null)
            doorPanel.SetActive(false);
    }

    private void Update()
    {
        // Lue pelaajan input
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical"); // 2D: Y-akseli
        moveInput = new Vector2(moveX, moveY).normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            nearbyDoor = other.GetComponent<DoorController>();
            ShowDoorUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            nearbyDoor = null;
            ShowDoorUI(false);
        }
    }

    private void ShowDoorUI(bool show)
    {
        if (doorPanel != null)
            doorPanel.SetActive(show);
    }

    // UI-napit
    public void AvaaOvi() => nearbyDoor?.ReceiveAction(DoorController.DoorAction.Avaa);
    public void SuljeOvi() => nearbyDoor?.ReceiveAction(DoorController.DoorAction.Sulje);
    public void LukitseOvi() => nearbyDoor?.ReceiveAction(DoorController.DoorAction.Lukitse);
    public void AvaaLukko() => nearbyDoor?.ReceiveAction(DoorController.DoorAction.AvaaLukko);
}
