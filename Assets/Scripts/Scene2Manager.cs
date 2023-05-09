using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    [SerializeField] private GameObject particule;
    [SerializeField] private float speed_out;
    [SerializeField] private float particuleMass;
    [SerializeField] private float particuleDynamicViscosity;
    [SerializeField] private bool open;

    [HideInInspector] public List<Scene2Particule> particules;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Scene2Particule comp = Instantiate(particule).GetComponent<Scene2Particule>();
            comp.dynamicViscosity = 0;
            comp.initital_speed = 1;
            comp.mass = 1;
            comp.manager = this;
            particules.Add(comp);
        }
    }
}
