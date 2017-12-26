using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {
	public IntVector2 coordinates;

	private int initializedEdgeCount;

	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}


	//Cells will store edges in an array, 4 edges
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges [(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	//Unbiased random Uninitialized direction: 
	//By randomly deciding how many Unintialized directions to skip.
	//This will work when there are Uninitialized edges remaining
	public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range (0, MazeDirections.Count - initializedEdgeCount);
			//Loop through edges array, if find a hole: check if out of skips
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					//if TRUE: this is RUD
					if (skips == 0) {
						return (MazeDirection)i;
					}
					//if FALSE: skips--
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left");
		}
	}



}
