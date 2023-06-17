using UnityEngine;

/// <summary>
/// Se encarga de todo el control del player, pero no de la l�gica de juego
/// </summary>
public class PlayerController : MonoBehaviour {

    #region Public
    /// <summary>
    /// Instancia singleton de PlayerController
    /// </summary>
    public static PlayerController instance;
    public LayerMask whatIsGround;
    public AudioSource audioSource;
    #endregion

    #region Private
    Rigidbody2D rb2d;
    bool isFacingRight, isGrounded, isShooting;
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
        audioSource = GetComponent<AudioSource>();
        isFacingRight = true;
        isGrounded = false;
        isShooting = false;   
    }

    private void FixedUpdate() {
        if(PlayerManager.instance.GetState() != PlayerState.Dead){
            isGrounded = Physics2D.OverlapCircle(footPosition.position, footRadious, whatIsGround) && rb2d.velocity.y < 0.1f;
            HorizontalMovement();
            verticalMovement();
            return;
        } 
        rb2d.velocity = Vector2.zero;
        //rb2d.gravityScale = 0f;
    }

    private void HorizontalMovement() {
        float xMove = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(xMove * xSpeed, rb2d.velocity.y);
        if ((xMove < 0 && isFacingRight) || (xMove > 0 && !isFacingRight)) {
            flip();
        }
        if (isGrounded) {
            if (xMove != 0) {
                PlayerManager.instance.ChangePlayerState(PlayerState.Running);
            } else if (xMove == 0) {
                PlayerManager.instance.ChangePlayerState(PlayerState.Idle);
            }
        }
    }

    void verticalMovement() {
        if (isGrounded) {
            return;
        }
        if (rb2d.velocity.y >= 0.1) {
            PlayerManager.instance.ChangePlayerState(PlayerState.Jumping);
        } else if (rb2d.velocity.y < 0.1) {
            PlayerManager.instance.ChangePlayerState(PlayerState.JumpFall);
        }   
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            jump();
        }

        //Debug.Log("IS grounded: " + isGrounded);
    }
    void jump() {
        if (!isGrounded) {
            return;
        }
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        audioSource.Play();
    }

    void flip() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(footPosition.position, footRadious);
    }

     void Shoot(){
        isShooting = !isShooting;
    }

    
}
