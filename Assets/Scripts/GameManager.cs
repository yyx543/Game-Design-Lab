using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
	public  Text score;

    
    public GameObject enemySpawnManager;
    private SpawnManager spawnManager;

        
    // Singleton Pattern
    private  static  GameManager _instance;
    // Getter
    public  static  GameManager Instance
    {
        get { return  _instance; }
    }

    private  int _healthPoints; 

    //healthPoints is a basic property  
    public  int healthPoints { 
        get { 
            //Some other code  
            // ...
            return _healthPoints; 
        } 
        set { 
            // Some other code, check etc
            // ...
            _healthPoints = value; // value is the amount passed by the setter
        } 
    }

    void Start() {
        spawnManager = enemySpawnManager.GetComponent<SpawnManager>();
    }

	private  int playerScore =  0;
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
        spawnManager.spawnNewEnemy();
	}

    public  void  coinIncreaseScore(){
		playerScore  +=  2;
		score.text  =  "SCORE: "  +  playerScore.ToString();
        spawnManager.spawnNewEnemy();
	}

    public  delegate  void gameEvent();
    public  static  event  gameEvent OnPlayerDeath;
    
    public void damagePlayer()
    {
        OnPlayerDeath();
    }
}