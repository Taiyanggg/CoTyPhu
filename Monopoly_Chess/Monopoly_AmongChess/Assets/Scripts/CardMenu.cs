using UnityEngine;
using UnityEngine.UI;

public class CardMenu : MonoBehaviour {

    Turn turn;
    Button payButton;
    Text pay;
    Text buy;
    Button buyButton; 
    Button declineButton;
    DialogCentre dia;
    hudChange hud;

    void Start () {
        buyButton = GetComponentsInChildren<Button>(true)[0];
        payButton = GetComponentsInChildren<Button>(true)[1];
        declineButton = GetComponentsInChildren<Button>(true)[2];


        hud = GameObject.Find("HUD").GetComponentInChildren<hudChange>();
        turn = GameObject.Find("players").GetComponent<Turn>();
        Operations op = turn.GetComponent<Operations>();
        buyButton.onClick.AddListener(op.Buy);
        payButton.onClick.AddListener(op.Pay);
        declineButton.onClick.AddListener(op.Decline);
        pay = payButton.GetComponentInChildren<Text>();
        buy = buyButton.GetComponentInChildren<Text>();
    }

    internal void ShowPay()
    {
        payButton.gameObject.SetActive(true);
        pay.text = "Paid " + turn.player.targetCard.cost.ToString();
        
    }

    internal void ShowBuy()
    {
        buyButton.gameObject.SetActive(true);
        buy.text = "Bought " + turn.player.targetCard.cost.ToString();

    }

    internal void ShowAuction()
    {
        declineButton.gameObject.SetActive(true);
    }

    internal void ChanceCard()
    {
        PlayerStuff player = turn.player;
        dia = hud.GetComponentInChildren<DialogCentre>(true);
        
        switch (UnityEngine.Random.Range(0, 4))
        {

            case 0: dia.ShowMessage("Player " + player.name + " took lucky money on the Lunar New Year. +684$"); player.GetComponent<Money>().Transaction(-684); player.EndTurn(); break;
            case 1: dia.ShowMessage("Player " + player.name + " was beaten and took the indemnify money. +980$"); player.GetComponent<Money>().Transaction(-980); player.EndTurn(); break;
            case 2: dia.ShowMessage("Player " + player.name + " looted from chest. +1500$"); player.GetComponent<Money>().Transaction(-1500); player.EndTurn(); break;
            case 3: dia.ShowMessage("Player " + player.name + " broke his heart by marriage. Repair with 1000$"); player.targetCard.cost = 1000; ShowPay(); break;
            case 4: dia.ShowMessage("Player " + player.name + " wants to buy new GI character. Pay 2000$ ?"); player.targetCard.cost = 2000; ShowPay(); break;
        }
    }
}
