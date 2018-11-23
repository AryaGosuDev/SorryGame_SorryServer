//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {
	
	
	
	bool show = false;
	public double delTime;
	public GUIText guiText;
	private GameProcess process;
	
	
	
	void Start () 
	{
		process = GameObject.Find("GameProcess").GetComponent<GameProcess>();
	}
	
	void OnGUI () {
		
		if ( !show )
		{
			if (GUI.Button (new Rect (500,200,100,20), "Connect")) 
			{
				guiText.text = "Connecting...";
				if ( process.returnSocket().Connect() )
				{	
					
					show = !show;
					guiText.text = "Connect Succeeded";
					
					
				}
				else guiText.text = "Connect Failed";
			}
		}
		
		if ( show ) 
		{
			if ( GUI.Button( new Rect( 500, 200, 100, 20), "Disconnect"))
			{
				//********* COMPLETE THE FOLLOWING CODE
				//********* KILL THREAD AND SEVER CONNECTION
				
				
				show = !show;
			}
		}
		
		
		if ( !show )
		{
			if ( GUI.Button( new Rect( 500, 230, 100, 20), "Roll" ))
			{
				guiText.text = "Not Connected";
				
			}
			
		}
		
		if ( show )
		{
			if ( GUI.Button( new Rect( 500, 230, 100, 20), "Roll"))
			{
				if ( process.turn == process.clientNumber && process.startGame == true  )
				{
					//********* COMPLETE THE FOLLOWING CODE
					
					else printGui ( "Sorry, you have already rolled a " + process.roll );
				}
				else
				{
					printGui ( "Sorry, it is not your turn. " + "It is Player " + process.turn + "'s turn.");
					
				}
			}
		}
		
		

		//if ( GUI.Button ( new Rect ( 500, 300, 100, 20 ), "Latency" ))
		//{
			//process.returnSocket().measureLatency() ;
		//}
		
		//GUI.Label ( new Rect ( 500, 330, 100, 20 ) , "Latency : " + process.returnSocket().returnLatency()  );
		
	}
	
	
	void Update () 
	{
	}
	
	public void printGui ( string printStr )
	{
		int wordCount = 0 ;
		string[] words = printStr.Split(' ');
		
		printStr = "";
		
		for ( int i = 0 ; i < words.Length ; ++ i )
		{
			if ( wordCount <= 4 )
			{
				printStr += words[i] + " " ;
				wordCount ++ ;
			}
			else
			{
				printStr += words[i] + "\n" ;
				wordCount = 0;
				
			}	
		}
		
		guiText.text = printStr ;
	}
	
	
	
}
