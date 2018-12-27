using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public Planet planet;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (planet)
        {
            planet.Attract(rigidBody, transform);
        }
    }
}
