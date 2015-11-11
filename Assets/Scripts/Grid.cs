﻿using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour{	
	public static int height=20;
	public static int width=10;
	
	[Tooltip("Default block prefab.")]
	public Transform blockEtalon;	
		
	private List<Line> lines=new List<Line>();// The lines contain info about all blocks from bottom to top.
	private Rect gridRect;//to determine is movement is out of grid
	
	private void Awake(){
		gridRect=new Rect(-0.45f, -0.45f, width+0.9f, height+0.9f);
	}

	public bool IsGameOver(){
		return lines.Count >= height;
	}
	
	/// <summary>
	/// Remove all blocks on grid.
	/// </summary>
	public void Clear(){
		foreach(Line line in lines)
			line.Clear();
		lines.Clear();
	}
	
	
	/// <summary>
	/// Determines whether this chip is can move down from the current position.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this chip is can move down; otherwise, <c>false</c>.
	/// </returns>
	public bool IsCanMove(Chip activeChip, Vector3 direction){
		Vector3 newBlockPos;
		foreach(Transform blockTrans in activeChip.blocks){
			newBlockPos=transform.TransformPoint(blockTrans.position)+direction;
			if(!gridRect.Contains(newBlockPos) || 
				!IsPositionIsFree(newBlockPos))
				return false;			
		}		
		return true;
	}
	private bool IsPositionIsFree(Vector3 pos){
		//TODO: Check grid position
		return true;
	}
	
	/// <summary>
	/// Check grid lines and remove filled from lines list
	/// </summary>
	public void RemoveFilledLines(){
		
	}
	
	/// <summary>
	/// Add the chip blocks to grid.
	/// </summary>
	/// <param name='toAdd'>
	/// The chip is will be added.
	/// </param>
	public void Add(Chip toAdd){
		
	}
	
	public void OnDrawGizmos(){
		Vector3 size=new Vector3(width, height, 1f);
		Gizmos.DrawWireCube(transform.position + size*0.5f, size);
	}
}
