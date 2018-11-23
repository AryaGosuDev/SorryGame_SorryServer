//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
	
	
	public bool leftStart ;
	public Red1 pawn ;
	public int boardPosition;
	private GameProcess gp ;
	public bool roundBend ;
	
	void Start () {
		this.renderer.enabled = false;
		
		
		pawn = GameObject.Find("Red1").GetComponent<Red1>();
		boardPosition = 0;
		roundBend = false;
		gp = GameObject.Find("GameProcess").GetComponent<GameProcess>();
		leftStart = false;
		
	
	}
	
	
	void Update () {
		
		
		if ( gp.turn == 2 )
		{
			this.renderer.enabled = true ;
			
			//********* COMPLETE THE FOLLOWING CODE
			if ( gp.takingTurn && gp.playerPositions[1] == 0 && gp.roll != 1 && gp.roll != 2 )
			{
				gp.takingTurn = false;
				gp.printGui ( "Sorry, you cannot leave your base with a roll of " + gp.roll + "." ) ;
				
				
				
			}
		}
		else this.renderer.enabled = false;
		
	
	}
	
	
}
