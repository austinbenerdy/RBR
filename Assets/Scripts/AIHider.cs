using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIHider : AIAbstract
{
    public Queue<Vector3> lastTenTargetLocations = new Queue<Vector3>();
    private int sporadicNature = 45;

    private float damageTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        ModifySpeed(-20);
    }

    // Update is called once per frame
    void Update()
    {

        currentCycleTime -= Time.deltaTime;
        if (currentCycleTime < 0)
        {
            ResetCycleTime();
            findTarget();

            if (target != null)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < 2.0)
                {
                    Attack();
                } else if (Vector3.Distance(target.transform.position, transform.position) < 4.0)
                {
                    Vector3 chaseDirection = target.transform.position - transform.position;
                    Move(chaseDirection);
                }
                else
                {
                    RunAwayFrom(target.transform.position);
                }
            }
        }
    }

    protected void RunAwayFrom(Vector3 point)
    {
        
        Vector3 chaseDirection = (point - transform.position) * -1;

        if (Random.Range(0, 1000) < 25)
        {
            Vector3 perpindicularVector = Vector3.Cross(chaseDirection, new Vector3(0, 0, 1)).normalized;
            Vector3 sporadicShift = perpindicularVector * Random.Range(1, sporadicNature);
            if (Random.Range(0, 9) % 2 == 0)
            {
                sporadicShift = sporadicShift * -1;
            }

            momentum = chaseDirection + sporadicShift;
        }

        

        // Vector3 sporadicShift = new Vector3(Random.Range(-359, 359), Random.Range(-359, 359), 0).normalized * sporadicNature;
        // Move(chaseDirection);
        Move(momentum);
    }
}
