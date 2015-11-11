﻿using UnityEngine;
using System.Collections.Generic;

public class Line{

	public Transform[] blocks;
	
	public Line(){
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
}
