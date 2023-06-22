using UnityEngine;
using Cinemachine;


public class LevelManager : MonoBehaviour
{
    public static LevelManager s_instance;

    LevelState m_levelState;
    float time = 2;
    float secondsToWait = 4f;
    int enemySpawnArea = 0;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private void Awake() {
        if (FindObjectOfType<LevelManager>() != null &&
            FindObjectOfType<LevelManager>().gameObject != gameObject) {
            Destroy(gameObject);
        } else {
            s_instance = this;
        }
    }

    private void Start()
    {
        GameManager.s_instance.changeGameSate(GameState.Playing);
        PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
        virtualCamera.Follow = PlayerManager.instance.transform;
        
    }

    private void Update() {
        if (m_levelState == LevelState.LevelFinished) {
            GameManager.s_instance.changeScene();
            Debug.Log("LLegamos aca");
        }
        if (m_levelState == LevelState.GameOver) {
            GameManager.s_instance.changeGameSate(GameState.GameOver);
        }
    }

    public void changeLevelState(LevelState state) {
        m_levelState = state;
    }

    public float getTime() { return time; }

    public float getSecondsToWait() { return secondsToWait; }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            enemySpawnArea++;
        }
    }
    public int getEnemySpawnArea() {  return enemySpawnArea; }

    public void setEnemySpawnArea(int t_spawnArea) {
        enemySpawnArea = t_spawnArea;
    }
}


public enum LevelState {
    None,
    Continue,
    LevelFinished,
    GameOver
}

