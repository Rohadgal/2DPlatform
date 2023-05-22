using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState {
    None,
    Idle,
    Move,
    Attack
}
public class EnemyManager : MonoBehaviour
{
    EnemyState enemyState;
    
    public static EnemyManager instance;

   Animator animator;
    private void Awake() {
        
    }
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeEnemyState(EnemyState state)
    {
        enemyState = state;
        switch(enemyState)
        {
            case EnemyState.None:
            break;
            case EnemyState.Idle:
            break;
        }
    }
}
