using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : Singleton<GameManager>

{
	public Text score;
    public Image Panel;
    public Button Button;

    
    public GameObject enemySpawnManager;
    private SpawnManager spawnManager;

        
    // Singleton Pattern
    private  static  GameManager _instance;
    // Getter
    public  static  GameManager Instance
    {
        get { return  _instance; }
    }

    override  public  void  Awake(){
		base.Awake();
		Debug.Log("awake called");
		// other instructions...
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