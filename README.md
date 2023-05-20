![GitHub Banners](https://github.com/Carter-Labs-Ltd/carter-unity-api/assets/16668357/52a512fa-b752-4245-99ac-40c9f379b601)

# Bring your sidekick into digital space.
Bring a Carter sidekick into your game in minutes.

## Installation

This package supports our more robust REST API for lightweight and nimble projects.

To do this, go to: Window -> Package Manager -> Click the (+) add package from git URL -> Paste:
`https://github.com/Carter-Labs-Ltd/carter-unity-api.git`

## Usage

Getting started with Carter is easy. First configure your agent [here](https://controller.carterlabs.ai). Once you've got your agent's API Key come back here and add Carter to your game as below.

### Text Based Chat

```...
using Carter;

public class MyExampleScript : MonoBehaviour
{
    Agent myAgent;

    void Start()
    {
        myAgent = gameObject.AddComponent<Agent>();
        myAgent.url = "https://api.carterlabs.ai/chat";
        myAgent.key = "YOUR API KEY";
        myAgent.playerId = "UNIQUE PLAYER ID (can be anything you want!)";
        myAgent.onMessage += (ApiResponse response) => {
            Debug.Log("Input: " + response.input);
            Debug.Log("Output Text: " + response.output.text);

            foreach (ForcedBehaviour fb in response.forced_behaviours) {
                Debug.Log("Forced Behaviour: " + fb.name);
            }

        };
        myAgent.Interact("Wake up!");
    }
}
```

### Voice Based Chat

````using Carter;

public class MyExampleScript : MonoBehaviour
{
    Agent myAgent;

    void Start()
    {
        myAgent = gameObject.AddComponent<Agent>();
        myAgent.url = "https://api.carterlabs.ai/chat";
        myAgent.key = "YOUR API KEY";
        myAgent.playerId = "UNIQUE PLAYER ID (can be anything you want!)";
        myAgent.onMessage += (ApiResponse response) => {
            Debug.Log("Input: " + response.input);
            Debug.Log("Output Text: " + response.output.text);

            myAgent.say(response.output.audio, "male");
            foreach (ForcedBehaviour fb in response.forced_behaviours) {
                Debug.Log("Forced Behaviour: " + fb.name);
            }
        };
        myAgent.StartListening();
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            myAgent.listen();
        }

        if (!Input.GetKey("space"))
        {
            myAgent.sendAudio();
        }
    }
}```

[Further Documentation](https://carterapi.gitbook.io/carter-docs/)

## Join The Community

Join our awesome [Discord community](https://discord.gg/YqWwCVU8UH).

Follow us on [TikTok](https://www.tiktok.com/@carterlabs)

Binge & Learn on [YouTube](https://www.youtube.com/@Carter_Labs)
````
