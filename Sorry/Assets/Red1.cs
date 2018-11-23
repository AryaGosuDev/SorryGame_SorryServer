//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class Red1 : MonoBehaviour {

	private Vector3 lastPosition ;
	private GameProcess gp;
	
	
	void Start () 
	{
		gp = GameObject.Find("GameProcess").GetComponent<GameProcess>();
		lastPosition = gameObject.transform.position ;
	}
	
	void Update () 
	{
		
		
	}
	
	void OnMouseDrag()
	{
		if ( gp.turn == 2  )
		{
				if ( gp.takingTurn == true )
				{
					Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					point.z = 500;
			        gameObject.transform.position = point;
				}
				else gp.printGui ( "Please roll first.\n");        
		}
		      
	}
	
	void OnMouseUp()
	{
		
		if (  gp.turn == 2 && gp.takingTurn == true ) 	
		{
			if ( !PawnHandle.findMove ( this.transform.position, ref  gp   ) ) 
			{
				this.transform.position = lastPosition ;
			}
		
			else//********* COMPLETE THE FOLLOWING CODE
			{
				if ( gp.winningMove != 0 )
				{
					
				}
				else
				{
					
					
					if ( gp.playerPositions[1] > 60 ) gp.playerPositions[1] -= 60;
					
					PawnHandle.doesCollide ( ref gp ) ;
					
					lastPosition = this.transform.position ;
					
				}
			
			
			}
		}

	}
}
