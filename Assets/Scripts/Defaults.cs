using UnityEngine;

public class Defaults : MonoBehaviour {
	public static volatile Defaults instance;
	[RuntimeInitializeOnLoadMethod]
	private static void CreateInstance(){
		print("Load Defaults");
		instance=Resources.Load("Defaults", typeof(Defaults)) as Defaults;
	}
	
	
}
