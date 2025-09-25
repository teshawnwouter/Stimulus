using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    [SerializeField] private Registering register;
    [SerializeField] private TMP_InputField _inputField;

    public void SubmitName()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return StartCoroutine(register.RegisterUser(_inputField.text));

                SceneManager.LoadScene("MainGame");

    }

}
