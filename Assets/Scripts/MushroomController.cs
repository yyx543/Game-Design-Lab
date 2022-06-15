using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D mushroomBody;

    private Vector2 currentPosition;
    private Vector2 currentDirection = new Vector2(1,0);

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
        mushroomBody.MovePosition(nextPosition);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Pipe")) {
            currentDirection *= -1;
        }
        
        if (col.gameObject.CompareTag("Player")) {
            speed = 0;
            //set the mushroom to be invisible then can trigger onBecameInvisible
            gameObject.SetActive(false);

        }
    }

    void OnBecameInvisible() {
        Debug.Log("mushroom hit");
        Destroy(gameObject);
    }
}
