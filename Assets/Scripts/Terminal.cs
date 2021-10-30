using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputField;
    public ScrollRect sr;
    public GameObject msgList;

    Interpreter interpreter;

    private void Start()
    {
        interpreter = GetComponent<Interpreter>();
    }

    private void OnGUI()
    {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            // Store user input
            string userInput = terminalInput.text;

            // Clear input field
            ClearInputField();

            // Init a gameobject with directoryLine prefix
            AddDirectoryLine(userInput);

            // Add interpretation lines
            int lines = AddInterpreterLines(interpreter.Interpret(userInput));

            // Scroll to the botth of scrollrect
            ScrollToBottom(lines);

            // Move user input line to end
            userInputField.transform.SetAsLastSibling();

            // Refocus input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    void ClearInputField()
    {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput)
    {
        // Resizing command line container
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        // Init dir line
        GameObject msg = Instantiate(directoryLine, msgList.transform);

        // Set its child index
        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        // Set the text of this new gameobect
        msg.GetComponentsInChildren<Text>()[1].text = userInput;
    }

    int AddInterpreterLines(List<string> interpretation)
    {
        for(int i = 0; i < interpretation.Count; i++)
        {
            // Init response line
            GameObject res = Instantiate(responseLine, msgList.transform);

            // Set it to end
            res.transform.SetAsLastSibling();

            //Get size of message list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35.0f);

            // Set the text of this response line to whatever interpreter returns
            res.GetComponentInChildren<Text>().text = interpretation[i];
        }

        return interpretation.Count;
    }

    void ScrollToBottom(int lines)
    {
        if (lines > 4)
            sr.velocity = new Vector2(0, 450);
        else
            sr.verticalNormalizedPosition = 0;
    }
}
