using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class RobotMovement : AIAbstract
{
    public Rigidbody2D rb;
    public Animator animator;
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        ModifySpeed(100);
        health = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        Move(movement);
    }

    protected override void EndLife()
    {
        Debug.Log("WE DIED");
        Destroy(rb);
        Destroy(animator);
    }
}
