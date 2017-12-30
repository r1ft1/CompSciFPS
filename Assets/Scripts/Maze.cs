using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generates contents of Maze
public class Maze : MonoBehaviour {

	//Ractangular grid to fill with maze
	public IntVector2 size;

	//Stores MazeCell prefab to instantiate
	public MazeCell cellPrefab;

	//Delcares 2D array of MazeCells with the name cells
	private MazeCell[,] cells; 

	//Intervals between each cell generated to visual Cells Generation
	public float generationStepDisplay; 

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	//Retrieves maze's cell coordinates
	public MazeCell GetCell (IntVector2 coordinates) { 
		return cells[coordinates.x, coordinates.z];
	}

	private MazeCell CreateCell (IntVector2 _coordinates) {
		//Instantiate creates a clone of the cellPrefab Object which is casted to type MazeCell using 'as'
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell; 
		cells[_coordinates.x, _coordinates.z] = newCell;
		newCell.coordinates = _coordinates;
		newCell.name = "MazeCell " + _coordinates.x + ", " + _coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(_coordinates.x - size.x * 0.5f + 0.5f, 0f, _coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	//Constructs the Maze contents
	public IEnumerator Generate () 
	{
		WaitForSeconds delay = new WaitForSeconds (generationStepDisplay);
		//Creates array of MazeCells using values from Inspector
		cells = new MazeCell [size.x, size.z];  
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	/*
	private void CreatePassageInSameRoom (MazeCell cells, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction.GetOpposite ());
	}
	*/

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell (randomCoordinates));
	}
		
	//Retrieves current cell, check if next move possible without collision, take care of removing cells from list
	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count -1;
		MazeCell currentCell = activeCells[currentIndex];
		//To completely fill maze:
		//Only remove cell from active list when all edges initialized
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt (currentIndex);
			return;
		}
		//Only pick random direction that not yet initialized for current cell
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if (containsCoordinates(coordinates)) 
		{
			MazeCell neighbour = GetCell (coordinates);

			if (neighbour == null) {
				neighbour = CreateCell (coordinates);
				CreatePassage (currentCell, neighbour, direction);
				activeCells.Add (neighbour);
			} 
			//else if (currentCell.room == neighbour.room) {
			//	CreatePassageInSameRoom (currentCell, neighbour, direction);
			//}
			else {
				CreateWall (currentCell, neighbour, direction);
			}
		}

		//If occupied remove from list
		else 
		{
			CreateWall (currentCell, null, direction);
			//No longer removing cell here
				//activeCells.RemoveAt (currentIndex);
		}
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate (wallPrefab) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite());
		}
	}

	//Property - Item of Data held in an object. Method that looks like variable
	//Read only Property which returns a random IntVector2 set of coordinates
	public IntVector2 randomCoordinates 
	{
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool containsCoordinates (IntVector2 coordinate) {  //Checks whether coordinates fall inside Maze
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}
}
