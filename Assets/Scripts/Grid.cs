using UnityEngine;
using System.Collections.Generic;

public class Grid{	
	public static int height=20;
	public static int width=10;
	
	/// <summary>
	/// The lines contain info about all blocks from bottom to top.
	/// </summary>
	private LinkedList<Line> lines=new LinkedList<Line>();
	
	
	public bool IsGameOver(){
		return lines.Count >= height;
	}
	
	/// <summary>
	/// Check grid lines and remove filled from lines list
	/// </summary>
	public void RemoveFilledLines(){
		
	}
}
