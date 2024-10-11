using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet : MonoBehaviour
{
    public UIControler uiControler;
    public Vector3 initialPos;
    public int vTheta;
    public float degres;
    public float radious;
    public Vector3 center;
    private Boolean editing;
    public Boolean rotate;
    private float initialRadians;
    // Start is called before the first frame update
    void Start()
    {
        degres = 0;
        editing = true;
        rotate = false;
        //initialPos = transform.position;// new Vector3(0, 0, 0);
        //transform.position = new Vector3(radious, 0, 0);// initialPos;
    }
    public void StartRotate()
    {
        //var value = (float)transform.position.z / transform.position.y;
        //initialRadians = (float)Mathf.Atan(value);
        //transform.position = initialPos;
        var h = Mathf.Atan2(transform.position.z, transform.position.x);
        degres = (float)(h / Math.PI * 180.0f);
        //Debug.Log("planet");
        //Debug.Log(initialPos);
        //Debug.Log(initialPos.y);
        //Debug.Log(transform.position.z);
        //Debug.Log(transform.position.y);
        //Debug.Log("planet");
        //Debug.Log(radious);
        //Debug.Log(value);
        Debug.Log(degres);
        
        editing = false;
        radious = Vector3.Distance(center, transform.position);
        rotate = true;
    }
    //// Update is called once per frame
    void Update()
    {
        
        if (rotate)
        {

            
            degres += vTheta * Time.deltaTime;
            var radians = Math.PI * degres / 180.0f;
            radians += initialRadians;
            float cos = (float)Math.Round(Math.Cos(radians), 2);
            float sin = (float)Math.Round(Math.Sin(radians), 2);
            transform.position = new Vector3(cos * radious + center.x, 0f, sin * radious + center.z);
            transform.Rotate(0.0f, vTheta/10, 0.0f, Space.World);
        }
        else if(editing)
        {
            if (Input.GetMouseButtonDown(0)) { editing = false; }
            
            transform.position = uiControler.getMousePos();
        }
    }



    //private Vector3 screenPosition;
    //private Vector3 worldPosition;
    //Plane invisiblePlane = new Plane(Vector3.down, 0);
    //Vector3 getMousePos()
    //{
    //    screenPosition = Input.mousePosition;
    //    Ray ray = Camera.main.ScreenPointToRay(screenPosition);
    //    if (invisiblePlane.Raycast(ray, out float distance))
    //    {
    //        worldPosition = ray.GetPoint(distance);
    //    }
    //    return worldPosition;
    //}
}