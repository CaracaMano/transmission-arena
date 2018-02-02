using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManagerSingleton {

	public class PlayerSettings {
		public string controllerType;
		public Color playerColor;
	}

	private static PlayersManagerSingleton _instance;
	public static PlayersManagerSingleton Instance {
		get {
			if (_instance == null) {
				_instance = new PlayersManagerSingleton();
			}
			return _instance;
		}
	}
	
	public Dictionary<string, PlayerSettings> players;

	private PlayersManagerSingleton() {
		players = new Dictionary<string, PlayerSettings>();
	}

	public void Reset() {
		players = new Dictionary<string, PlayerSettings>();
	}

}
