using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameStateLog
{
	public string SceenName;
	public float randNum;
	public string startTime;

	List<Vector3> onPositions;
	List<Vector3> offPositions;

	List<float> RoomClearTimes;

	public static void SaveGameStateLog(GameStateLog instace)
	{
		string destination = instace.GetFilePath();
		FileStream file;

		if (File.Exists(destination)) file = File.OpenWrite(destination);
		else file = File.Create(destination);

		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, instace);
		file.Close();
	}

	public static GameStateLog LoadFile(string filePath)
	{
		FileStream file;

		if (File.Exists(filePath)) file = File.OpenRead(filePath);
		else
		{
			Debug.LogError("File not found");
			return null;
		}

		BinaryFormatter bf = new BinaryFormatter();
		GameStateLog instance = (GameStateLog)bf.Deserialize(file);
		file.Close();
		return instance;
	}

	public GameStateLog(string SceenName)
	{
		this.SceenName = SceenName;

		System.Random rnd = new System.Random();
		this.randNum = rnd.Next(1024);

		DateTime StartTime = DateTime.Now;
		startTime = StartTime.Year + "_" + StartTime.Month + "_" + StartTime.Day + "_" + 
							StartTime.Hour + "_" + StartTime.Minute + "_" + StartTime.Second;

		onPositions = new List<Vector3>();
		offPositions = new List<Vector3>();
		RoomClearTimes = new List<float>();
	}

	public string GetFilePath()
	{
		return Application.persistentDataPath + "/" + SceenName + "_" + startTime + "___" + randNum + ".dat";
	}

	public void LogPositions(Vector3 onPosition, Vector3 offPosition)
	{
		onPositions.Add(onPosition);
		offPositions.Add(offPosition);
	}

	public void LogRoomClear(float clearTime)
	{
		RoomClearTimes.Add(clearTime);
	}
}