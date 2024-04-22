using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetX : MonoBehaviour
{
    Rigidbody targetRb;
    GameManagerX gameManager;

    [SerializeField] ParticleSystem explosionParticle;

    float minValueX = -3.75f;
    float minValueY = -3.75f;
    float spaceBetweenSquares = 2.5f;

    [SerializeField] int pointValue;
    [SerializeField] float timeOnScreen = 1.0f;


    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPosition();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerX>();

        StartCoroutine(RemoveObjectRoutine());
    }

    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }

    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(timeOnScreen);
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.forward * 5, Space.World);
        }
    }
}
