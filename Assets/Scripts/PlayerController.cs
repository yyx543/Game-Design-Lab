using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float upSpeed;
    public Transform enemyLocation;
    public Transform startLimit; //GameObject that indicates start of map
    public Transform endLimit; // GameObject that indicates end of map
    public Text scoreText;
    public ParticleSystem dustCloud;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;

    private bool onGroundState = true;
    private bool faceRightState = true;

    private int score = 0;
    // private bool countScoreState = false;

    private Animator marioAnimator;
    private AudioSource marioAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAnimator.SetBool("onGround", onGroundState);
        marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        
        // if (!onGroundState && countScoreState)
        // {
        //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //     {
        //         countScoreState = false;
        //         score++;
        //         Debug.Log(score);
        //     }
        // }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void  FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
        }
        
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            // countScoreState = true; //check if Gomba is underneath
        }

        if (marioBody.position.x <= startLimit.transform.position.x) {
            marioBody.position = new Vector2(startLimit.transform.position.x, marioBody.position.y);
        } else if (marioBody.position.x >= endLimit.transform.position.x) {
            marioBody.position = new Vector2(endLimit.transform.position.x, marioBody.position.y);
        }

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            dustCloud.Play();
            // countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            marioBody.velocity = Vector2.zero;
            SceneManager.LoadScene("SampleScene");
        }
    }

    void  PlayJumpSound() {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
