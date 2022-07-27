
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickTrigger : MonoBehaviour
{
	TicTacToeAI _ai;

	[SerializeField]
	private int _myCoordX = 0;
	[SerializeField]
	private int _myCoordY = 0;

	[SerializeField]
	public bool canClick;


	[SerializeField, HideInInspector]
	private List<int> _choices = new List<int>();


	private void Awake()
	{
		_ai = FindObjectOfType<TicTacToeAI>();
	}

	private void Start(){

		_ai.onGameStarted.AddListener(AddReference);
		_ai.onGameStarted.AddListener(() => SetInputEndabled(true));
		_ai.onPlayerWin.AddListener((win) => SetInputEndabled(false));
		
	}

	private void SetInputEndabled(bool val){
		canClick = val;
	}


	private void AddReference()
	{
		_ai.RegisterTransform(_myCoordX, _myCoordY, this);
		canClick = true;
	}

	private void OnMouseDown()
	{
		StopCoroutine(ShowPattern());
		if (canClick){
			SetInputEndabled(false);
			_ai.PlayerSelects(_myCoordX, _myCoordY);
		}
	}

	private void OnMouseUp()
	{
		_choices.Clear();
        if (!this.canClick)
        {
			StartCoroutine(ShowPattern());
        }
	
	}

	private IEnumerator ShowPattern()
	{
		yield return new WaitForSeconds(0.5f);
		List<int> _c = _ai.Choices(_choices);
		Debug.Log(_ai._cube.Length);
		if ( _c.Count > 0)
        {
			int rd = _c[Random.Range(0, _c.Count)];
			Debug.Log(_c.Count);
			ClickTrigger _tcube = _ai._cube[rd].GetComponent<ClickTrigger>();
			_ai.AiSelects(_tcube._myCoordX, _tcube._myCoordY);
			_tcube.canClick = false;

		}
		// etc
	}




	private void playerVerify()
	{
		if (!canClick)
		{
			_ai.AiSelects(_myCoordX, _myCoordY);

			SetInputEndabled(true);
		}
	}
	
}
