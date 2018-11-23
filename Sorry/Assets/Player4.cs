//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class Player4 : MonoBehaviour {
	
	
	
	public Yellow1 pawn;
	public bool leftStart ;
	public int boardPosition;
	public bool roundBend ;
	private GameProcess gp ;
	
	
	void Start () 
	{
		gp = GameObject.Find("GameProcess").GetComponent<GameProcess>();
		
		this.renderer.enabled = false;
		
		roundBend = false;
		leftStart = false;
		
		pawn = GameObject.Find("Yellow1").GetComponent<Yellow1>();
		boardPosition = 0;
		
		
	}
	
	void Update () {
		
		
		if ( gp.turn == 4 )
		{
			this.renderer.enabled = true ;
			
			//********* COMPLETE THE FOLLOWING CODE
			if ( gp.takingTurn && gp.playerPositions[3] == 0 && gp.roll != 1 && gp.roll != 2 )
			{
				gp.takingTurn = false;
				gp.printGui ( "Sorry, you cannot leave your base with a roll of " + gp.roll + "." ) ;
				
			}
		}
		else this.renderer.enabled = false;
		
	}
	
}
