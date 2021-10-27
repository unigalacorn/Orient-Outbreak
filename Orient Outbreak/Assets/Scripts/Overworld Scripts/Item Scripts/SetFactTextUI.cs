using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFactTextUI : MonoBehaviour
{
    [SerializeField] private Text factText;

    private void Start()
    {
        GameManager.instance.UpdateGameState(GameState.Dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            GameManager.instance.UpdateGameState(GameState.Exploration);
        }
    }

    public void SetFactText(string _text)
    {
        factText.text = _text;
    }

}

