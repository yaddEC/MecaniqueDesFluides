using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    [SerializeField] private Scene1PhysicManager physicManager;
    [SerializeField] private WaterTower waterTower;
    [SerializeField] private GameObject particule;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waterTower.waterLevel = physicManager.height;
        if (!physicManager.runMode)
        {
            physicManager.UpdateVariables(waterTower);
        }
        else if (physicManager.height > 0.001f && waterTower.holeSize > 0.0f)
        {
            WaterParticule comp = particule.GetComponent<WaterParticule>();
            comp.initital_speed = physicManager.speed_out;
            comp.volume = physicManager.volumetric_flow * Time.fixedDeltaTime;
            comp.diameter = Mathf.Sqrt(physicManager.volumetric_flow / physicManager.speed_out * 4 / Mathf.PI);
            Instantiate(particule, waterTower.Hole.transform.position, Quaternion.identity);
        }
    }
}
