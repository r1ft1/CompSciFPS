using UnityEngine;

//Directions in clockwise order of compass
public enum MazeDirection {
	North, 	//0
	East,  	//1
	South,	//2
	West	//3
}

public static class MazeDirections {

	public const int Count = 4;

	//Generates random float from 0 to 3, casts type as MazeDirection and returns Direction
	public static MazeDirection RandomDirection {
		get {
			//eg: return (MazeDirection)3 --> return West
			return (MazeDirection)Random.Range(0, Count); 
		}
	}

	private static IntVector2[] directionVectors = { 
		new IntVector2(0, 1),	//North	
		new IntVector2(1, 0),	//East
		new IntVector2(0, -1),	//South
		new IntVector2(-1, 0)	//West
	};

	//Converts enumDirection to int vector via "someDirection.ToIntVector2()"
	public static IntVector2 ToIntVector2 (this MazeDirection direction) { 
		return directionVectors[(int)direction];
	}

	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};

	public static MazeDirection GetOpposite (this MazeDirection direction) {
		return opposites[(int)direction];
	}

	//Quaternions are used to represent rotations
	//Array of 4 different rotations about y axis
	private static Quaternion[] rotations = {
		Quaternion.identity, 			//0 degrees, no rotation
		Quaternion.Euler (0f, 90f, 0f), //90 degrees
		Quaternion.Euler (0f, 180f, 0f),//180 degrees
		Quaternion.Euler (0f, 270f, 0f) //270 degrees
	};

	public static Quaternion ToRotation (this MazeDirection direction)
	{
		return rotations [(int)direction];
	}

}