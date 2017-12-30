using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class HighScoreManager : MonoBehaviour {

	private string connectionString;

	private List<HighScore> highScores = new List<HighScore> ();

	// Use this for initialization
	void Start () {
		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";
		//InsertScore ("James", 10000);
		GetScores ();
		DeleteScore (3);
	}
	
	// Update is called once per frame
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

}
