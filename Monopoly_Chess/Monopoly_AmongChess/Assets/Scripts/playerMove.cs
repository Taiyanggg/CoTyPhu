using UnityEngine;
using Assets.New_scripts;

public class playerMove : MonoBehaviour
{

    hudChange hud;
    public Card target;
    public static CardMenu cardMenu;
    PlayerStuff player;
    DialogCentre dia;



    public float speed = 5f;

    private void Start()
    {
        player = GetComponent<PlayerStuff>();
        hud = GameObject.Find("HUD").GetComponentInChildren<hudChange>();
        dia = hud.GetComponentInChildren<DialogCentre>(true);
        if (cardMenu == null)
        {
            cardMenu = hud.GetComponentInChildren<CardMenu>();
        }
    }
    void Update()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        if (transform.position == target.transform.position)
        {
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        dia.ShowMessage(player.name + " went to  \"" + target.label + "\"");
        if (target.canBuy)
        {
            if (target.owner == null)
            {
                cardMenu.ShowBuy();
                cardMenu.ShowAuction();
            }
            else if (target.owner != player)
            {
                cardMenu.ShowPay();
            }
            else
            {
                player.EndTurn();
            }
        }

        else if (target.index == 9) // старт
        {
            dia.ShowMessage("Player " + player.name + " went to Start. Take " + GameSettings.StartLapMoney + "$");
            player.EndTurn();
        }
        else if (target.index == 10) // посещене тюрьмы
        {
            if (!player.inJail)
                dia.ShowMessage("Player " + player.name + " played in the prison. That funny");

            player.EndTurn();
        }
        else if (target.index == 11) // карта миниигры
        {
            dia.ShowMessage("Player " + player.name + " play minigame.");
            
            hud.miniGame.gameObject.SetActive(true);
            
        }
        else if (target.index == 12) // карта тюрьмы
        {
            dia.ShowMessage("Player " + player.name + " stole money for GI characters and was jailed.");

            player.GoToJail();
        }
        else if (target.index == 13) // заплатить немного
        {
            switch (Random.Range(0, 4))
            {

                case 0: dia.ShowMessage("Player " + player.name + " was fired by cop."); break;
                case 1: dia.ShowMessage("Player " + player.name + " was stolen!"); break;
                case 2: dia.ShowMessage("Player " + player.name + " bought new skin in LOL!"); break;
                case 3: dia.ShowMessage("Player " + player.name + " loss his phone and he must buy new one."); break;
                case 4: dia.ShowMessage("Player " + player.name + " send his money to Hoai Linh for the charity."); break;
            }


            cardMenu.ShowPay();
        }
        else if (target.index == 14) // заплатить много
        {
            switch (Random.Range(0, 4))
            {

                case 0: dia.ShowMessage("Player " + player.name + " was fired by cop."); break;
                case 1: dia.ShowMessage("Player " + player.name + " was stolen!"); break;
                case 2: dia.ShowMessage("Player " + player.name + " bought new skin in LOL!"); break;
                case 3: dia.ShowMessage("Player " + player.name + " loss his phone and he must buy new one."); break;
                case 4: dia.ShowMessage("Player " + player.name + " send his money to Hoai Linh for the charity."); break;
            }
            cardMenu.ShowPay();
        }
        else
        {
            cardMenu.ChanceCard();
        }

    }
}
