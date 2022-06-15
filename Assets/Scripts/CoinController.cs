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
            CentralManager.centralManagerInstance.increaseScore();
            CentralManager.centralManagerInstance.increaseScore();
            
            // StartCoroutine(consumeSequence());
        }
    }

    // IEnumerator consumeSequence(){
	// 	Debug.Log("consume starts");

	// 	float scaleUp = 0.8f;
    //     float scaleDown = -0.5f;

    //     this.transform.localScale = new Vector3(this.transform.localScale.x + scaleUp, this.transform.localScale.y + scaleUp, this.transform.localScale.z);
    //     this.transform.localScale = new Vector3(this.transform.localScale.x + scaleDown, this.transform.localScale.y + scaleDown, this.transform.localScale.z);
    //     yield return null;
    //     this.transform.localScale = new Vector3(0, 0, 0);
        
	// 	Debug.Log("consume ends");
	// 	this.gameObject.SetActive(false);

	// 	yield  break;
	// }

    // void PlayCoinPickupSound() {
    //     coinAudio.PlayOneShot(coinAudio.clip);
    // }

}