using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int score;

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
        if (score > 25)
        {
            StartCoroutine(scoreApi.SubmitScore(CreateSessionApi.session_number, score, 100, 2, 2));
            SceneManager.LoadScene("LoginScreen");
        }
    }
}
