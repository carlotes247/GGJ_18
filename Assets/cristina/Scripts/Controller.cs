using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    [SerializeField]
    private float time;

    [SerializeField]
    private float total_time;

    //Use this for initialization
    void Start () {
  
    }
	
	void Update () {
        total_time -= Time.time;
        Debug.Log(total_time);
	}
  
}
