<img src="https://www.carterapi.com/carter-full-white-transparent.svg" style="width: 200px"/>

# Carter Unity

Make your game characters talk in minutes with next generation artificial intelligence. This package is still under active development and is not yet ready for production use.

## Installation

This package supports our more robust REST API for lightweight and nimble projects.

To do this, go to: Window -> Package Manager -> Click the (+) add package from git URL -> Paste:
`https://github.com/Carter-Labs-Ltd/carter-unity-api.git`

## Usage

Getting started with Carter is easy. First configure your agent [here](https://studio.carterlabs.ai). Once you've got your agent's API Key come back here and add Carter to your game as below.

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
