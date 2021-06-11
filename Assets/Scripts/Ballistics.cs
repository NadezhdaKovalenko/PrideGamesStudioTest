using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    private Vector3 targetScreen;
    private Vector3 targetWorld;
    private Transform sourceTransform;
    private Vector3 sourcePositionOffset;

    public float angleDegree;
    public GameObject bullet;
    public GameObject bomb;
    private float g = Physics.gravity.y;

    private float offset;
    private GameObject mainCamera;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        offset = 3.0f;
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            targetWorld = hit.point;
        }
        sourceTransform = transform;
        sourcePositionOffset = new Vector3(sourceTransform.position.x, sourceTransform.position.y + offset, sourceTransform.position.z);
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //}
        if (Input.GetKeyUp(KeyCode.Mouse0) && GetSummBombs() > 0)
        {
            if (GetNumberTypeBullet() > 0)
                Spawn(bullet);
        }
    }

    private int GetSummBombs()
    {
        return mainCamera.GetComponent<GameControl>().GetSummBombs();
    }

    public void Spawn(GameObject obj)
    {
        Vector3 projection = targetWorld - sourcePositionOffset;
        Vector3 projectionXZ = new Vector3(projection.x, 0.0f, projection.z);

        float x = projectionXZ.magnitude;
        float y = projection.y;

        float angleRad = angleDegree * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleRad) * x) * Mathf.Pow(Mathf.Cos(angleRad), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Vector3 dir = new Vector3(0f, x * Mathf.Tan(angleRad), 0f) + projectionXZ;

        GameObject newBullet = Instantiate(obj, sourcePositionOffset, Quaternion.identity);
        newBullet.GetComponent<BulletControl>().Init(FindTypeBullet());        
        newBullet.GetComponent<Rigidbody>().velocity = dir.normalized * v;
    }

    private int FindTypeBullet()
    {
        return mainCamera.GetComponent<GameControl>().GetIdTypeBomb();
    }

    private int GetNumberTypeBullet()
    {
        int type = FindTypeBullet();
        return mainCamera.GetComponent<GameControl>().GetNumberTypeBomb(type);
    }
}
