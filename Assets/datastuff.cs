using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class datastuff : MonoBehaviour
{
    public GameObject dataObj;
    public string pathName = "ff.txt";
    public bool status = false;
    private Rigidbody rb;
    private HingeJoint hinge;
    private JointSpring hingeSpring;
    public float hSliderValue, sSliderValue;
    private Vector3 angVelo;
    private GUIStyle guiStyle = new GUIStyle();
    private float timer = 0.0f;
    private float seconds;
    
    // Start is called before the first frame update
    void Start(){
        rb = dataObj.GetComponent<Rigidbody>();
        hinge = dataObj.GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
    }

    
    void Update(){
        rb.angularDrag=hSliderValue;
        angVelo=rb.angularVelocity;
        if (status){
            dataW();
        }
    }
    
    void dataW() {
        using (StreamWriter file=new StreamWriter(pathName,true)){
            timer += Time.deltaTime;
            seconds = (timer % 60);
            string output = string.Format("{0},X,{1},t",dataObj.transform.localEulerAngles.x,seconds);
            file.WriteLine(output);  
            status=true;
        }
    }
    
    void clearD() {
        using (StreamWriter file=new StreamWriter(pathName,false)){
            file.WriteLine(); 
        }
    }
    
    void OnGUI(){
        if (GUI.Button(new Rect(100,Screen.height-40,80,30),"WRITE")) {
            status=true;
            Debug.Log("Write");
        }
        if (GUI.Button(new Rect(200,Screen.height-40,80,30),"CLEAR")) {
            clearD();
            Debug.Log("Clear");
        }
        if (GUI.Button(new Rect(300,Screen.height-40,80,30),"STOP")) {
            status=false;
            Debug.Log("STOP");
        }
        GUI.contentColor = Color.black;
        guiStyle.fontSize = 20;

        GUI.Label(new Rect(600,Screen.height-60,80,30), "Angular Drag",guiStyle);
        hSliderValue = GUI.HorizontalSlider(new Rect(600,Screen.height-40,80,30), hSliderValue, 0.0F, 5.0F);

        GUI.Label(new Rect(800,Screen.height-60,80,30), "Spring",guiStyle);
        sSliderValue = GUI.HorizontalSlider(new Rect(800,Screen.height-40,80,30), sSliderValue, 0.0F, 200.0F);
        if(sSliderValue!=0){
            hingeSpring.spring = sSliderValue;
            hinge.spring = hingeSpring;
            hinge.useSpring = true;  
        }
        else{
            hingeSpring.spring = 0;
            hinge.useSpring = false;
        }
          
        GUI.Label(new Rect(300,Screen.height-120,100,30), "Angular Velocity",guiStyle);
        GUI.Label(new Rect(300,Screen.height-100,100,30), angVelo.ToString(),guiStyle);

    }
}
