using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour{

	public Transform[] blocks;
	
	public void Awake(){
		blocks=new Transform[Grid.width];
	}
	
	/// <summary>
	/// Destroy all blocks at line.
	/// </summary>
	public void Clear(){
		foreach(Transform block in blocks){
			if(block)
				MonoBehaviour.Destroy(block.gameObject);
		}		
	}
	
	public bool IsFilled(){
		bool result=true;
		for(int i=0; i<blocks.Length; i++)
			result &= blocks[i] != null;
		return result;
	}
	
	public void UpdateLinePos(int newIndex){
		transform.localPosition=new Vector3(0f, newIndex, 0f);
	}
	
	public void Remove(){
		Clear();
		Destroy(gameObject);
	}
}
