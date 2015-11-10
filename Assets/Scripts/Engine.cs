using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	
	public Grid grid;
	
	
	void Start () {
	
	}
	
	void Update () {
		if(Input.anyKeyDown){
			if(Input.GetButtonDown("Left")){
				Debug.Log("Left");
			}
			if(Input.GetButtonDown("Right")){
				Debug.Log("Right");
			}
			if(Input.GetButtonDown("Down")){
				Debug.Log("Down");
			}
			if(Input.GetButtonDown("Rotate")){
				Debug.Log("Rotate");
			}			
		}
	}
}
