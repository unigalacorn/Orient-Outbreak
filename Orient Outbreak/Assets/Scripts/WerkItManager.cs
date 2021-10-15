using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Unya na nako ni limpyohon basta ma.okay na tong problema sa physics

public class WerkItManager : MonoBehaviour
{
    #region Variables

    // Marker
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;
    [SerializeField] Transform marker;

    float markerPosition;
    float markerDestination;

    float markerTimer;
    [SerializeField] float timerMultiplicator = 1f; // multiplicator for marker


    float markerSpeed;
    [SerializeField] float smoothMotion = 1f;

    // Hit Area
    [SerializeField] Transform topPivotHitArea;
    [SerializeField] Transform bottomPivotHitArea;

    [SerializeField] Transform hitArea;
    float hitAreaSizeTimer;

    [SerializeField] float hitAreaPosition;
    [SerializeField] float hitAreaSize = 0.15f; //0.1f
    [SerializeField] float hitAreaPower = 0.25f; // 1.0f
    float hitAreaProgress;
    [SerializeField] float hitAreaVelocity; // remove serializefield
    [SerializeField] float hitAreaPullPower = 0.01f;
    [SerializeField] float hitAreaGravityPower = 0.01f; //0.01f 0.003f
    [SerializeField] float hitAreaProgressDegradationPower = 0.1f;

    //Key Timer
    [SerializeField] float keyTimer;
    float keyToClick;
    [SerializeField] float keyTimerMultiplicator = 6f;
    

    //[SerializeField] SpriteRenderer hitAreaSpriteRenderer;
    [SerializeField] Renderer hitAreaRenderer;
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float failTimer = 10f;

    //UI
    [SerializeField] TextMeshProUGUI keyToClickTextUI;
    [SerializeField] TextMeshProUGUI levelTextUI;
    [SerializeField] TextMeshProUGUI startLevelTextUI;

    int currentLevel = 1;
    int lastLevel = 2;

    bool pause = false;
    bool isKeyHit;

    bool isRightInput = false;
    //bool isInputSpace = false;

    #endregion

    #region Unity Functions
    void Start()
    {
        Resize();
        StartLevel();
        //hitAreaVelocity = 0f;
    }

    void Update()
    {
        if (pause) { return; }
        Marker();
        KeyChange();
        HitArea();

        ProgressCheck();
        //HitAreaTimedResize();
        //Resize();
    }

    private void FixedUpdate()
    {
        //KeyHit();
    }
    #endregion

    private void StartLevel()
    {
        pause = true;
        levelTextUI.text = "Level " + currentLevel.ToString();

        startLevelTextUI.text = " Level " + currentLevel.ToString() + " BEGIN!";
        startLevelTextUI.gameObject.SetActive(true);

        // Reset values
        hitAreaProgress = 0f;

        StartCoroutine(WaitBeforeStart());
    }

    // Displays text or image animations before the level starts
    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(3);
        startLevelTextUI.gameObject.SetActive(false);
        pause = false;
    }

    void Resize()
    {
        Bounds b = hitAreaRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hitArea.localScale;
        float distance = Vector3.Distance(topPivotHitArea.position, bottomPivotHitArea.position);
        ls.y = (distance / ySize * hitAreaSize);
        hitArea.localScale = ls;
    }

    void Marker()
    {
        markerTimer -= Time.deltaTime;
        if (markerTimer <= 0f)
        {
            markerTimer = UnityEngine.Random.value * timerMultiplicator;

            //markerDestination = 0;
            markerDestination = UnityEngine.Random.value;

        }

        markerPosition = Mathf.SmoothDamp(markerPosition, markerDestination, ref markerSpeed, smoothMotion);
        marker.position = Vector3.Lerp(bottomPivot.position, topPivot.position, markerPosition);
    }

    void KeyChange()
    {
        // randomizes time for which key to click
        keyTimer -= Time.deltaTime;
        if (keyTimer <= 0f)
        {
            keyTimer = UnityEngine.Random.value * keyTimerMultiplicator;
            keyToClick = UnityEngine.Random.value;
        }
    }

    void KeyHit()
    {
        if(isRightInput)
        {
            hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
            // isKeyHit = false;



            //hitAreaVelocity -= hitAreaGravityPower * Time.fixedDeltaTime;
            //hitAreaPosition += hitAreaVelocity;

            //if (hitAreaPosition - hitAreaSize / 2 <= 0f && hitAreaVelocity < 0f)
            //{
            //    hitAreaVelocity = 0f;
            //}
            //if (hitAreaPosition + hitAreaSize / 2 >= 1f && hitAreaVelocity > 0f)
            //{
            //    hitAreaVelocity = 0f;
            //}

            //hitAreaPosition = Mathf.Clamp(hitAreaPosition, hitAreaSize / 2, 1 - hitAreaSize / 2);
            //hitArea.position = Vector3.Lerp(bottomPivotHitArea.position, topPivotHitArea.position, hitAreaPosition);


            isRightInput = false;
        }
    }

    void HitArea()
    {
        if (keyToClick > 0.5f) // space
        {
            //isRightInput = Input.GetKeyDown(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            keyToClickTextUI.text = "SPACE";
        }

        else if (keyToClick <= 0.5f) // W
        {
            //isRightInput = Input.GetKeyDown(KeyCode.W);
            if (Input.GetKeyDown(KeyCode.W))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            keyToClickTextUI.text = "W";
        }


        hitAreaVelocity -= hitAreaGravityPower * Time.fixedDeltaTime;
        hitAreaPosition += hitAreaVelocity;

        if (hitAreaPosition - hitAreaSize / 2 <= 0f && hitAreaVelocity < 0f)
        {
            hitAreaVelocity = 0f;
        }
        if (hitAreaPosition + hitAreaSize / 2 >= 1f && hitAreaVelocity > 0f)
        {
            hitAreaVelocity = 0f;
        }

        hitAreaPosition = Mathf.Clamp(hitAreaPosition, hitAreaSize / 2, 1 - hitAreaSize / 2);
        hitArea.position = Vector3.Lerp(bottomPivotHitArea.position, topPivotHitArea.position, hitAreaPosition);
    }

    //void HitAreaTimedResize()
    //{
    //    hitAreaSizeTimer -= Time.deltaTime;
    //    if (hitAreaSizeTimer <= 0f)
    //    {
    //        hitAreaSizeTimer = 10f; //UnityEngine.Random.value * timerMultiplicator;
    //        hitAreaSize = UnityEngine.Random.Range(0.2f,0.4f);
    //    }
    //}

    void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hitAreaProgress;
        progressBarContainer.localScale = ls;

        float min = hitAreaPosition - hitAreaSize / 2;
        float max = hitAreaPosition + hitAreaSize / 2;

        if(min < markerPosition && max > markerPosition)
        {
            hitAreaProgress += hitAreaPower * Time.deltaTime;
        }
        else
        {
            hitAreaProgress -= hitAreaProgressDegradationPower * Time.deltaTime;

            // Lose if time reaches 0
            failTimer -= Time.deltaTime;
            if (failTimer < 0f)
            {
                LoseLevel();
            }
        }

        if(hitAreaProgress >= 1f)
        {
            WinLevel();
        }

        hitAreaProgress = Mathf.Clamp(hitAreaProgress, 0f, 1f);
    }

    void WinLevel()
    {
        pause = true;

        // if won last level, Win
        if (currentLevel == lastLevel)
        {
            Win();
        }

        // else, go to next level
        else
        {
            currentLevel++;
            StartLevel();
        }
    }

    void Win()
    {
        pause = true;
        Debug.Log("You win!");

    }

    void LoseLevel()
    {
        pause = true;
        StartLevel();
        Debug.Log("You lose!");
    }
}
