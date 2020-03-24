using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class CreateProjectFolders : ScriptableWizard
{
    //public bool useNamespace = false;
    public bool createScriptsSubFolders = true;
    string assetsPath = "Assets";
    string resourcesPath = "Assets/Resources";

    private string SFGUID;

    public List<Dir> dirs = new List<Dir>()
    {
        new Dir("Scenes"), new Dir("Scripts"),new Dir("Prefabs"),
        new Dir("Materials"), new Dir("Animations"),new Dir("Sounds"),
        new Dir("Textures")
    };

    List<Dir> scriptsSub = new List<Dir>()
    {
        new Dir("Entities"), new Dir("Managers"),new Dir("Factories"),
        new Dir("Pool"), new Dir("GameFlow"), new Dir("GameSetup"),
        new Dir("Tools"), new Dir("Delegates"), new Dir("StateMachines"),
        new Dir("AI"), new Dir("Helpers")
    };

    //public List<string> nsFolders = new List<string>();


    [MenuItem("Edit/Create Project Folders...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Create Project Folders", typeof(CreateProjectFolders), "Create");
    }

    //Called when the window first appears
    void OnEnable()
    {

    }
    //Create button click
    void OnWizardCreate()
    {
        if (!AssetDatabase.IsValidFolder(resourcesPath))
            AssetDatabase.CreateFolder(assetsPath, "Resources");

        //create all the folders required in a project
        //primary and sub folders
        CreateFolders(dirs, resourcesPath);

        AssetDatabase.Refresh();

        /*if (useNamespace)
        {
            foreach (string nsf in nsFolders)
            {
                //AssetDatabase.Contain
                string guid = AssetDatabase.CreateFolder("Assets/Scripts", nsf);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

            }
        }*/
    }
    //Runs whenever something changes in the editor window...
    void OnWizardUpdate()
    {
        /*if (useNamespace)
            AddNamespaceFolders();
        else
            RemoveNamespceFolders();*/


        if (createScriptsSubFolders)
            AddScriptsSubFolder();
        else
            RemoveScriptsSubFolder();

    }
    /*void AddNamespaceFolders()
    {
        if (!nsFolders.Contains("Interfaces"))
            nsFolders.Add("Interfaces");

        if (!nsFolders.Contains("Classes"))
            nsFolders.Add("Classes");

        if (!nsFolders.Contains("States"))
            nsFolders.Add("States");
    }

    void RemoveNamespceFolders()
    {
        if (nsFolders.Count > 0)
            nsFolders.Clear();
    }*/

    void AddScriptsSubFolder()
    {
        dirs.Find(d => d.name == "Scripts").subDirs = scriptsSub;
    }

    void RemoveScriptsSubFolder()
    {
        dirs.Find(d => d.name == "Scripts").subDirs = null;
    }

    void CreateFolders(List<Dir> dirList, string currentPath)
    {
        foreach (Dir folder in dirList)
        {
            if (folder.name == "Scenes")
            {
                if (!AssetDatabase.IsValidFolder(assetsPath + "/" + folder.name))
                    AssetDatabase.CreateFolder(assetsPath, folder.name);
            }
            else
            {
                string guid;
                if (AssetDatabase.IsValidFolder(currentPath + "/" + folder.name))
                    guid = AssetDatabase.AssetPathToGUID(currentPath + "/" + folder.name);
                else
                    guid = AssetDatabase.CreateFolder(currentPath, folder.name);

                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                if (folder.name == "Scripts")
                    SFGUID = newFolderPath;
            }

            if (folder.subDirs != null && folder.subDirs.Count > 0)
                CreateFolders(folder.subDirs, currentPath + "/" + folder.name);
        }
    }
}

[System.Serializable]
public class Dir
{
    public string name;
    public List<Dir> subDirs;

    public Dir(string name)
    {
        this.name = name;
    }
}