using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AccelerometerData : MonoBehaviour
{
    [SerializeField] RectTransform content;

    AudioSource sound;
    int attempt;
    Vector3 rawAccelReading;
    float time;
    bool isRecording;
    List<Vector3> accelData = new ();
    List<Vector3> fullaccelData = new();

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        DataIsRecording();
    }

    public void BeginRecording()
    {
        time = .7f;
        isRecording = true;
        accelData.Clear();
    }

    void DataIsRecording()
    {
        if (!isRecording) return;
        rawAccelReading = Input.acceleration;
        accelData.Add(rawAccelReading);
        time -= Time.deltaTime;
        if (time >= 0) return;
        isRecording = false;
        StopRecording(); 
        sound.Play();        
    }

    void StopRecording()
    {
        attempt += 1;
        string dataFile = Path.Combine(Application.persistentDataPath, $"Accelerometer_Data{attempt}.cvs");
        StreamWriter streamWriter = new StreamWriter(dataFile, true);
        foreach (var n in accelData)
        {
            streamWriter.WriteLine(n);
            
            GameObject newData = new GameObject();
            newData.transform.SetParent(content.transform);
            TextMeshProUGUI dataText = newData.AddComponent<TextMeshProUGUI>();
            dataText.text = n.ToString("0.00000000");
            fullaccelData.Add(n);
        }
        streamWriter.Close();
        isRecording = false;
    }
}
