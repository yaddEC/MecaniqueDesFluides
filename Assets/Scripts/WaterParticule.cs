using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticule : MonoBehaviour
{
    public float volume;
    public float diameter;
    public float initital_speed;
    public float volumetricMass = 1000.0f;
    float gravity = 9.81f;
    float time = 0.0f;
    Vector3 speed;
    Vector3 acceleration;
    // Start is called before the first frame update
    void Start()
    {
        acceleration = Vector3.up * (-volume * volumetricMass * gravity);
        speed = Vector3.right * initital_speed;
        transform.localScale *= diameter;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        speed += acceleration * Time.fixedDeltaTime;
        transform.position += speed * Time.fixedDeltaTime;

        if (time > 10.0f || transform.position.y < -1.0f)
            Destroy(gameObject);
    }
}
