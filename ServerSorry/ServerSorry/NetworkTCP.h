//#define _WIN32_WINNT 0x501
#include <map>
#include <queue>
#include "pthread.h"
#include <winsock2.h>
#include <ws2tcpip.h>
//#include "ServerProcess.h"
#include "Player.h"

#define DEFAULT_PORT "2345"

class NetworkTCP : public Player
{
public:
    NetworkTCP(void);
    ~NetworkTCP(void);

	static unsigned int clientID;
	

	void service ( void ) ;

	static void *readStream (void * args );
	
	// accept new connections
    bool listenForClient(unsigned int & id);

	bool disconnectClients(void );

    // Socket to listen for new connections
    SOCKET listenSocket;

    // Socket to give to the clients
    SOCKET clientSocket;

    // for error checking return values
    int iResult;

    // table to keep track of each client's socket
    std::map<unsigned int, SOCKET> playerSessions;

	playerStruct ** returnPlayerArray(void);

	int returnIntMe ( void );
	
	playerStruct * arrsOfPlayers[2];

	static pthread_t  * readThreads[];

private:

};

