using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public float speed_out = 1.0f;
    public bool open = false;
    public bool run;

    [SerializeField] private GameObject particule;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private float particuleMass = 1.0f;
    [SerializeField] private float particuleDynamicViscosity = 0.01f;
    [SerializeField] private Vector3 firstPos = new Vector3(0.1f,0.1f,0);

    [HideInInspector] public List<Scene2Particule> particules;

    public List<GameObject> environement;

    // Start is called before the first frame update
    void Start()
    {
        particules = new List<Scene2Particule>();
    }

    public void SetSpeed(float s)
    {
        speed_out = s;
    }

    public void SetRun()
    {
        run = !run;
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            timer += Time.deltaTime;
            if (open && timer < 2.0f)
            {
                float rng = Random.Range(0, 9);
                Scene2Particule comp = Instantiate(particule, new Vector3(0, 30) + firstPos * rng, Quaternion.identity).GetComponent<Scene2Particule>();
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
        else
        {
            for (int i = 0; i < particules.Count; i++)
            {
                if (particules[i] != null && particules[i].gameObject != null)
                {
                    Destroy(particules[i].gameObject);
                    particules.RemoveAt(i);
                    i--;
                }
            }

            timer = 0.0f;
        }
        
    }
}
