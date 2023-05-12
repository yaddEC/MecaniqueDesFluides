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
            if (waterTower.waterLevel >= waterTower.holeSize)
            {
                particule.transform.localScale = new Vector3(waterTower.holeSize * 0.6f, waterTower.holeSize * 0.6f, particule.transform.localScale.z);
                Instantiate(particule, waterTower.Hole.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(particule, new Vector3 (waterTower.Hole.transform.position.x, waterTower.WaterLevel.transform.position.y, waterTower.Hole.transform.position.z), Quaternion.identity);
                particule.transform.localScale = new Vector3(waterTower.WaterLevel.transform.localScale.y * 0.6f, waterTower.WaterLevel.transform.localScale.y * 0.6f, particule.transform.localScale.z);
            }
        }
    }
}
