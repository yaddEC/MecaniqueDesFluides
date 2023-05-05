using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : MonoBehaviour
{
    public float height;
    public float width;
    public float supportHeight;
    public float holeSize;
    public float waterLevel;

    public GameObject VerLeft;
    public GameObject VerRight;
    public GameObject HorTop;
    public GameObject HorBottom;
    public GameObject Support;
    public GameObject Hole;
    public GameObject WaterLevel;

    private Vector3 PosVerRight;
    private Vector3 PosVerLeft;
    private Vector3 PosHorTop;
    private Vector3 PosHorBottom;
    private Vector3 PosHole;
    private Vector3 PosSupport;
    private Vector3 PosWaterLevel;

    // Start is called before the first frame update
    void Start()
    {
        PosVerRight = VerRight.transform.localPosition;
        PosVerLeft = VerLeft.transform.localPosition;
        PosHorTop = HorTop.transform.localPosition;
        PosHorBottom = HorBottom.transform.localPosition;
        PosHole = Hole.transform.localPosition;
        PosSupport = Support.transform.localPosition;
        PosWaterLevel = WaterLevel.transform.localPosition;
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
        VerLeft.transform.localPosition = new Vector3(PosVerLeft.x - (width * 0.5f) - 0.1f + 1.25f, PosVerLeft.y + Support.transform.localScale.y * 0.5f + (height * 0.5f)-6 , 0);
        VerLeft.transform.localScale = new Vector3(0.3f, height, 1);
        VerRight.transform.localPosition = new Vector3(PosVerRight.x + width * 0.5f + 0.1f - 1.25f, PosVerRight.y + Support.transform.localScale.y * 0.5f + (height * 0.5f)-6 , 0);
        Hole.transform.localPosition = new Vector3(VerRight.transform.localPosition.x, PosHole.y - (Support.transform.localScale.y * 0.5f - 0.3f - Hole.transform.localScale.y * 0.5f)-0.244f, 0);
        VerRight.transform.localScale = new Vector3(0.3f, height, 1);
        WaterLevel.transform.localScale = new Vector3(width, waterLevel, 1);
        WaterLevel.transform.localPosition = new Vector3(PosWaterLevel.x, PosWaterLevel.y+ waterLevel*0.5f-2.5f, 1);


    }

    void HorizontalUpdate()
    {
       HorTop.transform.localPosition = new Vector3(PosHorTop.x,PosHorTop.y+supportHeight * 0.5f + height+0.15f-7, 0);
       HorTop.transform.localScale = new Vector3(width,0.3f, 1);
       HorBottom.transform.localPosition = new Vector3(PosHorBottom.x, PosHorBottom.y+ supportHeight *0.5f-0.15f-5f, 0);
       HorBottom.transform.localScale = new Vector3(width, 0.3f, 1);
    }



}
