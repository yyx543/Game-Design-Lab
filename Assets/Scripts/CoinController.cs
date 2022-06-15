using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private AudioSource coinAudio;

    // Start is called before the first frame update
    void Start()
    {
        coinAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            Debug.Log("Hit Coin");
            coinAudio.Play();
            GetComponent<SpriteRenderer>().enabled  =  false;
            GetComponent<BoxCollider2D>().enabled  =  false;
            CentralManager.centralManagerInstance.coinIncreaseScore();
        }
    }
}