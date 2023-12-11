using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    [SerializeField] GameObject missionSuccess;
    public bool missionSuccessUI = false;

    public int enemyCount;
    void Start()
    {
        Time.timeScale = 1.0f;
        missionSuccess.SetActive(false);
    }

    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            Time.timeScale = 0;
            missionSuccess.SetActive(true);
            missionSuccessUI = true;
        }
    }
}
