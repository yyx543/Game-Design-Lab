using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerEV : MonoBehaviour
{
    private float originalX;
    private int moveRight = 1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;

    private bool faceRightState = true;
    public  GameConstants gameConstants;

    // events to subscribe
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        // get the starting position
        originalX = transform.position.x;

		moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
		enemySprite.flipX = !(moveRight==1);

        ComputeVelocity();
    }

    void ComputeVelocity() {
        velocity = new Vector2((moveRight)*gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveGomba() {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x  -  originalX) <  gameConstants.maxOffset) {
			MoveGomba();
		} else {
			// change direction
			moveRight  *=  -1;
			enemySprite.flipX = !enemySprite.flipX;
			ComputeVelocity();
			MoveGomba();
		}
    }

	void  OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Pipe" || other.gameObject.tag == "Obstacles") {
			moveRight *= -1;
			enemySprite.flipX = !enemySprite.flipX;
			ComputeVelocity();
		}

        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
                onEnemyDeath.Invoke();
            }
            else
            {
                // hurt player
                onPlayerDeath.Invoke();
            }
        }
	}

	void  KillSelf(){
		// enemy dies
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

	IEnumerator  flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper =  1.0f/(float) steps;

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  +  GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield  break;
	}

    // animation when player is dead
    void  EnemyRejoice(){
        Debug.Log("Enemy killed Mario");
        moveRight = 0;
		ComputeVelocity();
		InvokeRepeating("FlipXpos", 0, 0.2f);
	}
	void FlipXpos() {
        if (enemySprite.flipX) {
            enemySprite.flipX = false;
        } else {
            enemySprite.flipX = true;
        }
    }

    	// callbacks must be PUBLIC
    public void PlayerDeathResponse()
    {
        // GetComponent<Animator>().SetBool("playerIsDead", true);
        velocity = Vector3.zero;
    }
}
