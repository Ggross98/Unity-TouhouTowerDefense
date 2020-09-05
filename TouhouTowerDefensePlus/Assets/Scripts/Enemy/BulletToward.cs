using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletToward : MonoBehaviour
{
    private BulletScript bs;

    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        bs = GetComponent<BulletScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle;
        direction = bs.target .transform .position - this.transform .position ;
        if (direction.x > 0)
        {
            angle = -Vector3.Angle(Vector3.up, direction.normalized);
        }
        else
        {
            angle = Vector3.Angle(Vector3.up, direction.normalized);
        }

        transform.eulerAngles = new Vector3(0, 0, angle);
        //transform.Rotate(0, 0, angle);
        //transform.rotation = new Vector3(0, 0, angle);
    }
}
