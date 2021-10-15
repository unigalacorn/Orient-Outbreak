using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle1;
/*    [SerializeField] private GameObject collectible1;
    [SerializeField] private GameObject collectible2;
    [SerializeField] private GameObject collectible3;*/

    int chooseObs;

    float maxTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            GenerateObstacle();
            timer = 0; //generates obstacles every second
        }
    }

    void GenerateObstacle()
    {
        GameObject newObstacle = Instantiate(obstacle1); 
    }
}
