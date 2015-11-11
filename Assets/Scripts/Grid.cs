using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour{	
	public static int height=20;
	public static int width=10;
	
	[Tooltip("Default block prefab.")]
	public Transform blockEtalon;	
	
	
	/// <summary>
	/// The lines contain info about all blocks from bottom to top.
	/// </summary>
	private List<Line> lines=new List<Line>();
	

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
	public bool IsCanMove(Chip activeChip){
		//TODO: Check chip pos for moving
		
		return true;
	}
	
	/// <summary>
	/// Check grid lines and remove filled from lines list
	/// </summary>
	public void RemoveFilledLines(){
		
	}
	
	public void OnDrawGizmos(){
		Vector3 size=new Vector3(width, height, 1f);
		Gizmos.DrawWireCube(transform.position + size*0.5f, size);
	}
}
