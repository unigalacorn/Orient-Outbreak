using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneLoader.instance.ChangeScene("Overworld Scene");
    }
}
