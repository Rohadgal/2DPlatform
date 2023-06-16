using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Public
    public static EnemyController instance;
    //public LayerMask whatIsGround;
    public Transform playerPosition;
    //public float fireRange = 1f;
    public float viewAngle = 90f;
 
    #endregion

    #region Private
    //Rigidbody2D rb2d;
    bool isFacingLeft, isGrounded; // isShooting
    float angleToPlayer;

    ParticleSystem particles;

    Vector2 directionToPlayer;
    #endregion

    void Start() {
   
        particles = GetComponent<ParticleSystem>();
   
        //particles.Play();
        StartCoroutine(SelfDestroy());
    }

    private void FixedUpdate() {
       // isGrounded = Physics2D.OverlapCircle(footPosition.position, footRadious, whatIsGround);
            //Debug.LogWarning("FireBall");
            flip();

        //Debug.Log("transform.right: "+transform.right);
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(LevelManager.s_instance.getSecondsToWait());
        gameObject.SetActive(false);
    }

    public float Magnitud(Vector2 mag)
    {
        float x = mag.x * mag.x; 
        float y = mag.y * mag.y; 
        
        float sum = x + y;

        float resultado = Mathf.Sqrt(sum);
        return resultado;
    }

    void flip() {
        if(GameManager.s_instance.getGameState() == GameState.GameOver) {
            return;
        }
        Vector2 directionToPlayer = PlayerController.instance.transform.position - transform.position;

        float angleToPlayer = Mathf.Acos(Vector2.Dot(transform.right, directionToPlayer) / (Magnitud(transform.right) * Magnitud(directionToPlayer))) * Mathf.Rad2Deg;
        //Debug.LogWarning("angle to player: " + angleToPlayer);
        if (angleToPlayer <= viewAngle) // checar primero distancia del jugador antes de comenzar a calcular el ángulo al que se encuentra. Solución encontrado con ayuda de chatgpt.
        {
            if (Vector2.Dot(transform.right, directionToPlayer) > 0) {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}

