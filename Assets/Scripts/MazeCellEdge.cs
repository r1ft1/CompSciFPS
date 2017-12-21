using UnityEngine;

//Script tracks connections between cells
//Abstract Class: does not allow object instances of itself to be created
public abstract class MazeCellEdge : MonoBehaviour {
	
	//Reference to cell it belongs to and another Reference to other cell it connects with
	public MazeCell cell, otherCell;

	//Direction to remember orientation
	public MazeDirection direction;
	

	//Makes the edges - children of their cells and place them in same location. Parent: Cell -> Child: Edge
	public void Initialize (MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		this.cell = cell;
		this.otherCell = otherCell;
		this.direction = direction;
		cell.SetEdge (direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
	}
}
