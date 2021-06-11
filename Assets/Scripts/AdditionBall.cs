using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionBall : MonoBehaviour
{
    public GameObject bomb;
    private bool active;

    void Start()
    {
        active = true;
    }

    void Update()
    {
    }

    public void ActiveBall()
    {
        if (!active)
        {
            active = true;
            gameObject.SetActive(true);
        }
    }

    public void DisActiveBall()
    {
        if (active)
        {
            active = false;
            gameObject.SetActive(false);
        }
    }

    public void ChangeMaterial(int type)
    {
        gameObject.GetComponent<MeshRenderer>().material = bomb.GetComponent<BombControl>().GetMaterials(type);
    }    
}
