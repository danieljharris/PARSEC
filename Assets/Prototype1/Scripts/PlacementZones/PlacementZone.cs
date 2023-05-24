using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Fusion;

public class PlacementZone : MonoBehaviour
{
    [SerializeField] private string fileName = "PlacementZones.csv";
    [SerializeField] private List<PlacementZonePlaceable> inZone = new List<PlacementZonePlaceable>();
    [SerializeField] private List<GameObject> savingNotifications;
    void Start()
    {
        CreateFileIfNotExist();
    }

    private string GetPath()
    {
        // string path = Application.dataPath + "/" + fileName; // PC
        string path = Application.persistentDataPath + "/" + fileName; // Quest
        return path;
    }

    void CreateFileIfNotExist()
    {
        if(!File.Exists(GetPath()))
        {
            StreamWriter sw = File.CreateText(GetPath());
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
        RPC_Save();
    }

    [Rpc(RpcSources.All, RpcTargets.All, InvokeLocal = true)]

    private void RPC_Save()
    {
        SavingNotification();

        string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string zone = gameObject.name;

        StreamWriter sw = File.AppendText(GetPath());
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

    private void SavingNotification()
    {
        foreach(GameObject notification in savingNotifications)
        {
            notification.SetActive(true);
        }
        Invoke(nameof(DisableSavingNotification), 2f);
    }
    private void DisableSavingNotification()
    {
        foreach(GameObject notification in savingNotifications)
        {
            notification.SetActive(false);
        }
    }
}