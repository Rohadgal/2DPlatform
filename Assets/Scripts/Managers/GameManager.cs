using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s_instance;

    public GameObject canvas;
    public GameObject winUI;
    public GameObject gameOverUI;
    public GameObject creditsUI;
    private GameState m_gameState;

    //public bool isGameOver, isStarted;
    int levelIndex;
    int mainMenuScene = 0;
    int creditsScene = 4;
    int lastLevelScene = 3;

    private void Awake() {
        if (canvas != null && SceneManager.GetActiveScene().buildIndex != mainMenuScene) {
            canvas.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        if (FindObjectOfType<GameManager>() != null &&
            FindObjectOfType<GameManager>().gameObject != gameObject) {
            Destroy(gameObject);
            return;
        }
        s_instance = this;
        m_gameState = GameState.None;
    }

    private void Update() {
        if (m_gameState == GameState.GameOver) {
            gameOver();
        }
        if (m_gameState == GameState.GameFinished) {
            gameFinished();
        }
    }

    //public void changeGameStateInEditor(string newState) {
    //    changeGameSate((GameState)System.Enum.Parse(typeof(GameState), newState));
    //}

    void gameOver() {
        if (canvas != null) {
            canvas.SetActive(true);
            gameOverUI.SetActive(true);
        }  
    }

    void gameFinished() {
        if(canvas != null) {
            canvas.SetActive(true);
            winUI.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "LevelThree1") {
            StartCoroutine(openCredits());
        }
    }

    IEnumerator openCredits() {
        float waitTimeForCredits = 4f;
        yield return new WaitForSeconds(4f);
        winUI.SetActive(false );
        creditsUI.SetActive(true);
    }

    public void loadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void changeGameSate(GameState t_newState) {
        if (m_gameState == t_newState) {
            return;
        }
        m_gameState = t_newState;
        switch (m_gameState) {
            case GameState.None:
                break;
            case GameState.LoadMenu:
                loadMainMenu();
                break;
            case GameState.ChangeLevel:
                break;
            case GameState.Playing:
                break;
            case GameState.GameOver:
                break;
            case GameState.GameFinished:
                break;
            default:
                throw new UnityException("Invalid Game State");
        }
    }

    public void changeScene() {
        levelIndex = SceneManager.GetActiveScene().buildIndex;

        if(SceneManager.GetActiveScene().buildIndex == lastLevelScene) {
            StartCoroutine (openCredits());
            return;
        }
        
        if(levelIndex < SceneManager.sceneCountInBuildSettings - 1) {
            levelIndex++;
            SceneManager.LoadScene(levelIndex);
        }
        //else {
        //    m_gameState = GameState.GameFinished;
        //}
    }
    public GameState getGameState() { return m_gameState; }

    public void exitGame() {
            Application.Quit();
    }

    public void retryLevel() {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }
}

public enum GameState {
    None,
    LoadMenu,
    ChangeLevel,
    Playing,
    GameOver,
    GameFinished
}
