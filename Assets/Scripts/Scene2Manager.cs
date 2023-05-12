using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    [SerializeField] private GameObject particule;
    [SerializeField] private float speed_out = 1.0f;
    [SerializeField] private float particuleMass = 1.0f;
    [SerializeField] private float particuleDynamicViscosity = 0.01f;
    [SerializeField] private bool open = false;

    [HideInInspector] public List<Scene2Particule> particules;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Scene2Particule comp = Instantiate(particule, Vector3.zero, Quaternion.identity).GetComponent<Scene2Particule>();
            comp.dynamicViscosity = particuleDynamicViscosity;
            comp.initital_speed = speed_out;
            comp.mass = particuleMass;
            comp.manager = this;
            particules.Add(comp);
        }

        for (int i = 0; i < particules.Count; i++)
            particules[i].CalculateDensityAndPressure();

        for (int i = 0; i < particules.Count; i++)
            particules[i].CalculateForces();

        for (int i = 0; i < particules.Count; i++)
            particules[i].CalculateSpeed();
    }
}
