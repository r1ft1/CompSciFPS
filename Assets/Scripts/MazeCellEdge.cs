using UnityEngine;

//Script tracks connections between cells
public class MazeCellEdge : MonoBehaviour {
	
	//Reference to cell it belongs to and another Reference to other cell it connects with
	public MazeCell cell, otherCell;

	//Direction to remember orientation
	public MazeDirection direction;
	
	//Cells will store edges in an array, 4 edges
	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];


	public MazeCellEdge GetEdge (MazeDirection direction) {
		return edges[(int)direction];
	}

	public void SetCellEdge (MazeDirection direction, MazeCellEdge edge) {
		edges[(int)direction] = edge;
	}
}
