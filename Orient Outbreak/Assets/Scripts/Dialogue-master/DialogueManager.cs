using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using Dialogue;
using TMPro;

public class Choice
{
    public Button choice;
    public int index;
}

public class DialogueManager : MonoBehaviour
{
    DialogueGraph selectedGraph;
    Coroutine _parser;
    int prev;

    [Header("Dialogue UI")]
    public GameObject DialogueUI;
    public TextMeshProUGUI charNameUI;
    public TextMeshProUGUI charTitleUI;
    public TextMeshProUGUI dialogueLineUI;
    public Image charSpriteUI;

    [Header("Choices UI")]
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private Transform choicesContainer;
    private List<Button> choiceButtonList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartDialogue(DialogueGraph graph)
    {
        selectedGraph = graph;
        selectedGraph.Restart();
        
        DialogueUI.SetActive(true);

        prev = -1;
        
        _parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        charNameUI.text = selectedGraph.current.character.charName;
        charTitleUI.text = selectedGraph.current.character.charTitle;
        dialogueLineUI.text = selectedGraph.current.text;
        charSpriteUI.sprite = selectedGraph.current.character.dialogueSprite;

        // if choices exist
        if(selectedGraph.current.answers.Count > 0)
        {
            choiceButtonList = new List<Button>();
            int i = 0;
            foreach (Dialogue.Chat.Answer answer in selectedGraph.current.answers)
            {
                int closureIndex = i;
                Button btn = Instantiate(choiceButtonPrefab,choicesContainer);
                Debug.Log("current" + i);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = answer.text;
                choiceButtonList.Add(btn);
                choiceButtonList[closureIndex].onClick.AddListener(() => OnChoiceClicked(closureIndex));
                Debug.Log("DONE");
                
                i++;
            }
            yield return null;
        }

        else
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space) | Input.GetMouseButtonUp(0));

            NextNode(0);
        }
    }

    public void NextNode(int index)
    {
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }

        // traverses to next chat node
        selectedGraph.AnswerQuestion(index);

        // If reached last consecutive chat node, 
        if (prev == selectedGraph.current.GetInstanceID())
        {
            LastNode();
            // If chat node is followed by a action node
            //Debug.Log(selectedGraph.current.GetPort());
        }
        // if actual last node, set Dialogue UI to inactive
        else
        {
            //LastNode();

        }
        

        prev = selectedGraph.current.GetInstanceID();

        _parser = StartCoroutine(ParseNode());
    }

    // Inactivate Dialogue UI objects
    public void LastNode()
    {
        charNameUI.text = null;
        charTitleUI.text = null;
        dialogueLineUI.text = null;
        charSpriteUI.sprite = null;

        DialogueUI.SetActive(false);
    }

    public void OnChoiceClicked(int index)
    {
        foreach(Button btn in choiceButtonList)
        {
            Destroy(btn.gameObject);
        }
        NextNode(index);
    }
}
