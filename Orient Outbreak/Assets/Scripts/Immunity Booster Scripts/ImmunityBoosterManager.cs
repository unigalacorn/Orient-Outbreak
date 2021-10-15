using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImmunityBoosterManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private Transform fruitHolderTransform;
    [SerializeField] private float tempo;
    private bool hasStarted;

    [Header("Score System")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text comboText;
    private int comboCounter;
    private int scoreCounter;
    #endregion

    #region Unity Methods
    private void Start()
    {
        tempo = tempo / 60f;


        //Setup Score and Combo
        scoreCounter += 0;
        comboCounter += 0;

        scoreText.text = "SCORE: " + scoreCounter;
        comboText.text = "COMBO: " + comboCounter;
    }

    private void Update()
    {
        if (hasStarted)
        {
            fruitHolderTransform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f);
        }
        else if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(StartSoundtrack());
        }
    }
    #endregion

    #region Coroutines
    IEnumerator StartSoundtrack()
    {
        yield return new WaitForSeconds(0.1f);
        soundtrack.Play();
    }
    #endregion

    #region Public Methods
    public void catchFruit()
    {
        scoreCounter += 50;
        comboCounter += 1;

        scoreText.text = "SCORE: " + scoreCounter;
        comboText.text = "COMBO: " + comboCounter;
    }

    public void missFruit()
    {
        comboCounter = 0;

        comboText.text = "COMBO: " + comboCounter;
    }

    public void loadOverworldScene()
    {
        GameManager.instance.UpdateGameState(GameState.Exploration);
        SceneManager.LoadScene("Overworld Scene");
    }
    #endregion
}
