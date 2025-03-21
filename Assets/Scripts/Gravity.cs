using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;

    const float G = 0.00667f;

    [SerializeField] bool planets = false;
    [SerializeField] int orbitSpeed = 1000;

    public static List<Gravity> gravityObjectList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectList == null)
        {
            gravityObjectList = new List<Gravity>();
        }

        gravityObjectList.Add( this );

        //orbiting
        if (!planets)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }

    private void FixedUpdate()
    {
        foreach ( var obj in gravityObjectList ) 
            {
                //call Attract
                if (obj != this)
                Attract(obj);
            }

    }
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * ( rb.mass * otherRb.mass/ MathF.Pow( distance,2));
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}
