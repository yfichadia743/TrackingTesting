using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class client : MonoBehaviour
{
    Socket s;
    public float roll;
    public float pitch;
    public float yaw;
    public float x;
    public float y;
    public float z;
    public Transform block;
    // Start is called before the first frame update
    void Start()
    {
        string host = Dns.GetHostName();
        s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect(host, 1234);
        Debug.Log("connected");
    }

    // Update is called once per frame
    void Update()
    {
        byte[] buffer = new byte[255];
        byte[] buffer2 = new byte[255];
        int rec = s.Receive(buffer, buffer.Length, 0);
        string rec2 = Encoding.ASCII.GetString(buffer);
        //print(rec);
        string[] values = rec2.Split(' ');
        float[] values2 = new float[6];
        int j = 0;
        print(values[11]);
        for (int i = 1; i < values.Length; i = i + 2)
        {
            try {
                values[i] = values[i].Substring(0, values[i].Length - 3);
                values2[j] = float.Parse(values[i]);
                j++;
            } catch (System.Exception)
            {
                
            }
        }

        try
        {
            //values2[5] = float.Parse(values[11]);
            roll = values2[0];
            pitch = values2[1];
            yaw = values2[2];
            x = values2[3];
            y = values2[4];
            z = values2[5];
            block.transform.localEulerAngles = new Vector3((roll + 90), pitch, yaw);
            block.transform.localPosition = new Vector3(z, y, x);
        } catch (System.Exception)
        {

        }

        //print(rec2);
    }
}
