using UnityEngine;
using System.Collections.Generic;

public class Chip : MonoBehaviour {
	
	public List<Vector2> blockPositions;
	
	[HideInInspector]
	public List<Transform> blocks;
	
	private void Start(){
		CreateBlocks();
	}
	
	private void CreateBlocks(){
		if(blocks == null)
			blocks=new List<Transform>(blockPositions.Count);
		else // remove old blocks
			DestroyBlocks();
			
		
		Transform etalonBlock=Engine.instance.grid.blockEtalon;
		Vector3 blockPos;
		Quaternion blockRotation=transform.rotation;
		Transform newBlock;
		foreach(Vector2 pos in blockPositions){
			blockPos=transform.position+new Vector3(pos.x, pos.y, 0f);
			newBlock=Instantiate(etalonBlock, blockPos, blockRotation) as Transform;
			newBlock.SetParent(transform);
			blocks.Add(newBlock);
		}
	}
	public void DestroyBlocks(){
		if(blocks!=null){
			foreach(Transform block in blocks)
				Destroy(block.gameObject);
			blocks.Clear();
		}
	}
	
	public void OnDestroy(){
		DestroyBlocks();
	}
				
	
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
