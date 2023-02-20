using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;

namespace Carter {

    public class Agent
    {
        public SocketIOUnity socket;

            public string url { get; set; }
            public string apiKey { get; set; }
            public string playerId { get; set; }
            public string agentId { get; set; }

            public delegate void OnMessage(string message);

            public Agent(string apiKey, string playerId, string url, Action onConnect, Action onDisconnect, OnMessage onMessage )
            {
                this.apiKey = apiKey;
                this.agentId = agentId;
                this.url = url;
                this.playerId = playerId;

                Connect(onConnect, onDisconnect, onMessage);
            }

            void Connect(Action onConnect, Action onDisconnect, OnMessage onMessage)
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

                    onConnect();
                };

                socket.OnDisconnected += (sender, e) =>
                {
                    Debug.Log("socket.OnDisconnected");
                    onDisconnect();
                };


                socket.Connect();

                socket.OnUnityThread("message", (data) =>
                {
                    // data is { output : {text : string}}}
                    var text = data.GetValue("output").GetValue("text").ToString();
                    onMessage(message);
                });

            }

            public void disconnect() {
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
