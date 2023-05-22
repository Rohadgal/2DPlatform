using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadzoneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerManager.instance.ChangePlayerState(PlayerState.Dead);
        }
        
    }
}
