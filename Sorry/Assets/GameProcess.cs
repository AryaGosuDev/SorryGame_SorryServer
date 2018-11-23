//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameProcess : MonoBehaviour {
	
	//PUBLIC MEMBERS
	public int clientNumber;
	public int turn ;
	public bool takingTurn;
	public int roll;
	public bool startGame;
	public int backMove ;
	public int forwardMove ;
	public int winningMove ;
	public readonly byte[] commands = {0x00, //connect
								  0x40, //turn
								  0x80, //roll
								  0xC0};//move
	
	public enum codes  {init, turn, roll, move }; // 00   01    10     11
	
	public Player1 p1;
	public Player2 p2;
	public Player3 p3;
	public Player4 p4;
	
	public GameObject[] pawnObjects; 
	
	public int[] playerPositions;
	
	//PRIVATE MEMBERS
	private Sockets socks;
	private byte byteBuffer;
	private byte tempBuffer;
	private Gui gui;
	private int moveCommands ;
	
	private readonly string[] moveOptions = { "You can move a pawn from the start or move forward 1 space.",
									          "You can move a pawn from the start or move forward 2 spaces.",
											  "Move forward 3 spaces.",
											  "Move backward 2 spaces.",
											  "Move forward 5 spaces.",
											  "Move forward 7 spaces.",
											  "Move forward 8 spaces.",
									          "Move forward 10 spaces or move backward 1 space.",
											  "Move forward 11 spaces or bump switch places with an opponent. If it is impossible to move forward 11 spaces, and there are no opponent pawns on the board, then you will have to forfeit your turn.",
											  "Move forward 12 spaces.",
											  "Sorry! : You can use this roll to bump an opponent's pawn to the start."};
	
	void Start () 
	{
		turn = -1;
		socks = new Sockets();
		takingTurn = false;
		moveCommands = 1;
		
		
		pawnObjects = new GameObject[4];
		playerPositions = new int[4]; 
		
		gui = GameObject.Find("Gui").GetComponent<Gui>();
		
		p1 = GameObject.Find("Player1").GetComponent<Player1>();
		p2 = GameObject.Find("Player2").GetComponent<Player2>();
		p3 = GameObject.Find("Player3").GetComponent<Player3>();
		p4 = GameObject.Find("Player4").GetComponent<Player4>();
		
		winningMove = 0;
		
		pawnObjects[0] = GameObject.Find("Green1").GetComponent<Green1>().gameObject;
		pawnObjects[1] = GameObject.Find("Red1").GetComponent<Red1>().gameObject;
		pawnObjects[2] = GameObject.Find("Blue1").GetComponent<Blue1>().gameObject;
		pawnObjects[3] = GameObject.Find("Yellow1").GetComponent<Yellow1>().gameObject;
	
	}
	
	void Update () 
	{
		//********* COMPLETE THE FOLLOWING CODE
		//********* THIS IS MAIN LOOP WHERE GAME INFORMATION IS PROCESSED FROM THE QUEUE
		//********* READ THE QUEUE AND PROCESS THE GAME
		if ( "items on queue" )
		{
			"process"
				
				switch ( code )
			{
			}
		}
	}
	
	public Sockets returnSocket ()
	{
		return socks;
	}
	
	
	
	public void nextTurn (  )
	{
		//********* COMPLETE THE FOLLOWING CODE
		takingTurn = false;
		
		
		
	}
	
	public void sendPositions ()
	{
		//********* COMPLETE THE FOLLOWING CODE
		
	
		
		
	}
	
	public void sendEndGame ()
	{
		//********* COMPLETE THE FOLLOWING CODE
		
		
	}
	
	public void printGui( string printStr )
	{
		this.gui.printGui(printStr );
	}
	
	
}
