using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : TurretAttack
{
    public float width = 0.1f;
    public float length = 20f;

    public bool oneHit = true;
    public float lifetime = 0.1f;

    private bool decaying = false;
    private float decay = 0f;

    private LineRenderer line;

    //private TurretScript turret;
    //private EnemyScript enemy;
    private Vector3 origin, end;

    void Awake()
    {
        form = LASER;

        line = GetComponentInChildren<LineRenderer>();
        line.enabled = false;
    }

    public void SetLaser(TurretScript ts)
    {
        turret = ts;
        target = ts.target;

        length = ts.range*2;

        origin = turret.transform.position;
        end = (target.transform.position - turret.transform.position).normalized * this.length + origin;
        //end = target.transform.position;

        line.SetPosition(0,origin);
        line.SetPosition(1, end);
        line.startWidth = width;
        line.endWidth = width;

        this.damage = ts.bulletDamage;

        //line.enabled = true;

    }

    public void Emit()
    {
        line.enabled = true;

        RaycastHit2D[] targets = Physics2D.RaycastAll(origin, end-origin, length, LayerMask.GetMask("Enemy"));

        if(targets.Length > 0)
        {
            foreach (RaycastHit2D hit in targets)
            {
                EnemyScript es = hit.collider.gameObject.GetComponent<EnemyScript>();
                if (es != null) es.HitBy(this);
            }
        }
        //StartCoroutine(ChangeToTransparent(lifetime));
        Destroy(this.gameObject , lifetime);
        decaying = true;
        decay = lifetime;

        if(clip != null)
        {
            AudioManager.instance.PlaySound(clip);
        }
    }

    void Update()
    {

        if(decaying)
        {
            float r = line.startColor.r;
            float g = line.startColor.g;
            float b = line.startColor.b;
            float a = decay / lifetime;
            line.startColor = new Color(r, g, b,a);
            line.endColor = new Color(r, g, b,a);

            if (decay > 0) decay -= Time.deltaTime;
            
        }
    }

    /*
    IEnumerator ChangeToTransparent(float time)
    {
        float cd = time;
        float r = line.startColor.r;
        float g = line.startColor.g;
        float b = line.startColor.b;
        while(cd > 0)
        {
            float alpha = cd / time;
            line.startColor = new Color(r, g, b, alpha);
            line.endColor = new Color(r, g, b, alpha);

            cd -= Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }*/
}
