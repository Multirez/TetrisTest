using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Engine : MonoBehaviour {
	public static Engine instance;
	
	struct GameStats{
		public int lines;
		public float score;
		
		public void Reset(){
			lines=0;
			score=0f;
		}
	}
	
	[Tooltip("Move down speed.")]
	public float speed;
	[Tooltip("Modifier accelerate gameplay by pressing the \"Down\".")]
	public float forcedFactor;
	[Tooltip("Score if remove one line.")]
	public float oneLineScore;
	[Tooltip("If remove more then one line at ones will be combo." +
		"\n Score = one * count * (base ^ (count - 1))" +
		"\n where:" +
		"\n - one - oneLineScore," +
		"\n - count - lines removed at ones," +
		"\n - base - comboScoreBase")]
	public float comboScoreBase;
	[Tooltip("Avaliable chip types.")]
	public List<Chip> chipList;
	[Tooltip("Position for the show next chip type.")]
	public Transform nextChipPos;
	[Tooltip("Start position for new chip.")]
	public Transform startChipPos;
	[Tooltip("Link to scene Grid object.")]
	public Grid grid;
	[Tooltip("Link to UI Text with game stats info.")]
	public Text statsText;
			
	private bool isPause;
	private Chip activeChip;
	private Chip nextChip;
	private float moveSpeed;//move speed equal speed or forcedSpeed
	private float lastMoveTimePoint;
	private GameStats gameStats;
		
	private void Awake(){
		instance=this;
	}	
	private void Start(){
		moveSpeed=speed;
		StartGame();
	}
	
	public void RestartGame(){
		grid.Clear();
		if(activeChip)
			Destroy(activeChip);
		StartGame();
	}
	public void PauseGame(){
		isPause=!isPause;
		lastMoveTimePoint=Time.time;
	}
	private void StartGame(){
		isPause=false;
		gameStats.Reset();
		OnStatsUpdate();
		lastMoveTimePoint=Time.time;
		CreateNextChip(chipList[Random.Range(0, chipList.Count)]);
	}
	private void CreateNextChip(Chip etalon){
		if(nextChip!=null)
			Destroy(nextChip.gameObject);
		Debug.Log("Create next chip");
		nextChip=Instantiate(etalon,
			nextChipPos.position, nextChipPos.rotation) as Chip;		
	}
	/// <summary>
	/// Moves the nextChip to start position and creates new nextChip.
	/// </summary>
	private void UseNextChip(){
		activeChip=nextChip;
		activeChip.transform.position=startChipPos.position;
		activeChip.transform.rotation=startChipPos.rotation;
		activeChip.transform.SetParent(grid.transform);
		
		nextChip=null;
		CreateNextChip(chipList[Random.Range(0, chipList.Count)]);
	}
	private void UpdateChipPos(){
		float timeStep=Time.time-lastMoveTimePoint;
		float oneMoveTime=1f/moveSpeed;
		while(timeStep > oneMoveTime){
			timeStep-=oneMoveTime;
			//calc move
			if(activeChip==null){
				UseNextChip();				
			}else if(!MoveChip(activeChip, Vector3.down)){
				grid.Add(activeChip);
				activeChip=null;
			}
		}
		lastMoveTimePoint=Time.time-timeStep;
	}
	private bool MoveChip(Chip chip, Vector3 direction){
		if(grid.IsCanMove(chip, direction)){
			chip.transform.localPosition+=direction;
			return true;
		}
		return false;
	}
	private bool RotateChip(Chip chip, Vector3 eulerAngles){
		if(grid.IsCanRotate(chip, eulerAngles)){
			chip.transform.Rotate(eulerAngles, Space.Self);
			return true;
		}
		return false;
	}
	
	private void OnGUI(){
		GUILayout.Label("Use arrow keys for movement, Up - rotate." +
			" WASD - menu navigation, Space - confirm.");
	}
	
	public void AddScore(int removedLinesCount){
		gameStats.lines+=removedLinesCount;
		gameStats.score+=removedLinesCount*oneLineScore*
			Mathf.Pow(comboScoreBase, removedLinesCount-1);
		OnStatsUpdate();
	}
	public void OnStatsUpdate(){
		//need to update InfoText
		if(statsText)
			statsText.text=
				"Level - 0"+
				"\nScore - "+gameStats.score.ToString("0")+
				"\nLines - "+gameStats.lines;
	}
	
	private void Update(){
		if(isPause)
			return;
		CheckInput();
		UpdateChipPos();
	}
	private void CheckInput(){
		if(Input.anyKeyDown){
			if(Input.GetButtonDown("Left")){
				Debug.Log("Left");
				if(activeChip)
					MoveChip(activeChip, Vector3.left);
			}
			if(Input.GetButtonDown("Right")){
				Debug.Log("Right");
				if(activeChip)
					MoveChip(activeChip, Vector3.right);
			}
			if(Input.GetButtonDown("Down")){
				Debug.Log("Down");
				moveSpeed=speed*forcedFactor;
			}
			if(Input.GetButtonDown("Rotate")){
				Debug.Log("Rotate");
				if(activeChip)
					RotateChip(activeChip, new Vector3(0f, 0f, 270f));
			}			
		}
		if(Input.GetButtonUp("Down")){
			moveSpeed=speed;
		}
	}
}
