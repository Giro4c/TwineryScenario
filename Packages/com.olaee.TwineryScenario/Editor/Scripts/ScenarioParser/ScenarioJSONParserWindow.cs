using UnityEditor;
using UnityEngine;

namespace TwineryScenario.Editor.Scripts.ScenarioParser
{
    public class ScenarioJSONParserWindow : EditorWindow
    {
        public string scenarioFile;

        private bool showFolders = true;
        private bool showPersons = true;
        private bool showEmotions = true;
        public string directoryScenario;
        public string directoryNodes;
        public string directoryProps;
        public string directoryLinks;
        public string directoryPerson;
        public string directoryPersons;
        public string directoryEmotion;
        public string directoryEmotions;
        
        [MenuItem("Scenario/JSON Parser")]
        public static void ShowWindow()
        {
            ScenarioJSONParserWindow wnd = GetWindow<ScenarioJSONParserWindow>();
            wnd.titleContent = new GUIContent("Scenario JSON Parser");
        }

        public void OnGUI()
        {

            showFolders = EditorGUILayout.Foldout(showFolders, "Folder Directories");
            if (showFolders)
            {
                // GUILayout.Label("Folder Directories", EditorStyles.boldLabel);
                directoryScenario = EditorGUILayout.TextField("Scenario", directoryScenario);
                directoryNodes = EditorGUILayout.TextField("Nodes", directoryNodes);
                directoryProps = EditorGUILayout.TextField("Props", directoryProps);
                directoryLinks = EditorGUILayout.TextField("Links", directoryLinks);
                showPersons = EditorGUILayout.Foldout(showPersons, "Persons");
                if (showPersons)
                {
                    // GUILayout.Label("Persons");
                    directoryPerson = EditorGUILayout.TextField("Person", directoryPerson);
                    directoryPersons = EditorGUILayout.TextField("PersonsList", directoryPersons);
                }
                showEmotions = EditorGUILayout.Foldout(showEmotions, "Emotions");
                if (showEmotions)
                {
                    // GUILayout.Label("Emotions");
                    directoryEmotion = EditorGUILayout.TextField("Emotion", directoryEmotion);
                    directoryEmotions = EditorGUILayout.TextField("EmotionsList", directoryEmotions);
                }
            }
            
            GUILayout.Label("Source file", EditorStyles.boldLabel);
            scenarioFile = EditorGUILayout.TextField("Path", scenarioFile);

            if (GUILayout.Button("Parse JSON"))
            {
                Debug.Log("Try get text asset");
                TextAsset scenarioTextAsset = Resources.Load<TextAsset>(scenarioFile);
                if (scenarioTextAsset != null)
                {
                    Debug.Log("Parse JSON");
                    ScenarioJSONParser.ParseScenario(scenarioTextAsset, directoryScenario, directoryNodes, directoryProps,
                        directoryLinks, directoryPerson, directoryPersons, directoryEmotion, directoryEmotions);
                }
            }
            
        }

        private void DebugAll()
        {
            TextAsset scenarioTextAsset = Resources.Load<TextAsset>(scenarioFile);
            Debug.Log(scenarioTextAsset);
            
            Debug.Log(directoryScenario);
            Debug.Log(directoryNodes);
            Debug.Log(directoryProps);
            Debug.Log(directoryLinks);
            Debug.Log(directoryPerson);
            Debug.Log(directoryPersons);
            Debug.Log(directoryEmotion);
            Debug.Log(directoryEmotions);
            
        }

        // public void CreateGUI()
        // {
        //     // Each editor window contains a root VisualElement object
        //     VisualElement root = rootVisualElement;
        //
        //     // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //     VisualElement btn = new Button();
        //     btn.name = "Parse JSON";
        //     root.Add(btn);
        //     
        // }
    }
   
}