using UnityEngine;

public class DoorController : MonoBehaviour
{
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
                    currentState = DoorState.Auki;
                break;

            case DoorAction.Sulje:
                if (currentState != DoorState.Lukittu)
                    currentState = DoorState.Kiinni;
                break;

            case DoorAction.Lukitse:
                currentState = DoorState.Lukittu;
                break;

            case DoorAction.AvaaLukko:
                if (currentState == DoorState.Lukittu)
                    currentState = DoorState.Kiinni;
                break;
        }

        Debug.Log($"{gameObject.name} tila: {currentState}");
    }
}
