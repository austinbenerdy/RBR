using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = Unity.Mathematics.Random;

public class AISeeker : AIAbstract
{
    public Queue<Vector3> lastTenTargetLocations = new Queue<Vector3>();

    private float damageTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        Init();
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
                }
                else
                {
                    Vector3 chaseDirection = target.transform.position - transform.position;
                    Move(chaseDirection);
                }
            }

            return;
        }
        Search();
        
        // Vector3 targetPosition = target.transform.position;
        //
        // // lastTenTargetLocations.Enqueue(targetPosition);
        // // while (lastTenTargetLocations.Count > 10)
        // // {
        // //     lastTenTargetLocations.Dequeue();
        // // }
        // //
        // // Vector3[] locationsCopy = new Vector3[10];
        // // lastTenTargetLocations.CopyTo(locationsCopy,0);
        // //
        // // Vector3[] normalizedPath = new Vector3[9];
        // //
        // // for (int i = 0; i < locationsCopy.Length; i++)
        // // {
        // //     if (i == 0)
        // //     {
        // //         continue;
        // //     }
        // //
        // //     Vector3 point1 = locationsCopy[i-1];
        // //     Vector3 point2 = locationsCopy[i];
        // //     Vector3 normalizedSingularComponent = new Vector3(point1.x + point2.x, point1.y + point2.y, 0).normalized;
        // //     normalizedPath.SetValue(normalizedSingularComponent, i-1);
        // // }
        // //
        // // Vector3 chaseDirection = new Vector3();
        // // for (int j = 0; j < normalizedPath.Length; j++)
        // // {
        // //     chaseDirection = chaseDirection + normalizedPath[j];
        // // }
        // // Move(targetPosition + chaseDirection - transform.position);
        //
        //
        // if (Vector3.Distance(targetPosition, transform.position) < 2.0)
        // {
        //     damageTimer -= Time.deltaTime;
        //     
        //     Debug.Log("Is Alive? " + target.isAlive());
        //
        //     if (damageTimer < 0 && !target.isAlive())
        //     {
        //         Debug.Log("WE DID DAMAGE");
        //         target.TakeDamage(10);
        //         damageTimer = 1;
        //     }
        // }
        // else
        // {
        //     Vector3 chaseDirection = targetPosition - transform.position;
        //     Move(chaseDirection);
        // }
    }

    protected void Search()
    {
        Vector3 direction = population[0].transform.position;
        Move(direction);
    }
}
