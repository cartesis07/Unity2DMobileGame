using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Movement : MonoBehaviour
{
    public ParticleSystem moveParticles;

    public int LEVEL;
    public Text levelCountText;

    public bool ISTRIGGER;

    public float moveSpeed;
    public float JumpForceStart;
    public float JumpForce;
    public float JumpForceSide;

    public float sensitivity;

    public bool changeScene;

    public bool isJumpingUp;
    public bool isJumpingDown;

    public bool isJumpingRight;
    public bool isJumpingLeft;
    public bool isGroundedDown;
    public bool isGroundedUp;

    public bool isSidedRight;
    public bool isSidedLeft;

    public Transform groundCheckDown;
    public Transform groundCheckUp;
    public float checkRadius;
    public LayerMask collisionLayersGround;

    public Transform sideCheckRight;
    public Transform sideCheckLeft;
    public LayerMask collisionLayersSide;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public float speed;

    private Vector3 velocity = Vector3.zero; //référence à la vélocité, nomenclature Unity
    private float horizontalMovement;

    public float player_position_y;
    Vector3 lastPosition = Vector3.zero;

    public float gravity;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            //Do your stuff here
            LEVEL = 1;
            SavePlayer();
        }
        else
        {
            Debug.Log("NOT First Time Opening");
            //Do your stuff here
            LoadPlayer();
        }
    }

    private void Start()
    {
        ISTRIGGER = false;

        Application.targetFrameRate = 40; //je set le framerate pour conserver le même saut
        JumpForce = 400;
        JumpForceStart = 400;
        //For computer test : 400
        //For Android : 150 plutôt 120 depuis le bug

        JumpForceSide = 400;
        //For computer test : 300, 400 même ?
        //For Android : 150

        sensitivity = 2.0f;
    }

    //fonction update réservée à la physique
    void FixedUpdate()
    {
        MovePlayer(horizontalMovement);

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; //touches horizontales par défaut
        //horizontalMovement = sensitivity*Input.acceleration.x * moveSpeed * Time.deltaTime; //touches horizontales par défaut

        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown.position, checkRadius, collisionLayersGround);
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, checkRadius, collisionLayersGround);

        isSidedRight = Physics2D.OverlapCircle(sideCheckRight.position, checkRadius, collisionLayersSide);
        isSidedLeft = Physics2D.OverlapCircle(sideCheckLeft.position, checkRadius, collisionLayersSide);
    }

    //fonction update réservée aux choses non physiques
    void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && (isGroundedDown) && (rb.gravityScale == 1.0f))//par défaut barre espace
        {
            isJumpingUp = true;
        }
        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && (isGroundedUp) && (rb.gravityScale == -1.0f)) //par défaut barre espace
        {
            isJumpingDown = true;
        }

        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && (isSidedLeft))
        {
            isJumpingRight = true;
        }

        if ((Input.GetButtonDown("Jump") || Input.touchCount > 0) && (isSidedRight))
        {
            isJumpingLeft = true;
        }

        player_position_y = this.transform.position.y + 2.622f;
        JumpForce = JumpForceStart + 1.5f*player_position_y;

        if (isGroundedDown)
        {
            createParticles();
        }
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        // .05f = SmoothTime échelle de temps sur laquelle le personnage effectue son déplacement

        if (isJumpingUp)
        {
             rb.AddForce(new Vector2(0f, JumpForce));
            isJumpingUp = false;
        }
        if (isJumpingDown)
        {
            rb.AddForce(new Vector2(0f, -JumpForce));
            isJumpingDown = false;

        }

        if (isJumpingLeft)
        {
             rb.AddForce(new Vector2(-JumpForceSide, JumpForce/1.5f));
             isJumpingLeft = false;
        }
        if (isJumpingRight)
        {
              rb.AddForce(new Vector2(JumpForceSide, JumpForce/1.5f));
              isJumpingRight = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckDown.position, checkRadius);
        Gizmos.DrawWireSphere(groundCheckUp.position, checkRadius);

        Gizmos.DrawWireSphere(sideCheckLeft.position, checkRadius);
        Gizmos.DrawWireSphere(sideCheckRight.position, checkRadius);
    }

   void createParticles()
    {
        moveParticles.Play();
    }

   public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.loadPlayer();
        LEVEL = data.level;
    }

    public void AddLvl()
    {
        LEVEL++;
        levelCountText.text = LEVEL.ToString();
    }

    public void DisplayLvl()
    {
        levelCountText.text = LEVEL.ToString();
    }

    public void Menu()
    {
        levelCountText.text = "";
    }

    public void Settings()
    {
        levelCountText.text = "Settings";
    }
}