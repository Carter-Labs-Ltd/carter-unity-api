using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Carter {
    [System.Serializable]
    public class ApiResponse {
        public ForcedBehaviour[] forced_behaviours;
        public string input;
        public Output output;
    }

    [System.Serializable]
    public class ForcedBehaviour {
        public string name;
    }

    [System.Serializable]
    public class Output {
        public bool audio;
        public string text;
    }

    [System.Serializable]
    public class Agent : MonoBehaviour
    {
        public string url;
        public string key;
        public string playerId;

        public delegate void MessageEventHandler(ApiResponse response);
        public event MessageEventHandler onMessage;


        [System.Serializable]
        public class Message {
            public string key;
            public string playerId;
            public string text;
        }

        private void HandleApiResponse(ApiResponse apiResponse) {
            onMessage?.Invoke(apiResponse);
        }

        public void Interact(string message) {
            Debug.Log("Sending message: " + message);
            StartCoroutine(SendJsonRequest(message));
        }

        IEnumerator SendJsonRequest(string message) {
            Message msg = new Message {
                key = this.key,
                playerId = this.playerId,
                text = message
            };
            string json = JsonUtility.ToJson(msg);

            using (UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)) {
                www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                } else {
                    ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(www.downloadHandler.text);
                    HandleApiResponse(apiResponse);
                }
            }
        }
    }
}
