using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    private GameObject player;
    public GameObject hairBallPrefab;
    private Vector3 enemyPosition;
    private float gTime = 3f;
    private float gTimer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (gTimer > 0)
        {
            gTimer -= Time.deltaTime;
        }
        else
        {
            gTimer = gTime;
            GenerateEnemy();
        }
    }

    private void GenerateEnemy()
    {
        if (player != null)
        {
            float rotZ = Random.Range(0f, 360f);
            enemyPosition = Quaternion.Euler(0, 0, rotZ) * Vector3.right * Random.Range(16, 20) + player.transform.position;
            Instantiate(hairBallPrefab, enemyPosition, Quaternion.identity);
        }
    }
}
