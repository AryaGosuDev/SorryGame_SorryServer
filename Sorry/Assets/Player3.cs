//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class Player3 : MonoBehaviour   
{
	
	public bool leftStart ;
	public Blue1 pawn;
	public int boardPosition;
	public bool roundBend ;
	private GameProcess gp ;
	
	
	void Start () 
	{
		
		leftStart = false;
		this.renderer.enabled = false;
		
		
		roundBend = false;
		
		pawn = GameObject.Find("Blue1").GetComponent<Blue1>();
		gp = GameObject.Find("GameProcess").GetComponent<GameProcess>();
		boardPosition = 0;
	}
	
	void Update () {
		
		
		if ( gp.turn == 3 )
		{
			this.renderer.enabled = true ;
			
			//********* COMPLETE THE FOLLOWING CODE
			if ( gp.takingTurn && gp.playerPositions[2] == 0 && gp.roll != 1 && gp.roll != 2 )
			{
				gp.takingTurn = false;
				gp.printGui ( "Sorry, you cannot leave your base with a roll of " + gp.roll + "." ) ;
				
				
				
			}
		}
		else this.renderer.enabled = false;
		
	
	}
}
