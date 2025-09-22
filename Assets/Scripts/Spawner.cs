using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> notesToSpawn = new List<GameObject>();
    [SerializeField] private List<GameObject> notePrefab = new List<GameObject>();
    [SerializeField] private GameObject spawnedNote;
    private int keyboardIndex;
    public float spawnInterval;


    private void Start()
    {
        StartCoroutine(SpawnNotes());
    }

    private IEnumerator SpawnNotes()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            keyboardIndex = Random.Range(0, notesToSpawn.Count);
            spawnedNote = notePrefab[Random.Range(0, notePrefab.Count)];

            spawnedNote.GetComponent<NotesToPlay>().notekey = notesToSpawn[keyboardIndex].GetComponent<KeysScript>().noteKey;
            Instantiate(spawnedNote,
                new Vector3(notesToSpawn[keyboardIndex].transform.position.x
                , notesToSpawn[keyboardIndex].transform.position.y + 7.5f
                , notesToSpawn[keyboardIndex].transform.position.z)
                , Quaternion.identity);

        }

    }
}
