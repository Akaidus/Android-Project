using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AccelerometerData : MonoBehaviour
{
    [SerializeField] RectTransform content;
    
    float rawAccelReading;
    float time;
    float stillThreshold = .05f;
    bool isRecording;
    int yCellSpacing = 42;
    List<float> accelData = new ();
    List<float> fullaccelData = new();

    void Update()
    {
        DataIsRecording();
    }

    public void BeginRecording()
    {
        time = .5f;
        isRecording = true;
        accelData.Clear();
    }

    void DataIsRecording()
    {
        if (!isRecording) return;
        rawAccelReading = Input.acceleration.y;
        accelData.Add(rawAccelReading);
        //float testData = Random.Range(0, 100);
        //accelData.Add(testData);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            isRecording = false;
            StopRecording();
            //play sound to indicate finish
        }
    }

    void StopRecording()
    {
        //string dataFile = Path.Combine(Application.persistentDataPath, "Accelerometer_Data.cvs");
        //StreamWriter streamWriter = new StreamWriter(dataFile);
        foreach (var n in accelData)
        {
            //streamWriter.WriteLine(n);

            GameObject newData = new GameObject();
            newData.transform.SetParent(content.transform);
            TextMeshProUGUI dataText = newData.AddComponent<TextMeshProUGUI>();
            dataText.text = n.ToString("0.00000000");
            fullaccelData.Add(n);
            
        }
        //streamWriter.Close();
        isRecording = false;
    }
}
