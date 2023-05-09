using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlates : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    int matrixX, matrixY;

    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
    }

    public void SetCoodinatesBoard(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void setReference(GameObject obj)
    {
        reference = obj;
    }
    public GameObject getReference()
    {
        return reference;
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack == true)
        {
            GameObject cp = controller.GetComponent<GamePlay>().getPosition(matrixX, matrixY);

            if (cp.name == "w_King") controller.GetComponent<GamePlay>().Winner("Black");
            if (cp.name == "b_King") controller.GetComponent<GamePlay>().Winner("White");

            Destroy (cp);
        }
        controller.GetComponent<GamePlay>().setPositionEmpty(reference.GetComponent<Chessman>().getXBoard(), reference.GetComponent<Chessman>().getYBoard());

        reference.GetComponent<Chessman>().setXBoard(matrixX);
        reference.GetComponent<Chessman>().setYBoard(matrixY);
        reference.GetComponent<Chessman>().setCoordinates();

        controller.GetComponent<GamePlay>().setPosition(reference);

        controller.GetComponent<GamePlay>().nextTurn();

        reference.GetComponent<Chessman>().DestroyMovePlates();
    }
}
