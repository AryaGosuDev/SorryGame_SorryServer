#include "StdAfx.h"
#include "StreamHandler.h"
//#include "ServerProcess.h"
#include "NetworkTCP.h"
//#include "Player.h"

unsigned int NetworkTCP::clientID;

int maxPlayers = 2;

pthread_t  * NetworkTCP::readThreads[2];
//Player::playerStruct * arrsOfPlayers[4];

bool Player::playerStruct::gameStarted ;
bool Player::playerStruct::disconnect;
std::queue<unsigned char> * Player::playerStruct::recvQueue;
int Player::playerStruct::turn ;

extern char * inPort;


NetworkTCP::NetworkTCP(void)
{
	//arrsOfPlayers = new ServerProcess::player[2];
	clientID = 1;
	
	
    WSADATA wsaData;

    listenSocket = INVALID_SOCKET;

	struct addrinfo *result = NULL;
    struct addrinfo hints;

	iResult = WSAStartup(MAKEWORD(2,2), &wsaData);

    if (iResult != 0) 
	{
        std::cout << "WSAStartup failed with error: " << iResult << "\n" ;
        exit(1);
    }

	ZeroMemory(&hints, sizeof(hints));
    hints.ai_family = AF_INET;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;    // TCP connection!!!
    hints.ai_flags = AI_PASSIVE;

	
    //iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);
	iResult = getaddrinfo(NULL, inPort, &hints, &result);

    if ( iResult != 0 ) 
	{
		std::cout << "getaddrinfo failed with error: " << iResult << "\n" ;
        WSACleanup();
        exit(1);
    }

	// Create a SOCKET for connecting to server
    listenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);

    if (listenSocket == INVALID_SOCKET) {
		std::cout << "socket failed with error: " << WSAGetLastError() << "\n" ;
        
        freeaddrinfo(result);
        WSACleanup();
        exit(1);
    }
	/*
	u_long iMode = 1;
    iResult = ioctlsocket(listenSocket, FIONBIO, &iMode);
    
    if (iResult == SOCKET_ERROR) {
	    printf("ioctlsocket failed with error: %d\n", WSAGetLastError());
	    closesocket(listenSocket);
	    WSACleanup();
	    exit(1);
	}
	*/
	// Setup the TCP listening socket
    iResult = bind( listenSocket, result->ai_addr, (int)result->ai_addrlen);

    if (iResult == SOCKET_ERROR) 
	{
		std::cout << "bind failed with error: " << WSAGetLastError() << "\n" ;
        
        freeaddrinfo(result);
        closesocket(listenSocket);
        WSACleanup();
        exit(1);
    }

	// no longer need address information
    freeaddrinfo(result);

	// start listening for new clients attempting to connect
    iResult = listen(listenSocket, SOMAXCONN);

    if (iResult == SOCKET_ERROR) 
	{
		std::cout << "listen failed with error: " << WSAGetLastError() << "\n" ;
       
        closesocket(listenSocket);
        WSACleanup();
        exit(1);
	}

	service ();
	
}

NetworkTCP::~NetworkTCP(void )
{
}

void NetworkTCP::service (void)
{
	// get new clients
	std::cout << "Waiting for clients to connect...\n" ;
	while ( clientID < (maxPlayers + 1) )
	{
	   if(listenForClient(clientID))
	   {
			std::cout << "client " << clientID <<   " has been connected to the server\n" ;
        
			clientID++;
	   }
	}


	//Player::playerStruct * NetworkTCP::arrsOfPlayers[4];
	std::cout << "Sending client packets to clients.\n" ;

	//START THREAD FOR EACH CLIENT
	for ( int i = 0 ; i < maxPlayers ; ++i )
	{
		pthread_t * thread = new pthread_t(); //INITIALIZE THREADS
		NetworkTCP::readThreads[i] = thread ;

		Player::playerStruct *  thisPlayer = new Player::playerStruct() ; //INITIALIZE DATA STRUCTURES
		NetworkTCP::arrsOfPlayers[i] = thisPlayer ;
	
		(*NetworkTCP::arrsOfPlayers[i]).client = i+1 ;//SET CLIENT VALUES
		(*NetworkTCP::arrsOfPlayers[i]).playerInfo = &playerSessions ;
		if ( i == 0 )
		{
			(*NetworkTCP::arrsOfPlayers[i]).gameStarted = true ;
			(*NetworkTCP::arrsOfPlayers[i]).recvQueue = new std::queue<unsigned char>() ;
			(*NetworkTCP::arrsOfPlayers[i]).turn = 1;
			
		}
		
		
		char one = static_cast<char>(i+ 1);
		StreamHandler::sendPacket ( playerSessions[i+1] , &one, 1 ); //send a client command signal for each player


		pthread_create ( readThreads[i], NULL, &readStream, NetworkTCP::arrsOfPlayers[i] ); //RUN THREADS
	}

	std::cout << "\n" << "All Clients have connected. The game has started.\n" ;
	char one = static_cast<char>(0x05);
	StreamHandler::sendPacketToAll ( &playerSessions , &one, 1 ); //send a start game signal to all players

	std::cout << "\n" << "Start the game with turn for client 1.\n" ;
	one = static_cast<char>(0x40 | 0x01);
	StreamHandler::sendPacketToAll ( &playerSessions , &one, 1 ); //send a start game signal to all players
}

bool NetworkTCP::listenForClient ( unsigned int & id )
{
	clientSocket = accept ( listenSocket, NULL, NULL );

	if (clientSocket != INVALID_SOCKET) 
    {
        //disable nagle on the client's socket
        char value = 1;
        setsockopt( clientSocket, IPPROTO_TCP, TCP_NODELAY, &value, sizeof( value ) );

        // insert new client into session id table
        playerSessions.insert( std::pair<unsigned int, SOCKET>(id, clientSocket) );
		
        return true;
    }
    return false;
}

bool NetworkTCP::disconnectClients ( void )
{
	for ( int i = 0 ; i < maxPlayers ; ++i )
	{

		pthread_t * thread = NetworkTCP::readThreads[i];
		delete thread;
	}

	for ( int i = 0 ; i < maxPlayers ; ++i )
	{
		Player::playerStruct * thisStruct =  NetworkTCP::arrsOfPlayers[i];
		
		delete thisStruct;
	}

	return true;






}

Player::playerStruct ** NetworkTCP::returnPlayerArray (void )
{		
		return NetworkTCP::arrsOfPlayers;
}

int NetworkTCP::returnIntMe ( void )
{
	return 1;
}

void * NetworkTCP::readStream (void * args )
{
	try
	{
		char  buffer ;

		int retValue ;

		struct Player::playerStruct * thisPlayer = (struct Player::playerStruct *)args;

		Sleep(2000);

		while ( !thisPlayer->disconnect )
		{
			
			if ( StreamHandler::recvPacket ( thisPlayer->playerInfo->at(thisPlayer->client ) , &buffer , 1 ) > 0)
			{
				
				//pthread_mutex_lock (
				thisPlayer->recvQueue->push( static_cast<unsigned char>(buffer) ) ;
			}
			else
			{
				std::cout << "The socket/client " << thisPlayer->client << " has returned an error code of : " << WSAGetLastError() << ".\n" ;
				std::cout << ".\n" ;
				std::cout << "A client disconnected from the the server\n" ;
				thisPlayer->disconnect = true;
			}
		}
	}
	catch ( std::exception const & ex )
	{
		std::cout << ex.what() << " : thread\n" ; 
	}
	return NULL;
}
