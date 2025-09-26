using UnityEngine;

public class NotesToPlay : MonoBehaviour
{
    private NoteCounter counter;
    public int pointsGiven;
    public float speed;
    public bool goodPlay, perfectPlay;
    private Score score;

    public KeyCode notekey;


    private void Start()
    {
        counter = FindFirstObjectByType<NoteCounter>();
        counter.notes.Add(this);
        score = FindAnyObjectByType<Score>();
    }

    void FixedUpdate()
    {
        transform.localPosition += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Despawner"))
        {
            score.AddScore(-5);
            score.bad++;
            counter.notes.Remove(this);
            Destroy(this.gameObject);
        }

        if (collision.collider.CompareTag("Good"))
        {
            goodPlay = true;
        }

        if (collision.collider.CompareTag("Perfect") && !collision.collider.CompareTag("Good"))
        {
            perfectPlay = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Good"))
        {
            goodPlay = false;
        }

        if (collision.collider.CompareTag("Perfect") && !collision.collider.CompareTag("Good"))
        {
            perfectPlay = false;
        }
    }

    public void PlayedNote()
    {
        if (this.gameObject.CompareTag("Normal"))
        {
            score.normal++;
        }
        if (this.gameObject.CompareTag("Gold"))
        {
            score.gold++;
        }
        if (this.gameObject.CompareTag("Bad"))
        {
            score.bad++;
        }
        counter.notes.Remove(this);
        Destroy(this.gameObject);
    }
}
