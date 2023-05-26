using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Particule : MonoBehaviour
{
    public float initital_speed;
    public float mass;
    public float dynamicViscosity; //voir cours (SPH Muller)
    public float kernelMaxLength = 1;
    [HideInInspector] public Scene2Manager manager;

    float density;
    float initital_density;
    float pressure;
    float k = 3.0f; //stiffness coefficient : voir cours (SPH Muller)
    public float restitution = 1.0f; //between 1 and 0

    Vector2 pressureForce;
    Vector2 viscosityForce;

    Vector2 gravity = new Vector2(0, -9.81f);
    public Vector2 speed;

    // Start is called before the first frame update
    void Start()
    {
        initital_density = 1;
        speed = Vector3.right * initital_speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position.y < -40.0f && manager.open)
        {
            transform.position = new Vector3(0, 30.0f, 0) + new Vector3(0.1f, 0.1f, 0) * Random.Range(0, 9);
            speed = Vector3.right * manager.speed_out;
        }
        
        CollisionDetection();
    }

    public void CalculateDensityAndPressure()
    {
        density = 0;

        for (int i = 0; i < manager.particules.Count; i++)
        {
            if (this != manager.particules[i])
            {
                Vector2 vec = transform.position - manager.particules[i].transform.position;
                density += DefaultKernel(vec, kernelMaxLength);
            }
        }
        density *= mass;

        pressure = k * (density - initital_density);
    }

    public void CalculateForces()
    {
        pressureForce = Vector2.zero;
        viscosityForce = Vector2.zero;
        for (int i = 0; i < manager.particules.Count; i++)
        {
            if (this != manager.particules[i])
            {
                Vector2 vec = transform.position - manager.particules[i].transform.position;
                pressureForce += GradPressureKernel(vec, kernelMaxLength);
                viscosityForce += (manager.particules[i].speed - speed) / manager.particules[i].density * ViscosityKernel(vec, kernelMaxLength);

            }
        }
        pressureForce *= -mass;
        viscosityForce *= dynamicViscosity * mass;
    }

    public void CalculateSpeed()
    {
        speed += (gravity + (pressureForce + viscosityForce) / density) * Time.fixedDeltaTime;
        if (!float.IsNaN(speed.x) && !float.IsNaN(speed.y))
            transform.position += new Vector3(speed.x, speed.y, 0.0f) * Time.fixedDeltaTime;
    }

    float DefaultKernel(Vector2 pos, float h)
    {
        float magnitude = pos.magnitude;
        float result = 315 / (64 * Mathf.PI * Mathf.Pow(h, 9));
        if (magnitude >= 0 || magnitude <= h)
            return result * Mathf.Pow(h * h - magnitude * magnitude, 3);
        
        return 0;
    }

    float PressureKernel(Vector2 pos, float h)
    {
        float magnitude = pos.magnitude;
        float result = 15 / (Mathf.PI * Mathf.Pow(h, 6));
        if (magnitude >= 0 || magnitude <= h)
            return result * Mathf.Pow(h  - magnitude, 3);
        
        return 0;
    }

    Vector2 GradPressureKernel(Vector2 pos, float h)
    {
        float magnitude = pos.magnitude;
        if(magnitude!=0)
        {
            Vector2 pressureKernel = -45 / (Mathf.PI * Mathf.Pow(h, 6)) * pos / magnitude * Mathf.Pow(h - magnitude, 2);
            if (magnitude > 0 || magnitude <= h)
                return pressureKernel;
        }

        return Vector2.zero;
    }

    float ViscosityKernel(Vector2 pos, float h)
    {
        float magnitude = pos.magnitude;
        if (magnitude != 0)
        {
            if (magnitude > 0 || magnitude <= h)
                return -45 / (Mathf.PI * Mathf.Pow(h, 6)) * (h - magnitude);
        }

        return 0;
    }

    void CollisionDetection()
    {
        for (int i = 0; i < manager.environement.Count; i++)
        {
            GameObject currentGO = manager.environement[i];
            Vector3 pos = transform.position;
            Vector3 center = currentGO.transform.position;
            float dist = (pos - center).magnitude;
            float radius = currentGO.transform.localScale.x / 2;
            float result = dist * dist - radius * radius;

            if ((currentGO.tag == "Obstacle" && result < 0) || (currentGO.tag == "Container" && result > 0))
            {
                Vector2 normal = (center - pos).normalized;
                float penetrationDepth = Mathf.Abs((center - pos).magnitude - radius);
                transform.position = center + radius * (transform.position - center).normalized;
                speed -= (1 + restitution * penetrationDepth / (Time.fixedDeltaTime * speed.magnitude)) * Vector2.Dot(speed, normal) * normal; //Formule Cours SPH Muller p38
            }
        }
    }
}
