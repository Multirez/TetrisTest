using UnityEngine;
using System.Collections.Generic;

public class Chip : MonoBehaviour {
	
	public List<Vector2> blockPositions;
	
	[HideInInspector]
	public List<Block> blocks;
	
	public void OnDrawGizmos(){
		if(blockPositions != null){
			Vector3 chipPos=transform.position;
			Vector3 blockPos;
			foreach(Vector2 pos in blockPositions){
				blockPos=chipPos+new Vector3(pos.x, pos.y, 0f); 
				Gizmos.DrawWireCube(blockPos, Vector3.one);
			}
		}
	}
}
