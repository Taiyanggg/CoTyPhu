using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{

    private int xBoard = 1, yBoard =-1;

    public GameObject controller, movePlates;

    private string player;

    public Sprite wKing, wQueen, wRook, wBishop, wKnight, wPawn,
                  bKing, bQueen, bRook, bBishop, bKnight, bPawn;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        setCoordinates();

        switch (this.name)
        {
            case "b_Queen" : this.GetComponent<SpriteRenderer>().sprite = bQueen  ; player = "black"; break;
            case "b_King"  : this.GetComponent<SpriteRenderer>().sprite = bKing   ; player = "black"; break;
            case "b_Bishop": this.GetComponent<SpriteRenderer>().sprite = bBishop ; player = "black"; break;
            case "b_Knight": this.GetComponent<SpriteRenderer>().sprite = bKnight ; player = "black"; break;
            case "b_Rook"  : this.GetComponent<SpriteRenderer>().sprite = bRook   ; player = "black"; break;
            case "b_Pawn"  : this.GetComponent<SpriteRenderer>().sprite = bPawn   ; player = "black"; break;
                                                                                  
            case "w_Queen" : this.GetComponent<SpriteRenderer>().sprite =  wQueen ; player = "white"; break;
            case "w_King"  : this.GetComponent<SpriteRenderer>().sprite =   wKing ; player = "white"; break;
            case "w_Bishop": this.GetComponent<SpriteRenderer>().sprite = wBishop ; player = "white"; break;
            case "w_Knight": this.GetComponent<SpriteRenderer>().sprite = wKnight ; player = "white"; break;
            case "w_Rook"  : this.GetComponent<SpriteRenderer>().sprite =   wRook ; player = "white"; break;
            case "w_Pawn"  : this.GetComponent<SpriteRenderer>().sprite = wPawn   ; player = "white"; break;
        }
    }


    public void setCoordinates()
    {
        float x = xBoard, y = yBoard;
        x -= 3.5f;
        y -= 3.5f;

        this.transform.position = new Vector3(x, y, 1f);
    }

    public int getXBoard()
    {
        return xBoard;
    }
    public int getYBoard()
    {
        return yBoard;
    }
    public void setXBoard(int x)
    {
        xBoard = x;
    }
    public void setYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if(!controller.GetComponent<GamePlay>().isOverGame() && controller.GetComponent<GamePlay>().getTurnPlayer() == player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }

        

    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "b_Queen":
            case "w_Queen":
                LineMovePlate(1, 0) ; LineMovePlate(0, 1)  ; LineMovePlate(1, 1) ; LineMovePlate(-1, 0);
                LineMovePlate(0, -1); LineMovePlate(-1, -1); LineMovePlate(1, -1); LineMovePlate(-1, 1);
                break;

            case "b_Knight":
            case "w_Knight":
                LMovePlate();
                break;

            case "b_Bishop":
            case "w_Bishop":
                LineMovePlate(1, 1); LineMovePlate(-1, 1); LineMovePlate(1, 1); LineMovePlate(-1, -1);
                break;

            case "b_King":
            case "w_King":
                SurroundMovePlate();
                break;

            case "b_Rook":
            case "w_Rook":
                LineMovePlate(1, 0); LineMovePlate(0, 1); LineMovePlate(0, -1); LineMovePlate(-1, 0);
                break;

            case "b_Pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "w_Pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }


    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        GamePlay sc = controller.GetComponent<GamePlay>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while(sc.positionOnBoard(x,y) && sc.getPosition(x,y) == null)
        {
            movePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if(sc.positionOnBoard(x,y) && sc.getPosition(x,y).GetComponent<Chessman>().player !=player)
        {
            moveAttackPlateSpawn(x, y);
        }

    }

    public void LMovePlate()
    {
        pointMovePlate(xBoard + 1, yBoard + 2);
        pointMovePlate(xBoard + 1, yBoard - 2);
        pointMovePlate(xBoard - 1, yBoard + 2);
        pointMovePlate(xBoard - 1, yBoard - 2);
        pointMovePlate(xBoard + 2, yBoard + 1);
        pointMovePlate(xBoard + 2, yBoard - 1);
        pointMovePlate(xBoard - 2, yBoard + 1);
        pointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        pointMovePlate(xBoard, yBoard + 1);
        pointMovePlate(xBoard, yBoard - 1);
        pointMovePlate(xBoard + 1, yBoard + 1);
        pointMovePlate(xBoard + 1, yBoard - 1);
        pointMovePlate(xBoard - 1, yBoard + 1);
        pointMovePlate(xBoard - 1, yBoard - 1);
        pointMovePlate(xBoard + 1, yBoard);
        pointMovePlate(xBoard - 1, yBoard);
    }

    public void pointMovePlate(int x, int y)
    {
        GamePlay sc = controller.GetComponent<GamePlay>();

        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.getPosition(x, y);

            if(cp == null)
            {
                movePlateSpawn(x, y);
            }
            else if(cp.GetComponent<Chessman>().player != player)
            {
                moveAttackPlateSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        GamePlay sc = controller.GetComponent<GamePlay>();
        if (sc.positionOnBoard(x, y) == true)
        {
            if(sc.getPosition(x, y) == null)
            {
                movePlateSpawn(x, y);
            }
            if (sc.positionOnBoard(x + 1, y) && sc.getPosition(x + 1, y) != null && sc.getPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                moveAttackPlateSpawn(x + 1, y);
            }
            if (sc.positionOnBoard(x - 1, y) && sc.getPosition(x - 1, y) != null && sc.getPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                moveAttackPlateSpawn(x - 1, y);
            }
        }
    }

    public void movePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX, y = matrixY;

        x -= 3.5f;
        y -= 3.5f;

        GameObject mp = Instantiate(movePlates, new Vector3(x, y, 1), Quaternion.identity);

        MovePlates mpScript = mp.GetComponent<MovePlates>();
        mpScript.setReference(gameObject);
        mpScript.SetCoodinatesBoard(matrixX, matrixY);
    }
    public void moveAttackPlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX, y = matrixY;

        x -= 3.5f;
        y -= 3.5f;

        GameObject mp = Instantiate(movePlates, new Vector3(x, y, 1), Quaternion.identity);

        MovePlates mpScript = mp.GetComponent<MovePlates>();
        mpScript.attack = true;
        mpScript.setReference(gameObject);
        mpScript.SetCoodinatesBoard(matrixX, matrixY);
    }

}
