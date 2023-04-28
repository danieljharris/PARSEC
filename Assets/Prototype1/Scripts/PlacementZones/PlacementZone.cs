using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlacementZone : MonoBehaviour
{
    [SerializeField] private string fileName = "PlacementZones.csv";
    [SerializeField] private List<PlacementZonePlaceable> inZone = new List<PlacementZonePlaceable>();
    void Start()
    {
        CreateFileIfNotExist();
    }

    void CreateFileIfNotExist()
    {
        string path = Application.dataPath + "/" + fileName;
        if(!File.Exists(path))
        {
            StreamWriter sw = File.CreateText(path);
            string header = "Time, Scene, Zone, Object, Validity";
            sw.WriteLine(header);   
            sw.Close();         
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlacementZonePlaceable placeable = other?.gameObject?.GetComponent<PlacementZonePlaceable>();
        if(placeable == null) return;

        inZone.Add(placeable);
    }
    void OnTriggerExit(Collider other)
    {
        PlacementZonePlaceable placeable = other?.gameObject?.GetComponent<PlacementZonePlaceable>();
        if(placeable == null) return;

        inZone.Remove(placeable);
    }

    public void SaveToFile()
    {
        string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string zone = gameObject.name;

        string path = Application.dataPath + "/" + fileName;
        StreamWriter sw = File.AppendText(path);
        for(int i = 0; i < inZone.Count; i++)
        {
            string objectName = inZone[i].gameObject.name;

            string validity;
            if (inZone[i].correctZone == null) validity = "Unknown";
            else if (inZone[i].correctZone == this) validity = "Correct";
            else validity = "Incorrect";

            sw.WriteLine($"{time}, {scene}, {zone}, {objectName}, {validity}");
        }
        sw.Close();
    }
}