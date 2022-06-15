using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{

    private bool broken = false;

    public GameObject prefab;
    public GameObject coinPrefab;

    public AudioSource breakAudio;

    public  GameConstants gameConstants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") &&  !broken){
            broken  =  true;
            breakAudio.Play();
            // assume we have 5 debris per box
            for (int x =  0; x<5; x++){
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            Instantiate(coinPrefab, new  Vector3(transform.position.x, transform.position.y  +  1.0f, transform.position.z), Quaternion.identity);
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled  =  false;
            // Destroy(gameObject.transform.parent);
        }
    }
}
