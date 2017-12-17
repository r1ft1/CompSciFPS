using UnityEngine;

public enum MazeDirection {
	North,
	East,
	South,
	West
}

public static class MazeDirections {
	public const int Count = 4;

	public static MazeDirection RandomDirection {
		get {
			return (MazeDirection)Random.Range(0, Count); //Generates random float from 0 to 4 and casts type as MazeDirection
		}
	}

	private static IntVector2[] vectors = { 
		new IntVector2(0, 1),	//North	
		new IntVector2(1, 0),	//East
		new IntVector2(0, -1),	//South
		new IntVector2(-1, 0)	//West
	};


	public static IntVector2 ToIntVector2 (this MazeDirection direction) { //Converts direction to integer vector via "someDirection.ToIntVector2()"
		return vectors[(int)direction];
	}

}