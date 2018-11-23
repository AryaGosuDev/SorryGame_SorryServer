//#define _WIN32_WINNT 0x501
#include <map>
#include <queue>
#include "pthread.h"
#include <winsock2.h>
#include <ws2tcpip.h>

class StreamHandler 
{
	public:
		static int StreamHandler::sendPacket ( SOCKET curSocket, char * message , int messageSize );
		static int StreamHandler::recvPacket ( SOCKET curSocket, char * buffer , int bufferSize );
		static int StreamHandler::sendPacketToAll ( std::map<unsigned int, SOCKET> * sessions, char * message , int messageSize );
		
	private:

};