using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1PhysicManager : MonoBehaviour
{
    float speed;
    float speed_out;
    float volumetric_flow;
    float height;
    float height_total;
    float container_diameter;
    float orifice_diameter;
    float volume;
    float time;
    float gravity = 9.81f;
    float container_surface;
    float orifice_surface;

    // Start is called before the first frame update
    void Start()
    {
        speed = time = 0.0f;

        height = height_total = 4.0f;
        container_diameter = 0.4f;
        orifice_diameter = 0.004f;

        container_surface = Mathf.PI * container_diameter * container_diameter / 4;
        orifice_surface = Mathf.PI * orifice_diameter * orifice_diameter / 4;

        volume = container_surface * height_total;

        speed_out = Mathf.Sqrt(2.0f * gravity * height_total);
        volumetric_flow = speed_out * orifice_surface;
    }

    // Update is called once per frame
    void Update()
    {
        volume -= volumetric_flow * Time.fixedDeltaTime;
        float new_height = volume / container_surface;
        speed = (height - new_height) / Time.fixedDeltaTime;
        speed_out = Mathf.Sqrt(speed * speed + 2.0f * gravity * new_height);
        volumetric_flow = speed_out * orifice_surface;
        height = new_height;
        time += Time.fixedDeltaTime;
    }
}
