using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] private Animator wipeTransition;
    [SerializeField] private Animator audioTransition;
    [SerializeField] private float waitTime = 1f;

    #region Unity Methods
    private void Start()
    {
        StartCoroutine(StartTheme());
    }

    private void Awake()
    {
        //Initialize Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ActivateCanvas();
    }
    #endregion

    #region Public Methods
    public void ChangeScene(string nextScene)
    {
        // Visual Transition when changing scenes
        StartCoroutine(LoadLevel(nextScene)); // Wipe Transition

        // Audio Transition when changing scenes
        audioTransition.SetTrigger("master_fadeOut");

        string thisScene = SceneManager.GetActiveScene().name;
        if (thisScene == "Immunity Booster Scene" || thisScene == "WerkIt Scene" || thisScene == "ShieldsUp Scene")
        {
            StartCoroutine(StartTheme());
        }

        // if nextscene is Minigame, stop all audio playing and play new song
        if (nextScene == "Immunity Booster Scene" || nextScene == "WerkIt Scene" || nextScene == "ShieldsUp Scene")
        {
            StartCoroutine(StopMusicPlaying(nextScene));
        }
    }

    public void ActivateCanvas()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeactivateCanvas()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion

    #region Coroutines
    IEnumerator StartTheme()
    {
        yield return new WaitForSeconds(waitTime);
        AudioManager.instance.StopAudio();
        AudioManager.instance.Play("Theme");
    }

    IEnumerator LoadLevel(string nextScene)
    {
        ActivateCanvas();
        wipeTransition.SetTrigger("Wipe_In");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(nextScene);
    }

    IEnumerator StopMusicPlaying(string nextScene)
    {
        yield return new WaitForSeconds(waitTime);

        AudioManager.instance.StopAudio();

        if(nextScene != "Immunity Booster Scene")
        {
            AudioManager.instance.Play("Other Minigame");
        }


    }
    #endregion
}
