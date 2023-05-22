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

    Animator animator;

    PlayerState playerState;

    private void Awake() {
        instance = this;
        playerState = PlayerState.None;
    }
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

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
                animator.SetBool("isIdle", true);
                break;
            case PlayerState.Running:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.Jumping:
                animator.SetBool("isJumping", true);
                break;
            case PlayerState.JumpFall:
                animator.SetBool("isFalling", true);
                break;
            case PlayerState.FreeFall:
                animator.SetBool("isFalling", true);
                break;
            case PlayerState.Dead:
                animator.SetBool("isDead", true);

                break;
            default: break;
        }
    }

    private void resetAnimatorParameters() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                animator.SetBool(parameter.name, false);
        }
    }

    public PlayerState GetState(){return playerState;}
   
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
