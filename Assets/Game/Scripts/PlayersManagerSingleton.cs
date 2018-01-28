using System.Collections;
using System.Collections.Generic;

public class PlayersManagerSingleton {


	private static PlayersManagerSingleton _instance;
	public static PlayersManagerSingleton Instance {
		get {
			if (_instance == null) {
				_instance = new PlayersManagerSingleton();
			}
			return _instance;
		}
	}
	
	public Dictionary<string, string> players;

	private PlayersManagerSingleton() {
		players = new Dictionary<string, string>();
	}

	public void Reset() {
		players = new Dictionary<string, string>();
	}

}
