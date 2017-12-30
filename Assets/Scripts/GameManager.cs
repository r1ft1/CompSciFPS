using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private void Start () {
		BeginGame();
	}
	
	private void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			RestartGame();
		}
	}


	//Prefab reference
	public Maze mazePrefab;
	//Holds instance of Prefab
	private Maze mazeInstance;

	private void BeginGame() {
		//Casted to 'Maze' in order to save in Maze mazeInstance
		mazeInstance = Instantiate(mazePrefab) as Maze;
		StartCoroutine(mazeInstance.Generate());
	}

	private void RestartGame() {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}
}
