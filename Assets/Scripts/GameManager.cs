using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class GameManager : MonoBehaviour
{
	public  Text score;

    public  delegate  void gameEvent();
    public  static  event  gameEvent OnPlayerDeath;
    public GameObject enemySpawnManager;
    private SpawnManager spawnManager;

    void Start() {
        spawnManager = enemySpawnManager.GetComponent<SpawnManager>();
    }

	private  int playerScore =  0;
    // public static event OnPlayerDeath;
	
	public  void  increaseScore(){
		playerScore  +=  1;
		score.text  =  "SCORE: "  +  playerScore.ToString();
        spawnManager.spawnNewEnemy();
	}

    public void damagePlayer()
    {
        OnPlayerDeath();
    }
}