using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public GameObject bomb;
    private int type;

    void Start()
    {        
    }

    void Update()
    {        
    }

    public void Init(int newType)
    {
        type = newType;
        gameObject.GetComponent<MeshRenderer>().material = bomb.GetComponent<BombControl>().GetMaterials(type);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Respawn")
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            mainCamera.GetComponent<GameControl>().CheckBullet(transform.position);
            mainCamera.GetComponent<GameControl>().MinusBombs(type);
            Destroy(gameObject);
        }
    }

    public void ChangeMaterial(int type)
    {
        gameObject.GetComponent<MeshRenderer>().material = bomb.GetComponent<BombControl>().GetMaterials(type);
    }
}
