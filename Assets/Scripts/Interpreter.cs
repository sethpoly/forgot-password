using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Interpreter : MonoBehaviour
{

    public GameObject terminal;

    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        { "black", "a021b21" },
        { "gray",  "a555d71"},
        { "red",   "aff5879"},
        { "yellow","#f2f1b9"},
        { "blue",  "a9ed9d8"},
        { "purple","ad96ff"},
        { "orange", "#ef5847"}
    };

    List<string> response = new List<string>();

    public List<string> Interpret(string userInput)
    {
        response.Clear();

        string[] args = userInput.Split();

        switch(args[0].ToLower())
        {
            case "ascii":
                LoadTitle("ascii.txt", "red", 2);
                break;
            case "help":
                ListEntry("help", "returns a list of commands");
                ListEntry("start", "start OS");
                ListEntry("stop", "pauses the game");
                break;
            case "start":
                response.Add("Booting bonkOS...");
                StartCoroutine(StartDesktop());
                break;
            default:
                response.Add("Command not recognized. Type \"help\" for a list of commands.");
                break;
        }

        return response;
    }

    IEnumerator StartDesktop()
    {
        yield return new WaitForSeconds(2f);
        terminal.SetActive(false);
    }
    
    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ": " + ColorString(b, colors["yellow"]));
    }

    void LoadTitle(string path, string color, int spacing)
    {
        StreamReader file = new StreamReader(Path.Combine(Application.streamingAssetsPath, path));

        for(int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        while (!file.EndOfStream)
        {
            response.Add(ColorString(file.ReadLine(), colors[color]));
        }

        for (int i = 0; i < spacing; i++)
        {
            response.Add("");
        }

        file.Close();
    }

}
