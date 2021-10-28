using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneLoader.instance.ChangeScene("Overworld Scene");
        //SceneManager.LoadScene("Overworld Scene");
    }
}
