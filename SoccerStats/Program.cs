﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SoccerStats
{
	class Program
	{
		static void Main(string[] args)


		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);

			//var fileName = Path.Combine(directory.FullName, "SoccerGameResults.csv");
			//var fileContents = ReadSoccerResults(fileName);


			//fileName = Path.Combine(directory.FullName, "players.json");
			//var players = DeserializePlayers(fileName);

			var fileName = Path.Combine(directory.FullName, "dawson89.json");
			var badges = DeserializeBadges(fileName);




			//var topTenPlayers = GetTopTenPlayers(players);
			//foreach (var player in topTenPlayers)
			//{
			//	Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);
			//}
			//fileName = Path.Combine(directory.FullName, "topten.json");
			//SerializePlayerToFile(topTenPlayers, fileName);



			// Custom json export start
			// var bottomTenPlayers = GetBottomTenPlayers(players);
			// foreach (var player in bottomTenPlayers)
			//{
			//	Console.WriteLine("Name: " + player.FirstName + " PPG: " + player.PointsPerGame);
			//}
			// fileName = Path.Combine(directory.FullName, "bottomten.json");
			// SerializePlayerToFile(bottomTenPlayers, fileName);
			// Custom json export end

			var bottomTenBadges = GetBottomTenBadges(badges);
			foreach (var badge in bottomTenBadges)
			{
				Console.WriteLine("Name: " + badge.Name + " ID Number: " + badge.Id);
			}

		}

		public static List<Badge> DeserializeBadges(string fileName)
		{
			var badges = new List<Badge>();

			var serializer = new JsonSerializer();
			using (var reader = new StreamReader(fileName))
			using (var jsonReader = new JsonTextReader(reader))
			{
				badges = serializer.Deserialize<List<Badge>>(jsonReader);

			}
			return badges;
		}
	
		public static string ReadFile(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				return reader.ReadToEnd();
			}
		}

		public static List<Badge> GetBottomTenBadges(List<Badge> badges)
		{
			var bottomTenBadges = new List<Badge>();
			badges.Sort(new PlayComp());
			int counter = 0;
			foreach (var badge in badges)
			{
				bottomTenBadges.Add(badge);
				counter++;
				if (counter == 10)
					break;
			}
			return bottomTenBadges;
		}


		//public static List<GameResult> ReadSoccerResults(string fileName)
		//{
		//	var soccerResults = new List<GameResult>();
		//	using (var reader = new StreamReader(fileName))
		//	{
		//		string line = "";
		//		reader.ReadLine();
		//		while ((line = reader.ReadLine()) != null)
		//		{
		//			var gameResult = new GameResult();
		//			string[] values = line.Split(',');
	
		//			DateTime gameDate;
		//			if (DateTime.TryParse(values[0], out gameDate))
		//			{
		//				gameResult.GameDate = gameDate;
		//			}
		//			gameResult.TeamName = values[1];
		//			HomeOrAway homeOrAway;
		//			if (Enum.TryParse(values[2], out homeOrAway))
		//			{
		//				gameResult.HomeOrAway = homeOrAway;
		//			}
		//			int parseInt;
		//			if (int.TryParse(values[3], out parseInt))
		//			{
		//				gameResult.Goals = parseInt;
		//			}
		//			if (int.TryParse(values[4], out parseInt))
		//			{
		//				gameResult.GoalAttempts = parseInt;
		//			}
		//			if (int.TryParse(values[5], out parseInt))
		//			{
		//				gameResult.ShotsOnGoal = parseInt;
		//			}
		//			if (int.TryParse(values[6], out parseInt))
		//			{
		//				gameResult.ShotsOffGoal = parseInt;
		//			}

		//			double possessionPercent;
		//			if (double.TryParse(values[7], out possessionPercent))
		//			{
		//				gameResult.PosessionPercent = possessionPercent;
		//			}
		//			soccerResults.Add(gameResult);
		//		}
		//	}
		//	return soccerResults;
		//}
		//public static List<Player> DeserializePlayers(string fileName)
		//{
		//	var players = new List<Player>();
		//	var serializer = new JsonSerializer();
		//	using (var reader = new StreamReader(fileName))
		//	using (var jsonReader = new JsonTextReader(reader))
		//	{
		//		players = serializer.Deserialize<List<Player>>(jsonReader);
		//	}
				

		//		return players;
		//}
		//public static List<Player> GetTopTenPlayers(List<Player> players)
		//{
		//	var topTenPlayers = new List<Player>();
		//	players.Sort(new PlayerComparer());
		//	int counter = 0;
		//	foreach (var player in players)
		//	{
		//		topTenPlayers.Add(player);
		//		counter++;
		//		if (counter == 10)
		//			break;
		//	}
		//	return topTenPlayers;
		//}
		//public static List<Player> GetBottomTenPlayers(List<Player> players)
		//{
		//	var bottomTenPlayers = new List<Player>();
		//	players.Sort(new PlayComp());
		//	int counter = 0;
		//	foreach (var player in players)
		//	{
		//		bottomTenPlayers.Add(player);
		//		counter++;
		//		if (counter == 10)
		//			break;
		//	}
		//	return bottomTenPlayers;
		//}

		public static void SerializePlayerToFile(List<Player> players, string fileName)
		{

			var serializer = new JsonSerializer();
			using (var writer = new StreamWriter(fileName))
			using (var jsonWriter = new JsonTextWriter(writer))
			{
				serializer.Serialize(jsonWriter, players);
			}


		}
	}
}
