using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    public float total_time;

    //Use this for initialization
    void Start () {
  
    }
	
	void Update () {
        total_time -= Time.deltaTime;
        Debug.Log(total_time);
	}
  
}
