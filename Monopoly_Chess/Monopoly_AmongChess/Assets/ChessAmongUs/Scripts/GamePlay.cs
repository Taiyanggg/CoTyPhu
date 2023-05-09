
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] blackPlayer = new GameObject[16];
    private GameObject[] whitePlayer = new GameObject[16];

    private string turnPlayer = "white";
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        whitePlayer = new GameObject[16]
        {
            Create("w_Rook", 0, 0), Create("w_Knight", 1, 0),Create("w_Bishop", 2, 0),Create("w_King", 3, 0),Create("w_Queen", 4, 0),Create("w_Bishop", 5, 0),Create("w_Knight", 6, 0),Create("w_Rook", 7, 0),
            Create("w_Pawn", 0, 1), Create("w_Pawn", 1, 1)  ,Create("w_Pawn", 2, 1)  ,Create("w_Pawn", 3, 1),Create("w_Pawn", 4, 1) ,Create("w_Pawn", 5, 1)  ,Create("w_Pawn", 6, 1)  ,Create("w_Pawn", 7, 1)
        };
        blackPlayer = new GameObject[16]
        {
            Create("b_Rook", 0, 7), Create("b_Knight", 1, 7),Create("b_Bishop", 2, 7),Create("b_King", 3, 7),Create("b_Queen", 4, 7),Create("b_Bishop", 5, 7),Create("b_Knight", 6, 7),Create("b_Rook", 7, 7),
            Create("b_Pawn", 0, 6), Create("b_Pawn", 1, 6)  ,Create("b_Pawn", 2, 6)  ,Create("b_Pawn", 3, 6),Create("b_Pawn", 4, 6) ,Create("b_Pawn", 5, 6)  ,Create("b_Pawn", 6, 6)  ,Create("b_Pawn", 7, 6)
        };

        for(int i = 0; i< blackPlayer.Length; i++)
        {
            setPosition(blackPlayer[i]);
            setPosition(whitePlayer[i]);
        }

    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();

        cm.name = name;
        cm.setXBoard(x);
        cm.setYBoard(y);
        cm.Activate();
        return obj;
    }

    public void setPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.getXBoard(), cm.getYBoard()] = obj;
    }


    public void setPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject getPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool positionOnBoard(int x ,int y)
    {
        if( x< 0 || y<0 || x > 7 || y > 7)
        {
            return false;
        }
        return true;
    }

    public string getTurnPlayer()
    {
        return turnPlayer;
    }

    public bool isOverGame()
    {
        return gameOver;
    }

    public void nextTurn()
    {
        if (turnPlayer == "white") { turnPlayer = "black"; }
        else {turnPlayer = "white"; }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene(0);
        }
    }

    public void Winner(string winplayer)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = winplayer + " win :DD";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;

    }
}
