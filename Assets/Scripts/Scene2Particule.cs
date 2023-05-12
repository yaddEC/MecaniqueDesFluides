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
    Vector2 pressureForce;
    Vector2 viscosityForce;

    Vector2 gravity = new Vector2(0, -9.81f);
    Vector2 speed;

    // Start is called before the first frame update
    void Start()
    {
        initital_density = 1;
        speed = Vector3.right * initital_speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -1.0f)
            Destroy(gameObject);
    }

    public void CalculateDensityAndPressure()
    {
        density = 0;

        for (int i = 0; i < manager.particules.Count; i++)
        {
            Vector2 vec = transform.position - manager.particules[i].transform.position;
            density += DefaultKernel(vec, kernelMaxLength);
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
            Vector2 vec = transform.position - manager.particules[i].transform.position;
            pressureForce += GradPressureKernel(vec, kernelMaxLength);
            viscosityForce += (manager.particules[i].speed - speed) / manager.particules[i].density * ViscosityKernel(vec, kernelMaxLength);
        }
        pressureForce *= -mass;
        viscosityForce *= dynamicViscosity * mass;
    }

    public void CalculateSpeed()
    {
        speed += (gravity * (pressureForce + viscosityForce) / density) * Time.fixedDeltaTime;
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
        if (magnitude >= 0 || magnitude <= h)
            return -45 / (Mathf.PI * Mathf.Pow(h, 6)) * pos / magnitude * Mathf.Pow(h - magnitude, 2);

        return Vector2.zero;
    }

    float ViscosityKernel(Vector2 pos, float h)
    {
        float magnitude = pos.magnitude;
        if (magnitude >= 0 || magnitude <= h)
            return -45 / (Mathf.PI * Mathf.Pow(h, 6)) * (h - magnitude);

        return 0;
    }
}
