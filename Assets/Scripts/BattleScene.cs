using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;

    void Start()
    {
        Instantiate(enemyPrefab, new Vector3(-3, 3, 0), Quaternion.identity);
        Instantiate(enemyPrefab, new Vector3(0, 3, 0), Quaternion.identity);
        Instantiate(enemyPrefab, new Vector3(3, 3, 0), Quaternion.identity);
    }

    void Update()
    {

    }
}
