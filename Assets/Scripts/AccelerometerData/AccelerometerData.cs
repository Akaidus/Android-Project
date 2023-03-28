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
    List<float> timeList = new ();
    List<Vector3> accelList = new ();

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        DataIsRecording();
    }

    public void BeginRecording()
    {
        time = 0f;
        isRecording = true;
        accelList.Clear();
    }

    void DataIsRecording()
    {
        if (!isRecording) return;
        time += Time.deltaTime;
        rawAccelReading = Input.acceleration;
        timeList.Add(time);
        accelList.Add(rawAccelReading);
        if (time <= 0.7f) return;
        StopRecording();
    }

    void StopRecording()
    {
        attempt += 1;
        string dataFile = Path.Combine(Application.persistentDataPath, $"Accelerometer_Data{attempt}.csv");
        StreamWriter streamWriter = new StreamWriter(dataFile, true);
        streamWriter.WriteLine("Time ; x ; y ; z");
        
        for (int i = 0; i < accelList.Count; i++)
        {
            streamWriter.WriteLine($"{timeList[i]:0.0000};{accelList[i].x:0.0000};{accelList[i].y:0.0000};{accelList[i].z:0.0000}");
         
            GameObject newData = new();
            newData.transform.SetParent(content.transform);
            TextMeshProUGUI dataText = newData.AddComponent<TextMeshProUGUI>();
            dataText.text += $"{timeList[i]:0.0000}";
            dataText.text += $"\n x:{accelList[i].x:0.0000}\n y:{accelList[i].y:0.0000}\n z:{accelList[i].z:0.0000}";
        }
        streamWriter.Close();
        isRecording = false;
        sound.Play();
    }
}
