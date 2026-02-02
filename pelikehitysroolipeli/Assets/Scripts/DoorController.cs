using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Kuvat oven eri tiloille
    [SerializeField]
    Sprite ClosedDoorSprite;
    [SerializeField]
    Sprite OpenDoorSprite;
    [SerializeField]
    Sprite LockedSprite;
    [SerializeField]
    Sprite UnlockedSprite;

    BoxCollider2D colliderComp;

    // Näitä värejä käytetään lukkosymbolin piirtämiseen.
    public static Color lockedColor;
    public static Color openColor;

    SpriteRenderer doorSprite; // Oven kuva
    SpriteRenderer lockSprite; // Lapsi gameobjectissa oleva lukon kuva

    // Debug ui
    [SerializeField]
    bool ShowDebugUI;
    [SerializeField]
    int DebugFontSize = 32;


    void Start()
    {
        doorSprite = GetComponent<SpriteRenderer>();
        colliderComp = GetComponent<BoxCollider2D>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites.Length == 2 && sprites[0] == doorSprite)
        {
            lockSprite = sprites[1];
        }


        lockedColor = new Color(1.0f, 0.63f, 0.23f);
        openColor = new Color(0.5f, 0.8f, 1.0f);


        // TODO
        // missä tilassa ovi on kun peli alkaa?
        currentState = DoorState.Lukittu;
    }
    public enum DoorState { Auki, Kiinni, Lukittu }
    public enum DoorAction { Avaa, Sulje, Lukitse, AvaaLukko }

    public DoorState currentState = DoorState.Kiinni;

    // Vastaanottaa pelaajan toiminnon
    public void ReceiveAction(DoorAction action)
    {
        switch (action)
        {
            case DoorAction.Avaa:
                if (currentState != DoorState.Lukittu)
                {
                    currentState = DoorState.Auki;
                    OpenDoor();
                }   break;

            case DoorAction.Sulje:
                if (currentState != DoorState.Lukittu)
                {
                    currentState = DoorState.Kiinni;
                    CloseDoor();
                }
                break;

            case DoorAction.Lukitse:
                if (currentState == DoorState.Kiinni)
                {
                    currentState = DoorState.Lukittu;
                    LockDoor();
                } break;

            case DoorAction.AvaaLukko:
                if (currentState == DoorState.Lukittu)
                {
                    currentState = DoorState.Kiinni;
                    UnlockDoor();
                } break;
        }

        Debug.Log($"{gameObject.name} tila: {currentState}");
    }
    private void OpenDoor()
    {
        doorSprite.sprite = OpenDoorSprite;
        colliderComp.isTrigger = true;
    }

    /// <summary>
    /// Vaihtaa oven kuvan suljetuksi oveksi
    /// ja laittaa törmäysalueen päälle
    /// </summary>
    private void CloseDoor()
    {
        doorSprite.sprite = ClosedDoorSprite;
        colliderComp.isTrigger = false;
    }

    /// <summary>
    /// Vaihtaa lukkosymbolin lukituksi ja
    /// vaihtaa sen värin
    /// </summary>
    private void LockDoor()
    {
        lockSprite.sprite = LockedSprite;
        lockSprite.color = lockedColor;
    }

    /// <summary>
    /// Vaihtaa lukkosymbolin avatuksi ja
    /// vaihtaa sen värin
    /// </summary>
    private void UnlockDoor()
    {
        lockSprite.sprite = UnlockedSprite;
        lockSprite.color = openColor;
    }

}
