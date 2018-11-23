#include "StdAfx.h"
#include "StreamHandler.h"

int StreamHandler::sendPacket ( SOCKET curSocket, char * message , int messageSize )
{
	return send(curSocket, message, messageSize, 0);
}

int StreamHandler::recvPacket ( SOCKET curSocket, char * buffer , int bufferSize )
{
	return recv(curSocket, buffer, bufferSize, 0);
}

int StreamHandler::sendPacketToAll ( std::map<unsigned int, SOCKET> * sessions, char * message , int messageSize )
{
	try
	{
		std::map<unsigned int, SOCKET>::iterator iter = sessions->begin();
		int returnVal ( 0 );
		while ( iter != sessions->end() )
		{
			returnVal += send ( (*iter).second , message, messageSize, 0 );
			++iter ;
		}
	
		return returnVal ;
		//return send(sessions, message, messageSize, 0);
	}
	catch (std::exception const & ex)
	{
		std::cout << ex.what() << " : thread\n" ; 

	}
}