using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class GameControl : MonoBehaviour
{
    // Enimy
    public GameObject enimyObject;
    public int numberEnimy;
    public int stepHpEnimy; // X (test)
    public int maxHpEnimy;
    public float radiusAttack; // K (test)
    public float timeReloadEnimy; // M (test)

    private GameObject[] enemies;


    // Bombs
    public GameObject bombObject;
    public float raduisBomb; // N (test)
    public float timeReloadBomb; // M (test)
    public int numberBombs;

    private GameObject[] bombs;
    private int idTypeBomb;
    private int summBombs;
    private int[] numberEachTypeBomb;
    private int numberTypeBombs;

    // Random Spawn
    public Vector3 center; // координаты центра
    public Vector3 size; // координаты в которых будут появляться объекты

    // Ball
    private GameObject ball;


    void Start()
    {        
        InitBombs();
        InitEnemies();
        idTypeBomb = -1;
        summBombs = 0;
        numberTypeBombs = bombObject.GetComponent<BombControl>().GetNumberTypeBombs();
        numberEachTypeBomb = new int[numberTypeBombs];

        for (int i = 0; i < numberTypeBombs; i++)
            numberEachTypeBomb[i] = 0;

        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void InitEnemies()
    {
        enemies = new GameObject[numberEnimy];

        for (int i = 0; i < numberEnimy; i++)
        {
            Vector3 position = FindRandomPosition();
            enemies[i] = Instantiate(enimyObject, position, Quaternion.identity);
            enemies[i].GetComponent<EnimyControl>().InitEnimy(maxHpEnimy, stepHpEnimy, radiusAttack, timeReloadEnimy, position);
        }
    }

    private void InitBombs()
    {
        bombs = new GameObject[numberBombs];

        for (int i = 0; i < numberBombs; i++)
        {
            Vector3 position = FindRandomPosition();
            bombs[i] = Instantiate(bombObject, position, Quaternion.identity);
            bombs[i].GetComponent<BombControl>().InitBomb(raduisBomb, timeReloadBomb);
        }
    }

    void Update()
    {
        UpdateBombs();
        UpdateEnemies();
        UpdateTypeBomb();
    }

    void OnGUI()
    {
        string res = "\n";

        for (int i = 0; i < numberTypeBombs; i++)
            res += " " + (numberEachTypeBomb[i]) + "\n";

        GUI.Button(new Rect(10, 10, 60, 80), res);
    }

    private void UpdateBombs()
    {
        for (int i = 0; i < numberBombs; i++)
        {
            bool activeBomb = bombs[i].GetComponent<BombControl>().GetActive();

            if (!activeBomb)
            {
                bombs[i].GetComponent<BombControl>().AddTimer(Time.deltaTime);

                if (bombs[i].GetComponent<BombControl>().GetCurrTimer() >= timeReloadBomb)
                {
                    bombs[i].GetComponent<BombControl>().ZerosTimer();
                    bombs[i].GetComponent<BombControl>().ChangeTypeBomb();
                    bombs[i].GetComponent<BombControl>().SetActive(true);
                    bombs[i].GetComponent<BombControl>().ActiveBomb();
                }
            }
        }
    }

    private void UpdateEnemies()
    {
        for (int i = 0; i < numberEnimy; i++)
        {
            bool activeEnimy = enemies[i].GetComponent<EnimyControl>().GetActive();

            if (!activeEnimy)
            {
                enemies[i].GetComponent<EnimyControl>().AddTimer(Time.deltaTime);

                if (enemies[i].GetComponent<EnimyControl>().GetCurrTimer() >= timeReloadEnimy)
                {
                    enemies[i].GetComponent<EnimyControl>().ZerosTimer();
                    enemies[i].GetComponent<EnimyControl>().SetActive(true);
                    enemies[i].GetComponent<EnimyControl>().ActiveEnimy();
                }
            }
        }
    }

    public void CheckBullet(Vector3 posBullet)
    {
        for (int i = 0; i < numberEnimy; i++)
        {
            enemies[i].GetComponent<EnimyControl>().MinusHpEnimy(posBullet);
        }
    }

    private void UpdateTypeBomb()
    {
        if (summBombs == 0)
        {
            ball.GetComponent<AdditionBall>().DisActiveBall();
        }

        if (summBombs != 0)
        {
            if (idTypeBomb == -1)
            {
                idTypeBomb = 0;
                while (numberEachTypeBomb[idTypeBomb] == 0)
                    idTypeBomb++;
            }

            ball.GetComponent<AdditionBall>().ActiveBall();
            ball.GetComponent<AdditionBall>().ChangeMaterial(idTypeBomb);         
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && idTypeBomb < numberTypeBombs - 1 && summBombs != 0)
        {
            idTypeBomb++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && idTypeBomb > 0 && summBombs != 0)
        {
            idTypeBomb--;
        }
    }

    private Vector3 FindRandomPosition()
    {
        return center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
            Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
    }

    public void AddBombs(int id)
    {
        numberEachTypeBomb[id]++;
        summBombs++;
    }

    public void MinusBombs(int id)
    {
        if (numberEachTypeBomb[id] > 0)
        {
            numberEachTypeBomb[id]--;
            summBombs--;
        }
    }

    public int GetIdTypeBomb()
    {
        return idTypeBomb;
    }

    public int GetSummBombs()
    {
        return summBombs;
    }

    public int GetNumberTypeBomb(int type)
    {
        return numberEachTypeBomb[type];
    }
}
