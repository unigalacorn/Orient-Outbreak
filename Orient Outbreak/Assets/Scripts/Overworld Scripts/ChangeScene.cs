using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Minigame minigame;

    private void Start()
    {
        GameManager.instance.UpdateGameState(GameState.Cutscene);
    }

    public void OnButtonClickPlay()
    {
        if (minigame == Minigame.ImmunityBooster)
        {
            GameManager.instance.UpdateGameState(GameState.Minigame);
            SceneLoader.instance.ChangeScene("Immunity Booster Scene");
        }
        else if (minigame == Minigame.ShieldsUp)
        {
            GameManager.instance.UpdateGameState(GameState.Minigame);
            SceneLoader.instance.ChangeScene("ShieldsUp Scene");
        }
        else if (minigame == Minigame.WerkIt)
        {
            GameManager.instance.UpdateGameState(GameState.Minigame);
            SceneLoader.instance.ChangeScene("WerkIt Scene");
        }
    }
}
