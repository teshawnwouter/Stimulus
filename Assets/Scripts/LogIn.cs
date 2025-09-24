using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    [SerializeField] private Registering _sessionApi;
    [SerializeField] private CreateSessionApi api;
    [SerializeField] private TMP_InputField _inputField;

    public void SubmitName()
    {
        StartCoroutine(_sessionApi.RegisterUser(_inputField.text));
        SceneManager.LoadScene("MainGame");
    }

}
