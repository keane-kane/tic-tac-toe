using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] gamePlanes;

    [SerializeField]
    private Button[] grid_btn;

    private RetryButton _retryButton;

    private EndMessage _endMessage;
    // Start is called before the first frame update
    void Start()
    {
        _retryButton = FindObjectOfType<RetryButton>();
        _retryButton.GetComponent<Button>().gameObject.SetActive(false);

        _endMessage = FindObjectOfType<EndMessage>();
        _endMessage.GetComponent<TMP_Text>().gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
