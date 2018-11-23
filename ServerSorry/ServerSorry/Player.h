#include <map>
#include <queue>
#include "pthread.h"
#include <winsock2.h>
#include <ws2tcpip.h>

class Player
{
	public :

		struct playerStruct
		{
		
				int client ;
				std::map<unsigned int, SOCKET> * playerInfo ;
				static bool gameStarted ;
				static std::queue<unsigned char> * recvQueue ;
				static int turn;
				static bool disconnect ;
		};

	private:

};