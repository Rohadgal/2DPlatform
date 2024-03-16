using UnityEngine;
using Cinemachine;


public class LevelManager : MonoBehaviour
{

    public static LevelManager s_instance;
    LevelState m_levelState;
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
        if (PlayerManager.instance != null) {
            virtualCamera.Follow = PlayerManager.instance.transform;
        }
        EventManager.MyEvent += changeEnemySpawnArea;

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


    public float getSecondsToWait() { return secondsToWait; }

    public int getEnemySpawnArea() {  return enemySpawnArea; }

    private void changeEnemySpawnArea() {
        enemySpawnArea++;
    }
    private void OnDestroy() {
        EventManager.MyEvent -= changeEnemySpawnArea;
    }

}


public enum LevelState {
    None,
    Continue,
    LevelFinished,
    GameOver
}

