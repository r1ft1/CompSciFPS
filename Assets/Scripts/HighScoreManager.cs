using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour {

	private string connectionString;

	private List<HighScore> highScores = new List<HighScore> ();

	//Variable to hold to prefab of the Score Template, for instantiation
	public GameObject scorePrefab;

	public Transform scoreParent;

	public int topRanks;

	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";
		ShowScores ();
	}

	void Update () {
		
	}

	private void InsertScore(string name, int newScore) 
	{
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = string.Format ("INSERT INTO HighScores(Name,Score) VALUES(\"{0}\",\"{1}\")", name, newScore);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}

	}

	private void GetScores()
	{
		highScores.Clear ();

		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM HighScores";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
					while (reader.Read())
					{
						highScores.Add (new HighScore (reader.GetInt32 (0), reader.GetInt32 (2), reader.GetString (1), reader.GetDateTime (3)));
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
		highScores.Sort ();
	}

	private void DeleteScore(int id)
	{
		//DELETE FROM HighScores WHERE PlayerID = "4"
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) 
		{
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) 
			{
				string sqlQuery = string.Format ("DELETE FROM HighScores WHERE PlayerID = \"{0}\"", id);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConnection.Close ();
			}
		}
	}

	private void ShowScores()
	{
		GetScores ();
		for (int i = 0; i < topRanks; i++) {
			if (i <= highScores.Count-1) {
				GameObject tmpObject = Instantiate (scorePrefab);

				HighScore tmpScore = highScores [i];

				tmpObject.GetComponent<HighScoreScript> ().SetScore (tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString ());
				tmpObject.transform.SetParent (scoreParent);

				tmpObject.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			}
		}
	}

}
