using UnityEngine;
using System.Collections.Generic;

public class Engine : MonoBehaviour {
	public static Engine instance;
	
	[Tooltip("Move down speed.")]
	public float speed;
	[Tooltip("Modifier accelerate gameplay by pressing the \"Down\".")]
	public float forcedFactor;
	[Tooltip("Avaliable chip types.")]
	public List<Chip> chipList;
	[Tooltip("Position for the show next chip type.")]
	public Transform nextChipPos;
	[Tooltip("Start position for new chip.")]
	public Transform startChipPos;
	
	public Grid grid;
		
	private Chip activeChip;
	private Chip nextChip;
	private float moveSpeed;//move speed equal speed or forcedSpeed
	private float lastMoveTimePoint;
		
	private void Awake(){
		instance=this;
	}	
	private void Start () {
		StartGame();
		moveSpeed=speed;
	}
	
	private void Restart(){
		grid.Clear();
		StartGame();
	}	
	private void StartGame(){
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
			}else if(grid.IsCanMove(activeChip)){
				activeChip.transform.Translate(Vector3.down, Space.Self);
			}else{
				grid.Add(activeChip);
				grid.RemoveFilledLines();
				activeChip=null;
			}
		}
		lastMoveTimePoint=Time.time-timeStep;
	}
	
	private void Update () {
		CheckInput();
		UpdateChipPos();
	}
	private void CheckInput(){
		if(Input.anyKeyDown){
			if(Input.GetButtonDown("Left")){
				Debug.Log("Left");
			}
			if(Input.GetButtonDown("Right")){
				Debug.Log("Right");
			}
			if(Input.GetButtonDown("Down")){
				Debug.Log("Down");
				moveSpeed=speed*forcedFactor;
			}
			if(Input.GetButtonDown("Rotate")){
				Debug.Log("Rotate");
			}			
		}
		if(Input.GetButtonUp("Down")){
			moveSpeed=speed;
		}
	}
}
