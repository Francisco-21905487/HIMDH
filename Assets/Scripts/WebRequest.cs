using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class TestWebRequest : MonoBehaviour
{
    public string url = "http://down-here.games3.iade.xyz/index.html"; // Endpoint to fetch the updated string
    public float pollingInterval = 5f; // Interval in seconds for polling updates

    public SpawnEnemiesAM spawnManager; // Reference to the SpawnEnemiesAM script

    private int lastDataVersion = -1;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(pollingInterval);

            StartCoroutine(GetRequest(url));
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string[] serverResponse = webRequest.downloadHandler.text.Split(new[] { ' ' }, 2);
                string receivedData = serverResponse[0];
                int dataVersion = int.Parse(serverResponse[1].Split(':')[1]);

                if (dataVersion != lastDataVersion)
                {
                    lastDataVersion = dataVersion;
                    Debug.Log("Received updated data: " + receivedData);

                    // Send the received data to the SpawnEnemiesAM script
                    spawnManager.SpawnEnemiesBasedOnReceivedData(receivedData);
                }
            }
        }

        StartCoroutine(GetRequest(url));
    }
}
