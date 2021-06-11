using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public enum TypeBomb
{
    RED,
    YELLOW,
    BLACK,
    ORANGE,
    PURPLE
};

public class BombControl : MonoBehaviour
{
    private float raduisBomb; // N (test)
    private float timeReloadBomb; // M (test)
    
    private TypeBomb currType;
    private float timer;
    private bool active;

    private GameObject player;
    private GameObject mainCamera;
    private Vector3 posPlayer;

    public List<Material> materials;

    void Start()
    {
        ChangeTypeBomb();
        timer = 0.0f;
        active = true;

        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        posPlayer = player.transform.position;
    }

    private float distBombPlayer()
    {
        var heading = transform.position - posPlayer;
        return heading.magnitude;
    }

    void Update()
    {
        posPlayer = player.transform.position;

        if (distBombPlayer() <= raduisBomb)
        {
            active = false;
            gameObject.SetActive(false);

            mainCamera.GetComponent<GameControl>().AddBombs((int)currType);
        }
    }

    public void InitBomb(float radius, float time)
    {
        raduisBomb = radius;
        timeReloadBomb = time;
    }

    private TypeBomb CreateTypeBomb()
    {
        return (TypeBomb)Random.Range(0, GetNumberTypeBombs());
    }

    public void ChangeMaterial(TypeBomb type)
    {
        if (type == TypeBomb.RED)
        {
            GetComponent<MeshRenderer>().material = materials[0];
        }
        else if (type == TypeBomb.YELLOW)
        {
            GetComponent<MeshRenderer>().material = materials[1];
        }
        else if (type == TypeBomb.BLACK)
        {
            GetComponent<MeshRenderer>().material = materials[2];
        }
        else if (type == TypeBomb.ORANGE)
        {
            GetComponent<MeshRenderer>().material = materials[3];
        }
        else if (type == TypeBomb.PURPLE)
        {
            GetComponent<MeshRenderer>().material = materials[4];
        }
    }

    public Material GetMaterials(int id)
    {
        return materials[id];
    }

    public void ActiveBomb()
    {
        gameObject.SetActive(true);
    }

    public float GetTimeReload()
    {
        return timeReloadBomb;
    }

    public void AddTimer(float addValue)
    {
        timer += addValue;
    }

    public float GetCurrTimer()
    {
        return timer;
    }

    public bool GetActive()
    {
        return active;
    }

    public void SetActive(bool newValue)
    {
        active = newValue;
    }

    public void ZerosTimer()
    {
        timer = 0.0f;
    }

    public void ChangeTypeBomb()
    {
        currType = CreateTypeBomb();
        ChangeMaterial(currType);
    }

    public int GetNumberTypeBombs()
    {
        return Enum.GetNames(typeof(TypeBomb)).Length;
    }
}
