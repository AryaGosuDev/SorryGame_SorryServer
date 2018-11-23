//AFSHIN MAHINI 2013 - 2014

using System;
using UnityEngine;
using System.Collections;


public static class PawnHandle 
{
	private const int p1EndLoc = 3;
	private const int p2EndLoc = 18;
	private const int p3EndLoc = 33;
	private const int p4EndLoc = 48;
	
	private static int lastPosition ;
	
	private const double proxThresh = 5.0;
	public static Vector3[] positions = {   new Vector3 (0.0f, 0.0f, -10f), 
		
										    new Vector3 (93.2f, 93.5f, -10f),
		
											new Vector3 (93.2f, 81.0f, -10f),
											new Vector3 (93.2f, 69.0f, -10f),
											new Vector3 (93.2f, 56.0f, -10f),
											new Vector3 (93.2f, 44.0f, -10f),
											new Vector3 (93.2f, 32.0f, -10f),
											new Vector3 (93.2f, 19.0f, -10f),
											new Vector3 (93.2f, 6.0f, -10f),
											new Vector3 (93.2f, -6.0f, -10f),
											new Vector3 (93.2f, -19.0f, -10f),
											new Vector3 (93.2f, -31.0f, -10f),
											new Vector3 (93.2f, -44.0f, -10f),
											new Vector3 (93.2f, -56.0f, -10f),
											new Vector3 (93.2f, -68.0f, -10f),
											new Vector3 (93.2f, -81.0f, -10f),
		
											new Vector3 (93.2f, -93.5f, -10f),
		
											new Vector3 (80.0f, -93.5f, -10f),
											new Vector3 (68.0f, -93.5f, -10f),
											new Vector3 (56.0f, -93.5f, -10f),
											new Vector3 (44.0f, -93.5f, -10f),
											new Vector3 (32.0f, -93.5f, -10f),
											new Vector3 (19.2f, -93.5f, -10f),
											new Vector3 (6.0f, -93.5f, -10f),
											new Vector3 (-6.0f, -93.5f, -10f),
											new Vector3 (-18.5f, -93.5f, -10f),
											new Vector3 (-31.0f, -93.5f, -10f),
											new Vector3 (-43.5f, -93.5f, -10f),
											new Vector3 (-56.0f, -93.5f, -10f),
											new Vector3 (-68.0f, -93.5f, -10f),
											new Vector3 (-80.3f, -93.5f, -10f),
		
											new Vector3 (-93.0f, -93.5f, -10f),
		
											new Vector3 (-93.0f, -81.0f, -10f),
											new Vector3 (-93.0f, -68.0f, -10f),
											new Vector3 (-93.0f, -56.0f, -10f),
											new Vector3 (-93.0f, -44.0f, -10f),
											new Vector3 (-93.0f, -31.0f, -10f),
											new Vector3 (-93.0f, -19.0f, -10f),
											new Vector3 (-93.0f, -6.0f, -10f),
											new Vector3 (-93.0f, 6.0f, -10f),
											new Vector3 (-93.0f, 19.0f, -10f),
											new Vector3 (-93.0f, 32.0f, -10f),
											new Vector3 (-93.0f, 44.0f, -10f),
											new Vector3 (-93.0f, 56.0f, -10f),
											new Vector3 (-93.0f, 69.0f, -10f),
											new Vector3 (-93.0f, 81.0f, -10f),
		
											new Vector3 (-93.0f, 93.5f, -10f),
		
											new Vector3 (-80.3f, 93.5f, -10f),
											new Vector3 (-68.0f, 93.5f, -10f),
											new Vector3 (-56.0f, 93.5f, -10f),
											new Vector3 (-43.5f, 93.5f, -10f),
											new Vector3 (-31.0f, 93.5f, -10f),
											new Vector3 (-18.5f, 93.5f, -10f),
											new Vector3 (-6.0f, 93.5f, -10f),
											new Vector3 (6.0f, 93.5f, -10f),
											new Vector3 (19.2f, 93.5f, -10f),
											new Vector3 (32.0f, 93.5f, -10f),
											new Vector3 (44.0f, 93.5f, -10f),
											new Vector3 (56.0f, 93.5f, -10f),
											new Vector3 (68.0f, 93.5f, -10f),
											new Vector3 (80f, 93.5f, -10f), 
		
										    new Vector3 (93.2f, 93.5f, -10f), // end of board values
		
											new Vector3 (93.2f, 81.0f, -10f), // extra positions for padding
											new Vector3 (93.2f, 69.0f, -10f),
											new Vector3 (93.2f, 56.0f, -10f),
											new Vector3 (93.2f, 44.0f, -10f),
											new Vector3 (93.2f, 32.0f, -10f),
											new Vector3 (93.2f, 19.0f, -10f),
											new Vector3 (93.2f, 6.0f, -10f),
											new Vector3 (93.2f, -6.0f, -10f),
											new Vector3 (93.2f, -19.0f, -10f),
											new Vector3 (93.2f, -31.0f, -10f),
											new Vector3 (93.2f, -44.0f, -10f),
											new Vector3 (93.2f, -56.0f, -10f),
											new Vector3 (93.2f, -68.0f, -10f),
	};
	
	public static Vector3[] startPositions = { new Vector3 ( 73.0f, 43.0f, -10.0f),
											   new Vector3 ( 44.0f, -75.0f, -10.0f),
											   new Vector3 ( -73.0f, -44.0f, -10.0f),
											   new Vector3 ( -43.0f, 74.0f, -10.0f)
	};
	
	//GAME FUNCTION !
	public static bool findMove ( Vector3 pawnParam, ref GameProcess gp ) 
	{
		switch ( gp.turn ) 
		{
			case 1: //WINNING CONDITIONS
				if ( gp.playerPositions[0] == 0 )
				{
					if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[5].x, positions[5].y )) < proxThresh)
					{
						gp.p1.leftStart = true;
						gp.playerPositions[0] = 5;
						gp.pawnObjects[0].transform.position = new Vector3 ( positions[5].x,  positions[5].y , positions[5].z );
						return true;
					}
				}
				else if ( (gp.playerPositions[0] + gp.forwardMove == 64 || gp.playerPositions[0] + gp.forwardMove == (p1EndLoc+1)) && (gp.playerPositions[0] > 52 || ( gp.playerPositions[0] >= 1 && gp.playerPositions[0] <= p1EndLoc)) && gp.p1.leftStart  ) 
				{
					gp.winningMove = gp.turn;
					return true;
				}
				else if ( (gp.playerPositions[0] + gp.forwardMove > 63 || ((gp.playerPositions[0] <= p1EndLoc ) ? gp.playerPositions[0] + gp.forwardMove > p1EndLoc : false )) && (gp.playerPositions[0] > 52 || ( gp.playerPositions[0] >= 1 && gp.playerPositions[0] <= p1EndLoc)) && gp.p1.leftStart && !didMoveBack(pawnParam, ref gp) ) 
				{
					return true;
				}
				
			
				break;
			case 2://WINNING CONDITIONS
				if ( gp.playerPositions[1] == 0 )
				{
					if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[20].x, positions[20].y )) < proxThresh)
					{
						gp.p2.leftStart = true;
						gp.playerPositions[1] = 20;
						gp.pawnObjects[1].transform.position = new Vector3 ( positions[20].x,  positions[20].y , positions[20].z );
						return true;
					}
				}
				else if ( gp.playerPositions[1] + gp.forwardMove == (p2EndLoc+1) && ( gp.playerPositions[1] >= 7 && gp.playerPositions[1] <= p2EndLoc) && gp.p2.leftStart  ) 
				{
					gp.winningMove = gp.turn;
					return true;
				}
				else if ( gp.playerPositions[1] + gp.forwardMove > p2EndLoc && ( gp.playerPositions[1] >= 7 && gp.playerPositions[1] <= p2EndLoc) && gp.p2.leftStart && !didMoveBack(pawnParam, ref gp)  ) 
				{
					return true;
				}
				
				
				break;
			case 3://WINNING CONDITIONS
				if ( gp.playerPositions[2] == 0 )
					{
						if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[35].x, positions[35].y )) < proxThresh)
						{
							gp.p3.leftStart = true;
							gp.playerPositions[2] = 35;
							gp.pawnObjects[2].transform.position = new Vector3 ( positions[35].x,  positions[35].y , positions[35].z );
							return true;
						}
					}
					else if ( gp.playerPositions[2] + gp.forwardMove == (p3EndLoc+1) && ( gp.playerPositions[2] >= 22 && gp.playerPositions[2] <= p3EndLoc) && gp.p3.leftStart  ) 
					{
						gp.winningMove = gp.turn;
						return true;
					}
					else if ( gp.playerPositions[2] + gp.forwardMove > p3EndLoc && ( gp.playerPositions[2] >= 22 && gp.playerPositions[2] <= p3EndLoc) && gp.p3.leftStart && !didMoveBack(pawnParam, ref gp) ) 
					{
						return true;
					}
					
					
					break;
			case 4://WINNING CONDITIONS
				if ( gp.playerPositions[3] == 0 )
					{
						if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[50].x, positions[50].y )) < proxThresh)
						{
							gp.p4.leftStart = true;
							gp.playerPositions[3] = 50;
							gp.pawnObjects[3].transform.position = new Vector3 ( positions[50].x,  positions[50].y , positions[50].z );
							return true;
						}
					}
					else if ( gp.playerPositions[3] + gp.forwardMove == (p4EndLoc+1) && ( gp.playerPositions[3] >= 37 && gp.playerPositions[3] <= p2EndLoc) && gp.p4.leftStart  ) 
					{
						gp.winningMove = gp.turn;
						return true;
					}
					else if ( gp.playerPositions[3] + gp.forwardMove > p4EndLoc && ( gp.playerPositions[3] >= 37 && gp.playerPositions[3] <= p4EndLoc) && gp.p4.leftStart && !didMoveBack(pawnParam, ref gp) ) 
					{
						return true;
					}
					
					
					break;
			default :
				break;
		}
		
		switch ( gp.forwardMove )
		{
			case 10: //ROLL OF 10 MOVED FORWARD
				if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].x, positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].y )) < proxThresh)
				{
					gp.playerPositions[gp.turn-1] += gp.forwardMove;
					gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
					
					return true;
				}
				else //ROLL OF 10 MOVED BACK
				{
					int movedBack = gp.backMove + gp.playerPositions[gp.turn-1];
					if ( movedBack  <= 0 ) movedBack += 60;
					
					if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[movedBack].x, positions[movedBack].y )) < proxThresh)
					{
						gp.playerPositions[gp.turn-1] += gp.backMove;
						if ( gp.playerPositions[gp.turn-1] <= 0 ) gp.playerPositions[gp.turn-1] += 60;
						gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
						
						return true;
					}
				}

				break;
			
			case 11: //ROLL OF 11 MOVED OR BUMBED
				if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].x, positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].y )) < proxThresh)
				{
					gp.playerPositions[gp.turn-1] += gp.forwardMove;
					gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
					
					return true;
				}
				for ( int i = 0 ; i < gp.playerPositions.Length ; ++i )
				{
					if ( (i+1) != gp.turn )
					{
						if  ( gp.playerPositions[i] != 0 &&  eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[ gp.playerPositions[i]].x, positions[gp.playerPositions[i]].y )) < proxThresh) 
						{
							gp.playerPositions[gp.turn-1] = gp.playerPositions[i];
							gp.playerPositions[i] = 0;
							gp.pawnObjects[i].transform.position = new Vector3 ( 0.0f, 0.0f, 0.0f) ;
							gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
							return true;
						}
					}
				}
				
				break;
			
			case -200: //ROLL OF SORRY 
			
				for ( int i = 0 ; i < gp.playerPositions.Length ; ++i )
				{
					if ( (i+1) != gp.turn )
					{
						if  ( gp.playerPositions[i] != 0 &&  eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[ gp.playerPositions[i]].x, positions[gp.playerPositions[i]].y )) < proxThresh) 
						{
							gp.playerPositions[i] = 0;
							gp.pawnObjects[i].transform.position = new Vector3 ( 0.0f, 0.0f, 0.0f) ;
							
							return true;
						}
					}
				}
				return true;
				break;
			
			default : //ALL OTHER ROLLS
				if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].x, positions[gp.forwardMove + gp.playerPositions[gp.turn-1]].y )) < proxThresh && (gp.forwardMove + gp.playerPositions[gp.turn-1]) > 0 )
				{
					gp.playerPositions[gp.turn-1] += gp.forwardMove;
					if ( gp.playerPositions[gp.turn-1] <= 0 ) gp.playerPositions[gp.turn-1] += 60;
					gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
					
					return true;
				}
				else
				{
					int movedBack = gp.backMove + gp.playerPositions[gp.turn-1];
					if ( movedBack  <= 0 ) movedBack += 60;
					
					if ( eucD ( new Vector2 ( pawnParam.x,   pawnParam.y) , new Vector2 ( positions[movedBack].x, positions[movedBack].y )) < proxThresh)
					{
						gp.playerPositions[gp.turn-1] += gp.backMove;
						if ( gp.playerPositions[gp.turn-1] <= 0 ) gp.playerPositions[gp.turn-1] += 60;
						gp.pawnObjects[gp.turn-1].transform.position = new Vector3 ( positions[ gp.playerPositions[gp.turn-1]].x,  positions[ gp.playerPositions[gp.turn-1]].y , positions[ gp.playerPositions[gp.turn-1]].z );
						
						return true;
					}
				}
			

				break;
		}
		return false;
	}
	
	
	//Handles collisions of pawns on the board
	public static void doesCollide (  ref GameProcess gp )
	{
		for ( int i = 0 ; i < gp.playerPositions.Length ; ++ i )
		{
			if ( (i+1) != gp.turn )
			{
				if ( gp.playerPositions[i] != 0 && gp.playerPositions[i] == gp.playerPositions[gp.turn-1] )
				{
					gp.playerPositions[i] = 0;
					return;
				}
			}
		}
		
		
	}
	
	//Did the pawn move a space back?
	public static bool didMoveBack ( Vector3 pawnParam, ref GameProcess gp )
	{	
		int movedBack = gp.backMove + gp.playerPositions[gp.turn-1];
		if ( movedBack  <= 0 ) movedBack += 60;
		if ( (gp.roll == 8 || gp.roll == 4) && eucD ( new Vector2 ( pawnParam.x, pawnParam.y ) , new Vector2 ( positions[movedBack].x,positions[movedBack].y)) < proxThresh )
		{
			return true;
		}
		return false;
		
	}
			
	//Basic Euclidean Distance
	public static double eucD ( Vector2 v1, Vector2 v2 )
	{
		return Math.Sqrt ( Math.Pow(v1.x-v2.x,2.0) + Math.Pow(v1.y-v2.y,2.0));			
	}
}