using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController : MonoBehaviour
{
    #region Public
    public static EnemyController instance;
    public LayerMask whatIsGround;
    public Transform playerPosition;
    public float fireRange = 1f;
    public float viewAngle = 90f;
    #endregion

    #region Private
    Rigidbody2D rb2d;
    bool isFacingLeft, isGrounded; // isShooting
    float xMove, yMove, angleToPlayer;
    Vector2 aVector, bVector;

    ParticleSystem particles;

    Vector2 directionToPlayer;
    #endregion

    #region Serialize Fields
    [SerializeField] float xSpeed, jumpForce, footRadious;
    [SerializeField] Transform footPosition;
    #endregion

    private void Awake() {
        instance = this;
    }
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        particles = GetComponent<ParticleSystem>();
        isFacingLeft = true;
        isGrounded = false;
        //isShooting = false;

        aVector = new Vector2(1,0);
        bVector = new Vector2(-3,4);
        particles.Play();
        StartCoroutine(SelfDestroy());
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(footPosition.position, footRadious, whatIsGround);

        if(IsPlayerInFireRange())
        {
            //Debug.LogWarning("FireBall");
            flip();
        }
        Debug.Log("transform.right: "+transform.right);


    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public float Magnitud(Vector2 mag)
    {
        float x = mag.x * mag.x; 
        float y = mag.y * mag.y; 
        
        float sum = x + y;

        float resultado = Mathf.Sqrt(sum);
        return resultado;
    }

    public bool IsPlayerInFireRange()
    {
        Vector2 directionToPlayer = playerPosition.position -transform.position;

        float angleToPlayer = Mathf.Acos(Vector2.Dot( transform.right, directionToPlayer) / (Magnitud(transform.right) * Magnitud(directionToPlayer))) * Mathf.Rad2Deg;
        if(angleToPlayer <= viewAngle) // checar primero distancia del jugador antes de comenzar a calcular el ángulo al que se encuentra. Solución encontrado con ayuda de chatgpt.
        {
     

            
           // Debug.Log("angle to player: " + angleToPlayer);
            if(Vector2.Dot(transform.right, directionToPlayer) > 0)
            {
                return true;  
                
            }
        }
        return false;

    }

    void flip() {
        transform.Rotate(0, 180, 0);
        isFacingLeft = !isFacingLeft;
        //Debug.Log("SPIN");
    }
}

