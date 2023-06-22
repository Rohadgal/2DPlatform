using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de toda la l�gica del Player, pero no del control
/// </summary>
public class PlayerManager : MonoBehaviour {
    /// <summary>
    /// Instancia p�blica de la clase PlayerManager
    /// </summary>
    public static PlayerManager instance;

    Animator m_animator;

    PlayerState playerState;

    private void Awake() {
        m_animator = GetComponent<Animator>();
        if (FindObjectOfType<PlayerManager>() != null &&
            FindObjectOfType<PlayerManager>().gameObject != gameObject)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        playerState = PlayerState.None;
    }
    // Start is called before the first frame update
    void Start() {
        m_animator = GetComponent<Animator>();
    }

    private void Update() {
        if (playerState == PlayerState.Dead) {
            //Debug.Log("Se acabó el juego");
            //GameManager.s_instance.changeGameSate(GameState.GameOver);
            LevelManager.s_instance.changeLevelState(LevelState.GameOver);
        }

        //Debug.Log("IS DEAD: " + IsDead());
       
    }

    public void ChangePlayerState(PlayerState newState) {
        if (playerState == newState) {

            return;
        }

        resetAnimatorParameters();

        playerState = newState;
        switch (playerState) {
            case PlayerState.None:
                break;
            case PlayerState.Idle:
                m_animator.SetBool("isIdle", true);
                break;
            case PlayerState.Running:
                m_animator.SetBool("isRunning", true);
                break;
            case PlayerState.Jumping:
                m_animator.SetBool("isJumping", true);
                break;
            case PlayerState.JumpFall:
                m_animator.SetBool("isFalling", true);
                break;
            case PlayerState.FreeFall:
                m_animator.SetBool("isFalling", true);
                break;
            case PlayerState.Dead:
                m_animator.SetBool("isDead", true);

                break;
            default: break;
        }
    }
    private void resetAnimatorParameters() {
        foreach (AnimatorControllerParameter parameter in m_animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool) {
                m_animator.SetBool(parameter.name, false);
            }
        }
    }
    public PlayerState GetState(){ return playerState; }

    private bool IsDead() {
        if (PlayerManager.instance.GetState() != PlayerState.Dead) {
            return false;
        }
        //Debug.LogWarning("You died");
        StartCoroutine(DestroyPlayer());
        return true;
    }

    IEnumerator DestroyPlayer() {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}

public enum PlayerState {
    None, 
    Idle,
    Running,
    Jumping,
    JumpFall,
    FreeFall,
    Dead
}
