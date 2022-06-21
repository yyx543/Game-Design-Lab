using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
	  
	// other components and interal state
    private float speed = 130.0f;

    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private bool isDead = false;

    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private Animator marioAnimator;

    public AudioClip marioJumpAudioClip;
    public AudioClip marioDieAudioClip;

    private GameObject cameraManager;
     // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAnimator.SetBool("onGround", onGroundState);
        GameManager.OnPlayerDeath  +=  PlayerDiesSequence;

        cameraManager = GameObject.Find("Main Camera");

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        force = gameConstants.playerDefaultForce;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
        }

        if (Input.GetKeyDown("x")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
        }

        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            // Check velocity
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }

        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            // Check velocity
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

  // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate() {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                marioBody.AddForce(movement * speed);
        } else {
            // stop horizontal movement
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }

        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            PlayJumpSound();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (onGroundState == false && (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles")))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            // dustCloud.Play();
            // countScoreState = false; // reset score state
            // scoreText.text = "Score: " + score.ToString();
        }
        //if mario hits pipe
        if (col.gameObject.CompareTag("Pipe")) 
        {
            // pipeAudio.Play();
        }

    }


    void PlayJumpSound(){
        GetComponent<AudioSource>().PlayOneShot(marioJumpAudioClip);
    }

    void PlayDieSound() {
        GetComponent<AudioSource>().PlayOneShot(marioDieAudioClip);
    }

    public void PlayerDiesSequence()
    {
        Debug.Log("mario died");
        isDead = true;
        marioAnimator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 30;
        StartCoroutine(dead());
    }
    
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }
}