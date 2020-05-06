using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject TitleBackgroundImage;

    public GameObject TitleText;

    public GameObject GameStartButton;

    public GameObject Player;

    public GameObject GameAssets;

    public GameObject playerPrefab;

    public Transform spawnPoint;

    public int PlayerLives = 3;

    public GameObject PlayerDeathText;

    public GameObject GameOverText;

    public List<GameObject> EnemySpawnPoints = new List<GameObject>();

    public string gameState = "StartScreen";

    public List<GameObject> currentEnemies = new List<GameObject>();

    public GameObject enemyPrefabs;

    public GameObject game;

    public void EnemySpawn()
    {
        Debug.Log("Spawning Enemies");
        foreach(GameObject spawnPoint in EnemySpawnPoints)
        {
            Debug.Log("Looping through enemy spawn points");
            Instantiate(enemyPrefabs, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    public void DestroyEnemies()
    {

    }

    public void ButtonTest()
    {
        Debug.Log("Button was pressed");
    }
    private void StartScreen()
    {
        // Show the start screen menu
        if (!TitleText.activeInHierarchy) 
        {
            TitleText.SetActive(true);
            TitleBackgroundImage.SetActive(true);
            GameStartButton.SetActive(true);
            
        }
        GameOverText.SetActive(false);
    }



    public void StartGame()
    {
        InitializeGame();
        SpawnPlayer();
        ChangeState("Gameplay");
    }

    private void InitializeGame()
    {
        TitleText.SetActive(false);
        TitleBackgroundImage.SetActive(false);
        GameStartButton.SetActive(false);
        game.SetActive(true);
        EnemySpawn();

    }



    private void SpawnPlayer()
    {
        if(Player == null)
        {
            Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
       
        PlayerDeathText.SetActive(false);
    }



    private void Gameplay()
    {
        
    }



    private void PlayerDeath()
    {
        PlayerDeathText.SetActive(true);
    }



    private void GameOver()
    {
        GameOverText.SetActive(true);
    }

    public void ChangeState(string newState)
    {
        gameState = newState;
    }

    
   

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "StartScreen")
{
            // Do the Start Screen Behavior
            StartScreen();

            // This state doesn't check for transitions.
            // The start button should call ChangeState with "InitializeGame" as the parameter.
        }
        else if (gameState == "InitializeGame")
        {
            InitializeGame();
            // Transition is automatic
            ChangeState("SpawnPlayer");
        }
        else if (gameState == "SpawnPlayer")
        {
            SpawnPlayer();
            // Transition is Automatic
            ChangeState("Gameplay");
        }
        else if (gameState == "Gameplay")
        {
            // Do the state behaviors
            Gameplay();
            // Check for transitions
            // if the player has been destroyed and has more than 0 lives, change to playerdeath
            if (Player == null && PlayerLives > 0)
            {
                ChangeState("PlayerDeath");
            }
            // if the player has been destroyed and has 0 lives, then change to gameover state.
            if(Player == null && PlayerLives <= 0)
            {
                ChangeState("GameOver");
            }
        }
        else if (gameState == "PlayerDeath")
        {
            // Do the state behaviors
            PlayerDeath();
            // Check for transitions
            if(Input.anyKeyDown)
            {
                ChangeState("SpawnPlayer");
            }
        }
        else if (gameState == "GameOver")
        {
            // Do the state behavior
            GameOver();
            // Check for transitions.
            if (Input.anyKeyDown)
            {
                ChangeState("StartMenu");
            }
        }
        else
        {
            Debug.LogWarning("Game State does not exist: " + gameState);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
