using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TicTacToeState{none, cross, circle}

[System.Serializable]
public class WinnerEvent : UnityEvent<int>
{
}

public class TicTacToeAI : MonoBehaviour
{

	int _aiLevel;

	TicTacToeState[,] boardState;

	[SerializeField]
	private bool _isPlayerTurn { get; set; }

	[SerializeField]
	private int _cubeSize = 3;
	
	[SerializeField]
	private TicTacToeState playerState = TicTacToeState.cross;
	TicTacToeState aiState = TicTacToeState.circle;

	[SerializeField]
	private GameObject _xPrefab;

	[SerializeField]
	private GameObject _oPrefab;


	[SerializeField]
	public GameObject[] _cube;


	[SerializeField, HideInInspector]
	public List<GameObject> _grid = new List<GameObject>();
	
	public UnityEvent onGameStarted;

	//Call This event with the player number to denote the winner
	public WinnerEvent onPlayerWin;

	public UnityEvent onGameEnd;

	ClickTrigger[,] _triggers;
	
	private void Awake()
	{
		if(onPlayerWin == null){
			onPlayerWin = new WinnerEvent();
		}
	}

	public void StartAI(int AILevel){
		_aiLevel = AILevel;
		_isPlayerTurn = true;
		StartGame();
	}

	public void RegisterTransform(int myCoordX, int myCoordY, ClickTrigger clickTrigger)
	{
		_triggers[myCoordX, myCoordY] = clickTrigger;
	}

	private void StartGame()
	{
		_triggers = new ClickTrigger[3,3];
		onGameStarted.Invoke();

	}

	public void PlayerSelects(int coordX, int coordY){
		if (_isPlayerTurn)
        {
			SetVisual(coordX, coordY, playerState);
			_isPlayerTurn = false;

		}
	}

	public void AiSelects(int coordX, int coordY){
		if (!_isPlayerTurn)
		{ 
			SetVisual(coordX, coordY, aiState);
			_isPlayerTurn = true;
			Debug.Log("AiSelects");
		}
		
	}

	private void SetVisual(int coordX, int coordY, TicTacToeState targetState)
	{
		Instantiate(
			targetState == TicTacToeState.circle ? _oPrefab : _xPrefab,
			_triggers[coordX, coordY].transform.position,
			Quaternion.identity
		);
	}


	public List<int> Choices(List<int> choices)
    {
		for (int i = 0; i < _cube.Length; i++)
        {
			ClickTrigger _tttcube =  _cube[i].GetComponent<ClickTrigger>();
            if (_tttcube.canClick)
            {
				choices.Add(i);
            }
		}

		return choices;

	}
	private bool PlayerVictory(GameObject id)
	{
		if ((_grid[0] == id && _grid[1] == id && _grid[2] == id) ||
			(_grid[3] == id && _grid[4] == id && _grid[5] == id) ||
			(_grid[6] == id && _grid[7] == id && _grid[8] == id) ||

			(_grid[0] == id && _grid[3] == id && _grid[6] == id) ||
			(_grid[1] == id && _grid[4] == id && _grid[7] == id) ||
			(_grid[2] == id && _grid[5] == id && _grid[8] == id) ||

			(_grid[0] == id && _grid[4] == id && _grid[8] == id) ||
			(_grid[2] == id && _grid[4] == id && _grid[6] == id)
			)
		{
			return true;
		}

		return false;
	}
}
