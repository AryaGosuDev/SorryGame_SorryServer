//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System;
using System.Collections;
using System.Net.Sockets;
using System.Net;

public class ThreadSock : MonoBehaviour 
{
	private NetworkStream nws;
	private byte[] streamBuffer;
	private byte byteBuffer;
	private byte tempBuffer;
	private Sockets socks;
	
	//********* COMPLETE THE FOLLOWING CODE
	public ThreadSock (NetworkStream nwsIn, Sockets inSocket )
	{
		
	}
	//********* COMPLETE THE FOLLOWING CODE
	//********* READ THE STREAM
	public void Service ()
	{	
		try
		{
			
		}
		catch ( Exception ex )
		{
			print ( ex.Message + " : Thread loop" );
			
		}
		
	}
	
}
