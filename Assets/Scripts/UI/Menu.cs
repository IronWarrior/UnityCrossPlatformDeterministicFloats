using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    string inputsFilename = "inputs.txt", resultsFilename = "results.txt";

    [SerializeField]
    long errorLimit = 100;

    [SerializeField]
    Button generateMenuButton, executeMenuButton, inspectorButton, generateButton, executeButton, manualTesterButton;

    [SerializeField]
    InputField seedInput, countInput;

    [SerializeField]
    Toggle treatAllNaNAlikeToggle;

    [SerializeField]
    Text output;

    [SerializeField]
    GameObject menuUI, testUI, generateUI, executeUI;

    [SerializeField]
    Toggle testTogglePrototype;

    private void Awake()
    {
        generateMenuButton.onClick.AddListener(() =>
        {
            menuUI.SetActive(false);
            generateUI.SetActive(true);
        });

        generateButton.onClick.AddListener(() =>
        {
            GenerateInputsFile();
            StartCoroutine(RunTest(true));
        });

        executeMenuButton.onClick.AddListener(() =>
        {
            menuUI.SetActive(false);
            executeUI.SetActive(true);
        });

        executeButton.onClick.AddListener(() =>
        {
            StartCoroutine(RunTest(false));
        });

        inspectorButton.onClick.AddListener(() => SceneManager.LoadScene("Inspector"));
        manualTesterButton.onClick.AddListener(() => SceneManager.LoadScene("ManualTester"));

        generateUI.SetActive(false);
        testUI.SetActive(false);
        executeUI.SetActive(false);
        menuUI.SetActive(true);

        Transform toggleParent = testTogglePrototype.transform.parent;

        foreach (var test in TestRunner.Tests)
        {
            Toggle toggle = Instantiate(testTogglePrototype, toggleParent);
            toggle.GetComponentInChildren<Text>().text = test.Name;
        }

        testTogglePrototype.gameObject.SetActive(false);
    }

    private string GetStreamingAssetsPath(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
                path = path.Insert(0, "file://");
#endif

        return path;
    }

    private void GenerateInputsFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, inputsFilename);

        Generator.GenerateInputsFile(path, int.Parse(seedInput.text), int.Parse(countInput.text));
    }

    private IEnumerator RunTest(bool write)
    {
        testUI.SetActive(true);
        menuUI.SetActive(false);
        generateUI.SetActive(false);
        executeUI.SetActive(false);

        UnityWebRequest inputsReq = UnityWebRequest.Get(GetStreamingAssetsPath(inputsFilename));
        yield return inputsReq.SendWebRequest();

        if (inputsReq.downloadHandler.data == null)
        {
            output.text = $"Unable to retrieve {inputsFilename}.";
            yield break;
        }

        var inputsReader = new StreamReader(new MemoryStream(inputsReq.downloadHandler.data));
        FloatInputs inputs = new FloatInputs(inputsReader);
        inputsReader.Close();

        if (write)
        {
            var writer = new StreamWriter(Path.Combine(Application.streamingAssetsPath, resultsFilename));

            TestRunner runner = new TestRunner(inputs, writer);
            runner.Execute(0);

            output.text = $"Generated ground truth file with {runner.TestCount} test results.";

            writer.Close();
        }
        else
        {
            UnityWebRequest resultsReq = UnityWebRequest.Get(GetStreamingAssetsPath(resultsFilename));
            yield return resultsReq.SendWebRequest();

            if (resultsReq.downloadHandler.data == null)
            {
                output.text = $"Unable to retrieve {resultsFilename}.";
                yield break;
            }

            var resultsReader = new StreamReader(new MemoryStream(resultsReq.downloadHandler.data));

            Toggle[] toggles = testTogglePrototype.transform.parent.GetComponentsInChildren<Toggle>(false);

            var tests = TestRunner.Tests;
            List<string> selectedTests = new List<string>();

            for (int i = 0; i < toggles.Length; i++)
            {
                if (toggles[i].isOn)
                    selectedTests.Add(tests[i].Name);
            }

            TestRunner runner = new TestRunner(inputs, resultsReader, selectedTests.ToArray(), treatAllNaNAlikeToggle.isOn);
            string log = runner.Execute(errorLimit);

            output.text = $"Executed {runner.TestCount} tests with {runner.ErrorCount} errors.\n";

            foreach (var kvp in runner.ErrorCountByTest)
            {
                output.text += $"<b>{kvp.Key.Name}</b>: {kvp.Value}\n";
            }

            output.text += "\n" + log;

            resultsReader.Close();
        }
    }
}
