using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class trytoconnect : MonoBehaviour
{
    SerialPort data_stream;
    public string recievedString;
    public GameObject test_data;
    public float recievedFloat1;
    public float recievedFloat2;
    public float recievedFloat3;
    public string recievedFloat4;
    public float recievedFloat5;

    // Start is called before the first frame update
    void Start()
    {
        data_stream = new SerialPort("\\\\.\\COM3", 115200);
        data_stream.Open();
        print("serial port open");
        data_stream.ReadTimeout = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (!data_stream.IsOpen)
        {
            data_stream.Open();
            print("opened sp");
        }
        try
        {
            recievedString = data_stream.ReadExisting();
            print(recievedString);
            string[] recievedStrings = recievedString.Split(',');
            recievedFloat1 = float.Parse(recievedStrings[3]);
            recievedFloat2 = float.Parse(recievedStrings[4]);
            recievedFloat3 = float.Parse(recievedStrings[6]);
            test_data.transform.position = new Vector3(recievedFloat1 * 5f, (recievedFloat3 * 3f) + 0.5f, recievedFloat2 * -6f);
            data_stream.BaseStream.Flush();
        }
        catch (System.Exception)
        {

        }
        data_stream.ReadTimeout = 1000;
    }
}
