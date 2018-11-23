//AFSHIN MAHINI 2013-2014

using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour 
{
	
	public bool leftStart ;
	
	public int boardPosition;
	
	public Green1 pawn;
	private GameProcess gp ;
	public bool roundBend ;
	
	 
	void Start () 
	{
		this.renderer.enabled = false;
		boardPosition = 0;
		roundBend = false;
		leftStart = false;
		
		pawn = GameObject.Find("Green1").GetComponent<Green1>();
		gp = GameObject.Find("GameProcess").GetComponent<GameProcess>();
			
	}
	
	
	void Update () 
	{
		if ( gp.turn == 1 )
		{
			this.renderer.enabled = true ;
			
			//********* COMPLETE THE FOLLOWING CODE
			if ( gp.takingTurn && gp.playerPositions[0] == 0 &&  gp.roll != 1 && gp.roll != 2 )
			{
				gp.takingTurn = false;
				gp.printGui ( "Sorry, you cannot leave your base with a roll of " + gp.roll + "." ) ;
				
			}	
		}
		else this.renderer.enabled = false;
		
	
	}
}
