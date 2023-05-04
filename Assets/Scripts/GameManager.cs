using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject tank1;
    public GameObject tank2;
    public Text turnText;
    public Text winnerText;
    public int currentPlayerIndex = 1;
    private bool gameIsOver = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        turnText.text = "Player 1's Turn";
        winnerText.enabled = false;      
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsOver)
        {          
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TankShooting tankShooting = GetCurrentTankShooting();
                if (tankShooting != null)
                {
                    tankShooting.Fire();
                    NextPlayer();
                    EndTurn();
                }
            }
        }
    }

    private TankShooting GetCurrentTankShooting()
    {
        if (currentPlayerIndex == 1)
        {
            Debug.Log("Tank 1 Fire");
            return tank1.GetComponent<TankShooting>();
        }
        else if (currentPlayerIndex == 2)
        {
            Debug.Log("Tank 2 FIre");
            return tank2.GetComponent<TankShooting>();
        }
        else
        {
            return null;
        }
    }

    public void NextPlayer()
    { 
        if (currentPlayerIndex == 1)
        {
            currentPlayerIndex = 2;
        }
        else
        {
            currentPlayerIndex = 1;
        }  
    }

    private void EndTurn()
    {
        TankHealth tank1Health = tank1.GetComponent<TankHealth>();
        TankHealth tank2Health = tank2.GetComponent<TankHealth>();

        if (tank1Health.currentHealth <= 0)
        {
            EndGame(2);
        }
        else if (tank2Health.currentHealth <= 0)
        {
            EndGame(1);
        }
        else
        {
            currentPlayerIndex = (currentPlayerIndex == 1) ? 2 : 1;
            turnText.text = "Player " + currentPlayerIndex + "'s Turn";
        }
    }

    public void EndGame(int winner)
    {
        gameIsOver = true;
        winnerText.enabled = true;
        winnerText.text = "Player " + winner + " Wins!";
        SceneManager.LoadScene(2);
    } 
}
