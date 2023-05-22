using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject canvas;
    private void Awake() {
        if(canvas != null) canvas.SetActive(false);
    }
    private void Update() {
        if(PlayerManager.instance.GetState() == PlayerState.Dead)
        {
            GameOver();
        }
    }

    void GameOver(){
        if(canvas != null) canvas.SetActive(true);
    }
}
