using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1PhysicManager : MonoBehaviour
{
    float speed;
    float height_total;
    float container_diameter;
    float volume;
    float time;
    float gravity = 9.81f;
    float container_surface;
    float orifice_surface;

    public float orifice_diameter;
    public bool runMode = false;
    public float speed_out;
    public float height;
    [HideInInspector]public float volumetric_flow;

    public Slider heightSlider;
    public Slider widthSlider;
    public Slider HoleSlider;

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

    public void SetRunMode()
    {
        if(HoleSlider.value > 0)
        {
            runMode = !runMode;
            heightSlider.interactable = !heightSlider.interactable;
            widthSlider.interactable = !widthSlider.interactable;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (runMode && height > 0.0f && orifice_diameter > 0)
        {
            orifice_surface = Mathf.PI * orifice_diameter * orifice_diameter / 4;
            volumetric_flow = speed_out * orifice_surface;

            volume -= volumetric_flow * Time.fixedDeltaTime;
            float new_height = volume / container_surface;
            speed = (height - new_height) / Time.fixedDeltaTime;
            speed_out = Mathf.Sqrt(speed * speed + 2.0f * gravity * new_height);
            volumetric_flow = speed_out * orifice_surface;
            height = new_height;
            time += Time.fixedDeltaTime;

            if (height < 0.001f)
            {
                height = 0.0f;
                volumetric_flow = 0.0f;
                volume = 0.0f;
                speed = 0.0f;
            }

        }
    }

    public void UpdateVariables(WaterTower tower)
    {
        height = height_total = tower.height;
        container_diameter = tower.width;
        orifice_diameter = tower.holeSize;

        container_surface = Mathf.PI * container_diameter * container_diameter / 4;

        volume = container_surface * height_total;
        speed_out = Mathf.Sqrt(2.0f * gravity * height_total);

    }
}
