﻿using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour{	
	public static int height=20;
	public static int width=10;
	
	[Tooltip("Default block prefab.")]
	public Transform blockEtalon;
	[Tooltip("Default line prefab.")]
	public Line lineEtalon;
	[Tooltip("Link to background for scale on start.")]
	public Transform backTrans;
		
	private List<Line> lines=new List<Line>();// The lines contain info about all blocks from bottom to top.
	private Rect gridRect;//to determine when movement is out of grid
	
	private void Awake(){
		gridRect=new Rect(-0.45f, -0.45f, width-0.1f, height+5);//height +5 due chips starts over grid, higher of the grid 
	}
	
	private void Start(){
		if(backTrans){
			backTrans.localScale=new Vector3(width*0.1f, 1f, height*0.1f);
			backTrans.localPosition=new Vector3(width*0.5f-0.5f, height*0.5f-0.5f, 0.5f);			
		}
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
	public bool IsCanRotate(Chip chip, Vector3 eulerAngles){
		Vector3 blockPos;
		chip.transform.Rotate(eulerAngles, Space.Self);
		foreach(Transform blockTrans in chip.blocks){
			blockPos=transform.TransformPoint(blockTrans.position);
			if(!gridRect.Contains(blockPos) || 
				!IsPositionIsFree(blockPos))
			{
				chip.transform.Rotate(-eulerAngles, Space.Self);
				return false;
			}
						
		}
		chip.transform.Rotate(-eulerAngles, Space.Self);
		return true;
	}
	private bool IsPositionIsFree(Vector3 pos){
		//Check grid position
		int lineIndex=Mathf.RoundToInt(pos.y);
		if(lineIndex>=lines.Count)
			return true;//the grid is not contain line with this index it's means avaliable positions
		int blockIndex=Mathf.RoundToInt(pos.x);
		if(lines[lineIndex].blocks[blockIndex]==null)
			return true;//pos is not used
		return false;		
	}
	
	/// <summary>
	/// Check grid lines and remove filled from lines list
	/// </summary>
	private void RemoveFilledLines(Line[] checkList){
		int lowLine=lines.Count-1;
		int count=0;
		foreach(Line line in checkList){
			if(line.IsFilled()){
				//Debug.Log("Line is filled, remove it.");
				count++;
				lines.Remove(line);
				lowLine=Mathf.Min(lowLine,
					Mathf.RoundToInt(line.transform.localPosition.y));
				line.Remove();
			}
		}
		for(int i=lowLine; i<lines.Count; i++)
			lines[i].UpdateLinePos(i);
		
		//update score		
		if(count>0)
			Engine.instance.AddScore(count);
	}
	
	/// <summary>
	/// Add the chip blocks to grid.
	/// </summary>
	/// <param name='toAdd'>
	/// The chip is will be added.
	/// </param>
	public void Add(Chip toAdd){
		int lineIndex, blockIndex;
		Vector3 blockPos;
		Line tempLine;
		HashSet<Line> affectedLines=new HashSet<Line>();
		foreach(Transform blockTrans in toAdd.blocks){
			blockPos=transform.TransformPoint(blockTrans.position);
			lineIndex=Mathf.RoundToInt(blockPos.y);
			if(lineIndex>=lines.Count){
				for(int i=lineIndex-lines.Count; i>=0; i--){
					tempLine=Instantiate(lineEtalon)as Line;
					tempLine.transform.SetParent(transform);
					tempLine.transform.rotation=Quaternion.identity;
					tempLine.transform.localPosition=new Vector3(0f, lines.Count, 0f);
					lines.Add(tempLine);					
				}
			}
			tempLine=lines[lineIndex];
			affectedLines.Add(tempLine);
			
			blockIndex=Mathf.RoundToInt(blockPos.x);
			tempLine.blocks[blockIndex]=blockTrans;
			blockTrans.SetParent(tempLine.transform);
		}
		//remove chip
		toAdd.blocks.Clear();
		Destroy(toAdd.gameObject);
		//check lines for remove
		Line[] checkList=new Line[affectedLines.Count];
		affectedLines.CopyTo(checkList);
		RemoveFilledLines(checkList);
	}
	
	public void OnDrawGizmos(){
		Vector3 size=new Vector3(width, height, 1f);
		Gizmos.DrawWireCube(transform.position + size*0.5f, size);
	}
}
