using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

/// <summary>
/// Forefront class for the server communication.
/// </summary>
public class ServerCommunication : MonoBehaviour
{

    // Server IP address
    [SerializeField]
    private string hostIP;
    // Server port
    [SerializeField]
    private int port = 3000;
    // Flag to use localhost
    [SerializeField]
    private bool useLocalhost = true;
    // Address used in code
    WebSocket websocket;
    private string host => useLocalhost ? "localhost" : hostIP;
    // Final server address
    private string server;
    // WebSocket Client
    private WsClient client;
    public ActionRouterWebGL handler;
    public static ServerCommunication Instance;
    async void Start()
    {
        Instance = this;
        server = "ws://" + host + ":" + port + "/ws";
        websocket = new WebSocket(server);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection to " + server + " open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("WS ERROR: " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection to " + server + " closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // getting the message as a string
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            HandleMessage(message);

        };
        // waiting for messages
        await websocket.Connect();
    }

    private void Awake()
    {
        server = "ws://" + host + ":" + port + "/ws";
        try
        {
            client = new WsClient(server);
        }
        catch { }
        // ConnectToServer();
    }


    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
        // Check if server send new messages
        // var cqueue = client.receiveQueue;
        // string msg;
        // while (cqueue.TryPeek(out msg))
        // {
        //     // Parse newly received messages
        //     cqueue.TryDequeue(out msg);
        //     HandleMessage(msg);
        // }
    }
    /// <summary>
    /// Method responsible for handling server messages
    /// </summary>
    /// <param name='msg'>Message.</param>
    private void HandleMessage(string msg)
    {
        Debug.Log("Server: " + msg);
        handler.HandleAction(msg);
    }
    /// <summary>
    /// Call this method to connect to the server
    /// </summary>
    public async void ConnectToServer()
    {
        await client.Connect();
    }

    public async void SendRequest(string command, KeyValuePair<string, string>[] data = null)
    {
        string sendData = "{\"msg\":\"" + command + "\"";
        if (data != null)
            foreach (KeyValuePair<string, string> pair in data)
            {
                sendData += ",\"" + pair.Key + "\":\"" + pair.Value + "\"";
            }
        sendData += "}";
        // client.Send(sendData);
        await websocket.SendText(sendData);
        Debug.Log(sendData);
    }
    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

}