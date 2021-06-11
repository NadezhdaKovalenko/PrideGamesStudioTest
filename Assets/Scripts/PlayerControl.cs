using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speedMove;
    public GameObject bomb;

    private bool move;
    

    void Start()
    {
        move = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            move = false;
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            move = true;

        if (move)
            MovePlayer();
    }

    private void MovePlayer()
    {
        float step = speedMove * Time.deltaTime;
        Vector3 move = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            move = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D))
        {
            move = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            move = new Vector3(transform.position.x, transform.position.y, transform.position.z + step);
        }

        if (Input.GetKey(KeyCode.S))
        {
            move = new Vector3(transform.position.x, transform.position.y, transform.position.z - step);
        }

        transform.position = Vector3.MoveTowards(transform.position, move, step);
    }
}
