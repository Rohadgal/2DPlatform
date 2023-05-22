using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Public
    public static SoundManager instance;
    public AudioClip EnemySpawnClip;

    public AudioClip BackgroundMusic;
    #endregion

    // Start is called before the first frame update
    private void Awake() {
        instance = this;
    }
    private void Start() {
        if(EnemySpawnClip != null)
        {   
           
        }
    }

}
