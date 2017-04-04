using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Innoactive
{
    public class EditorHelper : Editor
    {
        [MenuItem("Innoactive/Create Folder Structure")]
        static void InitProjectStructure()
        {
            CreateFolders();
            CreateTestMaterials();
            MoveThisScriptToEditor();
        }

        #region Folders

        /// <summary>
        /// Creates the project folder structure
        /// </summary>
        static void CreateFolders()
        {
            CreateFolder("Editor");
            CreateFolder("Extensions");
            CreateFolder("Plugins");
            CreateFolder("Scripts");
            CreateFolder("Scenes");
            CreateFolder("StandardAssets");

            CreateFolder("StaticAssets");
            CreateFolder("StaticAssets/Animations");
            CreateFolder("StaticAssets/Animators");
            CreateFolder("StaticAssets/Effects");
            CreateFolder("StaticAssets/Fonts");
            CreateFolder("StaticAssets/Characters");
            CreateFolder("StaticAssets/Environment");
            CreateFolder("StaticAssets/Props");
            CreateFolder("StaticAssets/Prefabs");
            CreateFolder("StaticAssets/Sounds");
            CreateFolder("StaticAssets/Shaders");
            CreateFolder("StaticAssets/Textures");

            CreateFolder("StreamingAssets");

            Debug.Log("Created Project Folders");
        }

        /// <summary>
        /// Creates a folder with a .gitkeep file
        /// </summary>
        /// <param name="path"></param>
        static void CreateFolder(string path)
        {
            path = "Assets/" + path;

            Directory.CreateDirectory(path);
            try
            {
                File.CreateText(path + "/.gitkeep");
            } catch(System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        #endregion

        #region Test Materials

        static void CreateTestMaterials()
        {
            string folderPath = "Assets/StaticAssets/Materials/TestMaterials/";
            CreateMaterial(Color.white, "White", folderPath);
            CreateMaterial(Color.black, "Black", folderPath);
            CreateMaterial(Color.red, "Red", folderPath);
            CreateMaterial(Color.green, "Green", folderPath);
            CreateMaterial(new Color(0, 0.75f, 1, 1), "Blue1", folderPath);
            CreateMaterial(Color.blue * 0.75f, "Blue2", folderPath);
            CreateMaterial(Color.yellow, "Yellow", folderPath);
            CreateMaterial(Color.white * 0.75f, "Grey0", folderPath);
            CreateMaterial(Color.grey, "Grey1", folderPath);
            CreateMaterial(Color.white * 0.25f, "Grey2", folderPath);
            CreateMaterial(Color.white * 0.125f, "Grey3", folderPath);
            CreateMaterial(new Color(1, 0.6f, 0, 1), "Orange", folderPath);

            Debug.Log("Created Test Materials");
        }

        /// <summary>
        /// Creates a Material with given color and name at the given folder path 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="name"></param>
        static void CreateMaterial(Color color, string name, string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            Material material = new Material(Shader.Find("Standard"));
            material.color = color;
            AssetDatabase.CreateAsset(material, folderPath + name + ".mat");
        }

        #endregion

        /// <summary>
        /// Moves the script into the Editor folder
        /// </summary>
        static void MoveThisScriptToEditor()
        {
            // Search script file
            string fileName = typeof(EditorHelper).Name + ".cs";
            DirectoryInfo directoryRoot = new DirectoryInfo("Assets/");
            FileInfo[] filesInDir = directoryRoot.GetFiles(fileName);

            foreach(FileInfo foundFile in filesInDir)
            {
                string fullName = foundFile.FullName;
                File.Move(fullName, "Assets/Editor/" + fileName);
            }
        }
    }
}
