using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Dialogue;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Variables
    DialogueGraph selectedGraph;

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

    // Stores original x and y values of certain dialogue UI elements
    private float charNameUI_xScale;
    private float charTitleUI_xScale;
    private float dialogueLineUI_xScale;

    private float charNameUI_yScale;
    private float charTitleUI_yScale;
    private float dialogueLineUI_yScale;

    private float dialogueContainer_xScale;
    private float dialogueContainer_yScale;
    private float dialogueContainer_yPos;

    private float choicesContainer_xScale;
    private float choicesContainer_yScale;
    #endregion

    #region Unity Methods
    void Start()
    {
        dialogueStarted = false;
        isReadyForNext = false;

        // Get original x-scale transform values of Dialogue UI elements
        charNameUI_xScale = charNameUI.GetComponent<Transform>().localScale.x;
        charTitleUI_xScale = charTitleUI.GetComponent<Transform>().localScale.x;
        dialogueLineUI_xScale = dialogueLineUI.GetComponent<Transform>().localScale.x;
        choicesContainer_xScale = choicesContainer.localScale.x;

        // Get original y-scale values of UI elements
        charNameUI_yScale = charNameUI.GetComponent<Transform>().localScale.y;
        charTitleUI_yScale = charTitleUI.GetComponent<Transform>().localScale.y;
        dialogueLineUI_yScale = dialogueLineUI.GetComponent<Transform>().localScale.y;
        choicesContainer_yScale = choicesContainer.localScale.y;

        // Get original values for the dialogue container
        dialogueContainer_xScale = dialogueContainer.localScale.x;
        dialogueContainer_yScale = dialogueContainer.localScale.y;
        dialogueContainer_yPos = dialogueContainer.localPosition.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") | Input.GetMouseButtonDown(0))
        {
            dialogueID++;

            if(dialogueStarted && isReadyForNext && dialogueID != 1){
                isReadyForNext = false;
                NextNode(0);
            }
        }

        //if(Input.GetKeyDown("space") | Input.GetMouseButtonDown(0) && dialogueStarted && isReadyForNext)
        //{
        //    if(dialogueID != 1)
        //    {
        //        isReadyForNext = false;
        //        NextNode(0);
        //    }
        //}
    }
    #endregion

    #region Public Methods
    // Receives a dialogue graph and starts the dialogue
    public void StartDialogue(DialogueGraph graph)
    {
        selectedGraph = graph;
        selectedGraph.Restart();
        prev = -1;
        dialogueID = 1;

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

        // Modify transform values of Dialogue UI elements according to if speaker is Jose or not
        if (charNameUI.text == "Jose")
        {
            dialogueContainer.localPosition = new Vector2(200, dialogueContainer_yPos);
            dialogueContainer.localScale = new Vector2(dialogueContainer_xScale, dialogueContainer_yScale);

            charSpriteUI.gameObject.transform.localPosition = new Vector2(-466, -147);

            charNameUI.GetComponent<Transform>().localScale = new Vector2(charNameUI_xScale, charNameUI_yScale);
            charTitleUI.GetComponent<Transform>().localScale = new Vector2(charTitleUI_xScale, charTitleUI_yScale);
            dialogueLineUI.GetComponent<Transform>().localScale = new Vector2(dialogueLineUI_xScale, dialogueLineUI_yScale);

            choicesContainer.localScale = new Vector2(choicesContainer_xScale, choicesContainer_yScale);

            //charSpriteUI.gameObject.transform.localPosition = new Vector3(-466, -147, 0);
            //dialogueContainer.localPosition = new Vector2(200, -95);
        }
        else
        {
            dialogueContainer.localPosition = new Vector2(-267, dialogueContainer_yPos);
            dialogueContainer.localScale = new Vector2(dialogueContainer_xScale * -1f, dialogueContainer_yScale);

            charSpriteUI.gameObject.transform.localPosition = new Vector2(382, -147);

            charNameUI.GetComponent<Transform>().localScale = new Vector2(charNameUI_xScale * -1f, charNameUI_yScale);
            charTitleUI.GetComponent<Transform>().localScale = new Vector2(charTitleUI_xScale * -1f, charTitleUI_yScale);
            dialogueLineUI.GetComponent<Transform>().localScale = new Vector2(dialogueLineUI_xScale * -1f, dialogueLineUI_yScale);

            choicesContainer.localScale = new Vector2(choicesContainer_xScale * -1f, choicesContainer_yScale);
            //charSpriteUI.gameObject.transform.localPosition = new Vector3(382, -147, 0);
            //dialogueContainer.localPosition = new Vector2(-267, -95);
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

                btn.GetComponentInChildren<TextMeshProUGUI>().text = answer.text;
                choiceButtonList.Add(btn);
                choiceButtonList[closureIndex].onClick.AddListener(() => OnChoiceClicked(closureIndex));

                i++;
            }
            
        }

        // If no choices exist, ready to traverse to next node
        else
        {
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

        StartCoroutine(ChangeState());
        // Change game state
        //if (GameManager.instance.currentState != GameState.Minigame)
        //GameManager.instance.UpdateGameState(GameState.Exploration);
        //Debug.Log("yeet");
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

    IEnumerator ChangeState()
    {
        yield return new WaitForEndOfFrame();
        if (GameManager.instance.currentState != GameState.Minigame)
            GameManager.instance.UpdateGameState(GameState.Exploration);
        Debug.Log("yeet");
    }
}
