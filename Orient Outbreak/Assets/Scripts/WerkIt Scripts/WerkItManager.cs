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
    //[SerializeField] float hitAreaPullPower = 0.01f;
    [SerializeField] float hitAreaGravityPower = 0.01f; //0.01f 0.003f
    [SerializeField] float hitAreaProgressDegradationPower = 0.1f;

    //Key Timer
    [SerializeField] float keyTimer;
    float keyToClick;
    [SerializeField] float keyTimerMultiplicator = 6f;
    

    //[SerializeField] SpriteRenderer hitAreaSpriteRenderer;
    [SerializeField] Renderer hitAreaRenderer;
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float failTimer;
    [SerializeField] float failTimerTotal = 60f;

    // Text UI
    //[SerializeField] TextMeshProUGUI keyToClickTextUI;
    [SerializeField] TextMeshProUGUI levelTextUI;
    [SerializeField] TextMeshProUGUI startLevelTextUI;
    [SerializeField] TextMeshProUGUI failTimerUI;
    [SerializeField] TextMeshProUGUI scoreUI;

    // Other UI
    [SerializeField] private GameObject keyUI;
    [SerializeField] private GameObject startLevelPanelUI;

    // Menu UI
    [SerializeField] private GameObject finishGamePanelUI;
    [SerializeField] private Text scoreFinishGamePanel;

    // Score
    int currentScore;
    int totalScore;

    // Level 
    int currentLevel = 1;
    int lastLevel = 3;
    
    // Bools
    bool pause = false;
    bool isKeyHit;
    bool isRightInput = false;

    //for Resizing hit Area
    Bounds b;
    float ySize;
    Vector3 ls;
    float distance;

    // Key Animators
    public Animator keyAnimator;//


    #endregion

    #region Unity Functions
    void Start()
    {
        //For resizing hitArea
        b = hitAreaRenderer.bounds;
        ySize = b.size.y;
        ls = hitArea.localScale;
        distance = Vector3.Distance(topPivotHitArea.position, bottomPivotHitArea.position);

        totalScore = 0;

        StartLevel();
        //hitAreaVelocity = 0f;
    }

    void Update()
    {
        failTimerUI.text = "TIME: " + ((int) failTimer).ToString();

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
        if (pause) { return; }
        ////////////////////////////////////
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

        //hitAreaPosition = Mathf.Clamp(hitAreaPosition, hitAreaSize / 2, 1 - hitAreaSize / 2);
        //hitArea.position = Vector3.Lerp(bottomPivotHitArea.position, topPivotHitArea.position, hitAreaPosition);
        /////////////////////////////////////

    }
    #endregion

    private void StartLevel()
    {
        pause = true;
        failTimer = failTimerTotal;
        levelTextUI.text = "Level " + currentLevel.ToString();

        startLevelTextUI.text = " Level " + currentLevel.ToString() + " BEGIN!";
        startLevelPanelUI.gameObject.SetActive(true);
        keyUI.gameObject.SetActive(false);

        // Reset values
        hitAreaProgress = 0f;
        

        scoreUI.text = "Score: " + totalScore;

        //// Levels
        if (currentLevel == 1)
        {
            hitAreaSize = 0.25f;
            currentScore = 2000;
        }

        else if (currentLevel == 2)
        {
            hitAreaSize = 0.22f;
            currentScore = 3500;
        }

        else if (currentLevel == 3)
        {
            hitAreaSize = 0.18f;
            currentScore = 4500;
        }

        Resize();

        StartCoroutine(WaitBeforeStart());
    }

    // Displays text or image animations before the level starts
    IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(3);
        startLevelPanelUI.gameObject.SetActive(false);
        keyUI.gameObject.SetActive(true);
        pause = false;
    }

    void Resize()
    {
        ls.y = (distance / ySize * hitAreaSize);
        hitArea.localScale = ls;
        Debug.Log(hitArea.localScale);
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
        if (keyToClick < 0.25f) // W
        {
            //isRightInput = Input.GetKeyDown(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.UpArrow))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            //keyToClickTextUI.text = "W";
            this.keyAnimator.SetTrigger("W-Key");
            //this.keyAnimator.runtimeAnimatorController = wAnimator as RuntimeAnimatorController;
            //keyAnimator.runtimeAnimatorController = Resources.Load("Assets/Animation/WerkIt/W-Key") as RuntimeAnimatorController;
        }
        else if (keyToClick >= 0.25f && keyToClick < 0.5f) // A
        {
            //isRightInput = Input.GetKeyDown(KeyCode.W);
            if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.LeftArrow))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            //keyToClickTextUI.text = "A";
            this.keyAnimator.SetTrigger("A-Key");
            //this.keyAnimator.runtimeAnimatorController = aAnimator as RuntimeAnimatorController;
            //keyAnimator.runtimeAnimatorController = Resources.Load("Assets/Animation/WerkIt/A-Key") as RuntimeAnimatorController;
        }

        else if (keyToClick >= 0.5f && keyToClick < 0.75f) // S
        {
            //isRightInput = Input.GetKeyDown(KeyCode.W);
            if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.DownArrow))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            //keyToClickTextUI.text = "S";
            this.keyAnimator.SetTrigger("S-Key");
        }

        else if (keyToClick >= 0.75f) // D
        {
            //isRightInput = Input.GetKeyDown(KeyCode.W);
            if (Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.RightArrow))
            {
                hitAreaVelocity += hitAreaPower * Time.fixedDeltaTime;
                //isKeyHit = true;
            }

            // Display UI which Key to click
            //keyToClickTextUI.text = "D";
            this.keyAnimator.SetTrigger("D-Key");
        }

        ////////////////////////////////////
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

        hitAreaPosition = Mathf.Clamp(hitAreaPosition, hitAreaSize / 2, 1 - hitAreaSize / 2);
        hitArea.position = Vector3.Lerp(bottomPivotHitArea.position, topPivotHitArea.position, hitAreaPosition);
        /////////////////////////////////////
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
        CalculateScore();

        // if won last level, Win
        if (currentLevel == lastLevel)
        {
            scoreUI.text = totalScore.ToString();
            Win();
        }

        // else, go to next level
        else
        {

            


            currentLevel++;
            
            StartLevel();
        }
    }

    // 2000, 3500, 4500
    // Calculate Score
    void CalculateScore()
    {
        float timeTaken = failTimerTotal - failTimer;
        if (timeTaken < 10)
        {
            totalScore += currentScore * 1;

        }
        else
        {
            totalScore += (int)(currentScore * (timeTaken/failTimerTotal));
        }
    }

    void Win()
    {
        pause = true;

        finishGamePanelUI.SetActive(true);
        scoreFinishGamePanel.text = "Score: " + totalScore.ToString();
    }

    void LoseLevel()
    {
        // Show Pause Menu 
        pause = true;
        StartLevel();
        Debug.Log("You lose!");
    }
}
