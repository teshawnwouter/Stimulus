using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public class Registering : MonoBehaviour
{
    public CreateSessionApi api;

    [Serializable]
    public class LoginRequest
    {
        public string username;
    }

    [Serializable]
    private class LoginResponse
    {
        public bool ok;
        public int user_Id;
        public string error;
    }

    private const string CreateUserUrl = "http://localhost:5173/api/register-user";

    public IEnumerator RegisterUser(string username)
    { 
        LoginRequest reqObj = new LoginRequest { username = username };
        string json = JsonUtility.ToJson(reqObj);

        UnityWebRequest www = new UnityWebRequest(CreateUserUrl, "POST");
        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string text = www.downloadHandler.text;
            try
            {
                LoginResponse res = JsonUtility.FromJson<LoginResponse>(text);
                if (res != null && res.ok)
                {
                    Debug.Log($"User Added. session_id = {res.user_Id}");
                }
                else
                {
                    Debug.LogError($"Server did not return ok. Response: {text}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}\nResponse: {text}");
            }
        }
        else
        {
            Debug.LogError($"HTTP error: {www.responseCode} - {www.error}");
            try
            {
                string text = www.downloadHandler.text;
                LoginResponse res = JsonUtility.FromJson<LoginResponse>(text);
                if (res != null && !string.IsNullOrEmpty(res.error))
                {
                    Debug.LogError($"The server provided the following error message: {res.error}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse JSON: {e.Message}");
            }
        }
        yield return StartCoroutine(api.CreateSession(username));
    }
}


