//AFSHIN MAHINI 2013 - 2014

using UnityEngine;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Diagnostics;
using System.Threading;

public static class NTPTime 
{

	public static DateTime getNTPTime( DateTime dt, ref Stopwatch uniClock  )
	{
		try
		{
            return dt;
            
		}
		catch ( Exception ex )
		{	
			Console.WriteLine ( ex.Message + " : getNTPTime " ) ; 
		}
		
		return dt;
	}
}
