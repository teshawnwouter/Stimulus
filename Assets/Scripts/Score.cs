using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int score;

    public int gold, normal, bad;

    [SerializeField] private SubmitScoreApi scoreApi;
    [SerializeField] private CreateSessionApi sessionApi;

    private void Update()
    {
        CopyResults();
    }

    public void AddScore(int amount)
    {
        score += amount;

    }

    public void CopyResults()
    {
        if (bad > 5)
        {
            StartCoroutine(scoreApi.SubmitScore(CreateSessionApi.session_number, score, gold, normal, bad));
            SceneManager.LoadScene("LoginScreen");
        }
    }
}
