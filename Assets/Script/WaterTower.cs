using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : MonoBehaviour
{
    public float height;
    public float width;
    public float supportHeight;
    public float holeSize;

    public GameObject VerLeft;
    public GameObject VerRight;
    public GameObject HorTop;
    public GameObject HorBottom;
    public GameObject Support;
    public GameObject Hole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VerticalUpdate();
        HorizontalUpdate();
    }


    void VerticalUpdate()
    {
        Hole.transform.localScale = new Vector3(Hole.transform.localScale.x, holeSize, Hole.transform.localScale.z);
        Support.transform.localScale = new Vector3(0.3f, supportHeight, 1);
        VerLeft.transform.position = new Vector3(-width * 0.5f-0.1f, supportHeight * 0.5f + (height * 0.5f), 0);
        VerLeft.transform.localScale = new Vector3(0.3f, height, 1);
        VerRight.transform.position = new Vector3(width * 0.5f+0.1f, supportHeight * 0.5f + (height * 0.5f), 0);
        Hole.transform.position = new Vector3(width * 0.5f + 0.1f, supportHeight * 0.5f +0.3f+ Hole.transform.localScale.y*0.5f, 0);
        VerRight.transform.localScale = new Vector3(0.3f, height, 1);

    }

    void HorizontalUpdate()
    {
       HorTop.transform.position = new Vector3(HorTop.transform.position.x, supportHeight * 0.5f + height-0.15f, 0);
       HorTop.transform.localScale = new Vector3(width,0.3f, 1);
       HorBottom.transform.position = new Vector3(HorBottom.transform.position.x, supportHeight*0.5f+0.15f, 0);
       HorBottom.transform.localScale = new Vector3(width, 0.3f, 1);
    }



}
