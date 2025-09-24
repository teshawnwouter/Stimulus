using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int score;

    [SerializeField] SubmitScoreApi scoreApi;
    [SerializeField] CreateSessionApi.CreateSessionResponse sessionApi;
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
            
            StartCoroutine(scoreApi.SubmitScore(sessionApi.session_id, score, 100,2,2));
            SceneManager.LoadScene("LoginScreen");
        }
    }
}
