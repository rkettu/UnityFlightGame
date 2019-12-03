using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TcpConnector : MonoBehaviour
{
    public static TcpConnector tcp_instance;

    #region private members 
    private string TCP_host = "192.168.1.1"; // raspi IP
    private int TCP_port = 5000;
    private int buffer_size = 1024;
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private bool initialized = false;
    private string serverMessage = "";
    #endregion

    // raspi sensor data accessor:
    public string RaspiData
    {
        get => serverMessage;
        
    }

    private void Awake()
    {
        // creates a singleton instance of this gameObject
        if (tcp_instance == null)
        {
            //Debug.Log ("TCP connector instance was null.");
            tcp_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (tcp_instance != this)
        {
            //Debug.Log ("TCP connector instance was not this. Disabling and destroying...");
            gameObject.SetActive(false);
            Destroy(gameObject);

        }

        // run once only
        if (!initialized && gameObject.activeSelf)
        {
            ConnectToTcpServer();

            initialized = true; // prevent this block from running again
            Debug.Log("initialized.");
        }
    }

    // close TCP socket & kill its thread when this gameobject is destroyed
    void OnDestroy()
    {
        socketConnection.Close();
        clientReceiveThread.Abort();
    }
    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient(TCP_host, TCP_port);
            Byte[] bytes = new Byte[buffer_size];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 
                        serverMessage = Encoding.UTF8.GetString(incommingData);
                        //Debug.Log("Sensor data received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    
}
