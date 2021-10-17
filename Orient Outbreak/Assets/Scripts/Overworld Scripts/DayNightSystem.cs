using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class DayNightSystem : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private TextMeshProUGUI dayDisplay;
    [SerializeField] private TextMeshProUGUI dayCycleDisplay;
    [SerializeField] private Light2D globalLight;

    [Header("Cycle Colors")]
    [SerializeField] private Color sunrise;
    [SerializeField] private Color day;
    [SerializeField] private Color sunset;
    [SerializeField] private Color night;
    [SerializeField] private Color midnight; 

    private GameObject[] mapLights; // enable/disable in day/night states

    private float percent;
    #endregion

    #region Unity Methods
    void Start()
    {
        mapLights = GameObject.FindGameObjectsWithTag("Light");
        DisplayDayCycle();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentState == GameState.Exploration)
        {
            GameManager.instance.SetCycleCurrentTime(GameManager.instance.cycleCurrentTime += Time.deltaTime);

            if (GameManager.instance.cycleCurrentTime >= GameManager.instance.cycleMaxTime)     //If cycleMaxTime is reached increment day cycle
            {
                GameManager.instance.SetCycleCurrentTime(0);
                GameManager.instance.SetDayCycle((int)(GameManager.instance.dayCycle += 1));

                if ((int)GameManager.instance.dayCycle != 6)
                    DisplayDayCycle();
            }

            DayNightCycle();
        }
    }
    #endregion

    #region Private Methods
    private void DayNightCycle()
    {
        // If reach final state, go back to the first state
        if (GameManager.instance.dayCycle == DayCycles.Midnight && GameManager.instance.cycleCurrentTime == 0f)
        {
            GameManager.instance.SetDay(GameManager.instance.day + 1);
        }
        else if (GameManager.instance.dayCycle > DayCycles.Midnight)
        {
            GameManager.instance.SetDayCycle(0);
            DisplayDayCycle();
        }
            

        percent = GameManager.instance.cycleCurrentTime / GameManager.instance.cycleMaxTime;

        switch (GameManager.instance.dayCycle)
        {
            case DayCycles.Sunrise:
                globalLight.color = Color.Lerp(midnight, sunrise, percent);
                //if (GameManager.instance.cycleCurrentTime > GameManager.instance.cycleMaxTime / 2)
                ControlLightMaps(false);    // turn off lights when its bright
                break;
            case DayCycles.Morning:
                globalLight.color = Color.Lerp(sunrise, day, percent);
                ControlLightMaps(false);    //for testing purposes
                break;
            case DayCycles.Afternoon:
                globalLight.color = day;
                ControlLightMaps(false);    //for testing purposes
                break;
            case DayCycles.Sunset:
                globalLight.color = Color.Lerp(day, sunset, percent);
                ControlLightMaps(false);
                break;
            case DayCycles.Night:
                globalLight.color = Color.Lerp(sunset, night, percent);
                if (GameManager.instance.cycleCurrentTime > GameManager.instance.cycleMaxTime/2)
                    ControlLightMaps(true);     //turn on lights when its dark
                break;
            case DayCycles.Midnight:
                globalLight.color = Color.Lerp(night, midnight, percent);
                ControlLightMaps(true);     //for testing purposes
                break;
        }
    }

    private void ControlLightMaps(bool status)
    {
        // enable/disable all lights in scene
        if (mapLights.Length > 0)
            foreach (GameObject _light in mapLights)
                _light.gameObject.SetActive(status);
    }

    private void DisplayDayCycle()
    {
        dayDisplay.text = "Day: " + GameManager.instance.day;
        dayCycleDisplay.text = "" + GameManager.instance.dayCycle;
    }
    #endregion
}
public enum DayCycles
{
    Sunrise = 0,
    Morning = 1,
    Afternoon = 2,
    Sunset = 3,
    Night = 4,
    Midnight = 5
}
