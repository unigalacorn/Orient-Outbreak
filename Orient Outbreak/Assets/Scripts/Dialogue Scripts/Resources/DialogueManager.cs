using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Dialogue;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    DialogueGraph selectedGraph;
    //Coroutine _parser;
    int prev;       // used to check if current node is last node
    bool dialogueStarted;
    bool isReadyForNext;
    int dialogueID; // to solve getkey problem

    [Header("Dialogue UI")]
    public GameObject DialogueUI;
    public TextMeshProUGUI charNameUI;
    public TextMeshProUGUI charTitleUI;
    public TextMeshProUGUI dialogueLineUI;
    public Transform dialogueContainer;
    public Image charSpriteUI;

    [Header("Choices UI")]
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private Transform choicesContainer;
    private List<Button> choiceButtonList;

    #region Unity Methods
    void Start()
    {
        dialogueStarted = false;
        isReadyForNext = false;
    }

    private void Update()
    {
        Debug.Log(dialogueID);
        if (Input.GetKeyDown("space") | Input.GetMouseButtonDown(0))
        {
            dialogueID++;
        }

        if(Input.GetKeyDown("space") | Input.GetMouseButtonDown(0) && dialogueStarted && isReadyForNext)
        {
            if(dialogueID != 1)
            {
                isReadyForNext = false;
                NextNode(0);
            }
        }
    }
    #endregion

    #region Public Methods
    // Receives a dialogue graph and starts the dialogue
    public void StartDialogue(DialogueGraph graph)
    {
        //_parser = null;
        selectedGraph = graph;
        selectedGraph.Restart();
        prev = -1;
        dialogueID = 0;

        dialogueStarted = true;
        isReadyForNext = false;

        DialogueUI.SetActive(true);

        //_parser = StartCoroutine(ParseNode());
        ParseNode();
    }
    #endregion

    #region Private Methods
    // Parses the current node in the dialogue graph
    private void ParseNode()
    {
        //Debug.Log(prev);

        // Sets info for dialogue UI elements
        // If null character reference, the current node is skipped
        try
        {
            charNameUI.text = selectedGraph.current.character.charName;
            charTitleUI.text = selectedGraph.current.character.charTitle;
            dialogueLineUI.text = selectedGraph.current.text;
            charSpriteUI.sprite = selectedGraph.current.character.dialogueSprite;
        }
        catch
        {
            isReadyForNext = false;
            NextNode(0);
            return;
        }

        // Changes display if current speaker is Jose or not
        if (charNameUI.text == "Jose")
        {
            charSpriteUI.gameObject.transform.localPosition = new Vector3(-466, -147, 0);
            dialogueContainer.localPosition = new Vector2(200, -95);
        }
        else
        {
            charSpriteUI.gameObject.transform.localPosition = new Vector3(382, -147, 0);
            dialogueContainer.localPosition = new Vector2(-267, -95);
        }

        // If choices exist, instantiate and display buttons
        if(selectedGraph.current.answers.Count > 0)
        {
            isReadyForNext = false;
            choiceButtonList = new List<Button>();
            int i = 0;

            // Create buttons
            foreach (Dialogue.Chat.Answer answer in selectedGraph.current.answers)
            {
                int closureIndex = i;
                Button btn = Instantiate(choiceButtonPrefab,choicesContainer);
                //Debug.Log("current" + i);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = answer.text;
                choiceButtonList.Add(btn);
                choiceButtonList[closureIndex].onClick.AddListener(() => OnChoiceClicked(closureIndex));
                //Debug.Log(choiceButtonList[closureIndex].IsInteractable());
                i++;
            }
            
        }

        // If no choices exist, ready to traverse to next node
        else
        {
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0));
            //yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space) | Input.GetMouseButtonUp(0));
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0));
            //yield return new WaitForSeconds(1f);
            //NextNode(0);
            isReadyForNext = true;
        }
    }

    // Traverse to next node
    private void NextNode(int index)
    {
        //if (_parser != null)
        //{
        //    StopCoroutine(_parser);
        //    _parser = null;
        //}


        prev = selectedGraph.current.GetInstanceID();

        // traverses to next chat node, and triggers any connected activate quest and minigame node
        selectedGraph.AnswerQuestion(index);

        // If reached last consecutive chat node, else parse node
        if (prev == selectedGraph.current.GetInstanceID())
        {
            LastNode();
        }
        else
        {
            ParseNode();
        }
    }

    // Inactivate Dialogue UI objects
    private void LastNode()
    {
        // Reset dialogue flags
        dialogueStarted = false;
        isReadyForNext = false;

        // Inactivate Dialogue UI objects
        charNameUI.text = null;
        charTitleUI.text = null;
        dialogueLineUI.text = null;
        charSpriteUI.sprite = null;
        DialogueUI.SetActive(false);

        // Change game state
        if (GameManager.instance.currentState != GameState.Minigame)
        GameManager.instance.UpdateGameState(GameState.Exploration);
    }

    // Goes to next node in the dialogue graph that is connected to the player's choice
    private void OnChoiceClicked(int index)
    {
        foreach(Button btn in choiceButtonList)
        {
            Destroy(btn.gameObject);
        }
        NextNode(index);
    }
    #endregion 
}
