using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CloudSaver
{
    public IEnumerator PostSaveData(string url, PixelArtState data)
    {
        string json = JsonUtility.ToJson(data);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(bodyRaw),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Post successful.");
        }
        else
        {
            Debug.LogError("Post error: " + request.error);
        }
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://www.my-server.com/image.png");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = DownloadHandlerTexture.GetContent(www);
        }
    }
}
