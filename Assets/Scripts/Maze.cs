using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates contents of Maze
public class Maze : MonoBehaviour {
	
	public IntVector2 size;

	public MazeCell cellPrefab;

	private MazeCell[,] cells; //Delcares multidimensional array of MazeCells with the name cells

	public float generationStepDisplay; //Intervals between each cell generated to visual Cells Generation

	public MazeCell GetCell (IntVector2 coordinates) { //Retrieves maze's cell coordinates
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate () 
	{
		WaitForSeconds delay = new WaitForSeconds (generationStepDisplay);
		cells = new MazeCell [size.x, size.z];  //Creates array of MazeCells using values from Inspector
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
		/*IntVector2 coordinates = randomCoordinates; //Calls Property randomCoordinates
		while (containsCoordinates(coordinates) && GetCell (coordinates) == null) {
			yield return delay;
			CreateCell(coordinates);
			coordinates += MazeDirections.RandomDirection.ToIntVector2(); 
		}*/ 
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell (randomCoordinates));
	}

	//Retrieves current cell, check if next move possible without collision, take care of removing cells from list
	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count -1;
		MazeCell currentCell = activeCells[currentIndex];
		MazeDirection direction = MazeDirections.RandomDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		//If next cell empty
		if (containsCoordinates(coordinates) && GetCell (coordinates) == null) {
			activeCells.Add(CreateCell(coordinates));
		}
		//If occupied remove from list
		else {
			activeCells.RemoveAt (currentIndex);
		}

	}

	private MazeCell CreateCell (IntVector2 _coordinates) {
		//Instantiate creates a clone of the cellPrefab Object which is casted to type Maze using 'as'
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell; 
		cells[_coordinates.x, _coordinates.z] = newCell;
		newCell.coordinates = _coordinates;
		newCell.name = "MazeCell " + _coordinates.x + ", " + _coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(_coordinates.x - size.x * 0.5f + 0.5f, -4.5f, _coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	//Property - Item of Data held in an object
	public IntVector2 randomCoordinates //Read only Property which returns a random IntVector2 set of coordinates
	{
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool containsCoordinates (IntVector2 coordinate) {  //Checks whether coordinates fall inside Maze
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}
}
