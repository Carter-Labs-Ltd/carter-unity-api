using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;

namespace Carter {

    [System.Serializable]
    class AgentOutput {
        public string text;
    }

    [System.Serializable]
    class AgentResponse {
        public AgentOutput output;
        public string input;
    }

    public class Agent
    {
        public SocketIOUnity socket;

            public string url { get; set; }
            public string apiKey { get; set; }
            public string playerId { get; set; }
            public string agentId { get; set; }

            public Boolean connected { get; set; }

            public delegate void OnMessage(string message);

            Action onConnect { get; set;}
            Action onDisconnect { get; set;}
            OnMessage onMessage { get; set;}

            public Agent(string apiKey, string playerId, string url, Action onConnect, Action onDisconnect, OnMessage onMessage )
            {
                this.apiKey = apiKey;
                this.agentId = agentId;
                this.url = url;
                this.playerId = playerId;
                this.onConnect = onConnect;
                this.onDisconnect = onDisconnect;
                this.onMessage = onMessage;


                Connect();
            }

            public void Connect()
            {

                var uri = new Uri(url);
                socket = new SocketIOUnity(uri, new SocketIOOptions
                {
                    Query = new Dictionary<string, string>
                        {
                            {"token", "UNITY" }
                        }                  
                    ,
                    Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
                });

                socket.JsonSerializer = new NewtonsoftJsonSerializer();

                socket.OnConnected += (sender, e) =>
                {
                    Debug.Log("socket.OnConnected"); 

                    // set connected
                    connected = true;

                    onConnect();
                };

                socket.OnDisconnected += (sender, e) =>
                {
                    Debug.Log("socket.OnDisconnected");
                    connected = false;
                    onDisconnect();
                };


                socket.Connect();

                socket.OnUnityThread("message", (data) =>
                {
                    AgentResponse response = data.GetValue<AgentResponse>();
                    onMessage(response.output.text);
                });

            }

            public void Disconnect() {
                socket.Disconnect();
            }

            public void send(string message) {
                if (socket == null) {
                    Debug.Log("Socket is null");
                    return;
                } else {
                    Debug.Log("Socket is not null");

                    socket.EmitStringAsJSON("message", "{\"text\": \"" + message + "\", \"apiKey\": \"" + this.apiKey + "\", \"playerId\": \"" + this.playerId + "\"}");
                }
            }
    }
}
