using System;
using UnityEditor;
using UnityEngine;

public class BuildConfigurator : EditorWindow
{
    private string buildDate = "";
    private string devLogVersion = "";
    private string platformCode = "";
    private string buildTypeCode = "";

    [MenuItem("Tools/Build Configurator")]
    public static void ShowWindow()
    {
        GetWindow<BuildConfigurator>("Build Configurator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Build Information", EditorStyles.boldLabel);
        
        devLogVersion = EditorGUILayout.TextField("Dev Log Version: ", devLogVersion);

        GUILayout.Label("Platform:");
        platformCode = EditorGUILayout.TextField("Platform Code (win/mac/lin/null)", platformCode);

        GUILayout.Label("Build Type:");
        buildTypeCode = EditorGUILayout.TextField("Build Type Code (D/F/U)", buildTypeCode); //D (Dev mode), F (Normal export), U (Everything else (Unity))

        if (GUILayout.Button("Print build version in inspector"))
        {
            //Set automatic information
            buildDate = DateTime.Now.ToString("ddMMyy"); //Set the build date
            
            if (string.IsNullOrEmpty(buildDate) || string.IsNullOrEmpty(devLogVersion) ||
                string.IsNullOrEmpty(platformCode) || string.IsNullOrEmpty(buildTypeCode))
            {
                Debug.LogError("Fill out all the fields.");
                return;
            }

            string buildName = $"{buildDate}{devLogVersion}{platformCode}{buildTypeCode}";

            Debug.Log("Version: \n" + buildName);
        }
    }
}