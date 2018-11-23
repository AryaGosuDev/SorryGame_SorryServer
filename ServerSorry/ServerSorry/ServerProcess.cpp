#include "StdAfx.h"
#include "ServerProcess.h"
#include "NetworkTCP.h"
#include "StreamHandler.h"


extern int maxPlayers;

NetworkTCP * network;
Player::playerStruct ** clientStructs;

std::queue<unsigned char> * recvQueue;



ServerProcess::ServerProcess ( void )
{
	srand (time(NULL));
	network = new NetworkTCP () ;
	turn = 1;
	service();
}

ServerProcess::~ServerProcess(void)
{
}

void ServerProcess::service(void )
{
	clientStructs = network->returnPlayerArray();

	recvQueue = clientStructs[0]->recvQueue ;

	unsigned char buffer; 
	char tempBuffer ;

	int randRoll;

	//std::cout << clientStructs[0]->client << "   " << clientStructs[1]->client << "\n" ;

	while ( !clientStructs[0]->disconnect )
	{
		try
		{
			if ( !recvQueue->empty() )
			{
			
				buffer = recvQueue->front();
				int boardPosition = 0;

				tempBuffer = buffer >> 6 ;


				if ( tempBuffer < 0 ) tempBuffer = tempBuffer * -1 ;
				switch (tempBuffer)
				{
					case 0x00:
						if ( clientStructs[0]->gameStarted == true )
						{
							

							std::cout << "Received a win command. Sending clients a win for " << (int)buffer  << ".\n" ;
							tempBuffer = buffer;
							StreamHandler::sendPacketToAll ( clientStructs[0]->playerInfo , &tempBuffer , 1 );
							clientStructs[0]->gameStarted = false;
						}
						break;

					case 0x01:
						
						if ( clientStructs[0]->turn == maxPlayers) clientStructs[0]->turn = 1;
						else clientStructs[0]->turn += 1; 
				
						//tempBuffer = 0x40 | (char)(turn ) ;
						tempBuffer = 0x40 | (char)(clientStructs[0]->turn ) ;

						StreamHandler::sendPacketToAll ( clientStructs[0]->playerInfo , &tempBuffer , 1 );

						std::cout << "Received a turn command. Sending clients a turn for  " << clientStructs[0]->turn  << ".\n" ;
						
						break;
					case 0x02: // ROLL
						randRoll = rand() % 6 ;
						if ( randRoll <= 1 )
						{
							randRoll = rand() % 2 + 1;
							
						}
						else
						{
							randRoll = rand() % 9 + 3;
							//randRoll = 8;
						}
						
						tempBuffer = 0x80 ;
						tempBuffer = tempBuffer | ((char)randRoll);

						

						//StreamHandler::sendPacketToAll ( clientStructs[0]->playerInfo, &tempBuffer , 1 );
						//if ( randRoll < 4 ) tempBuffer = 0x80 | 0x02 ;
						//else tempBuffer = 0x80 | 0x06;
						StreamHandler::sendPacketToAll ( clientStructs[0]->playerInfo, &tempBuffer , 1 );

						std::cout << "Sent a roll of " << randRoll << ".\n" ;

						break;
					case 0x03:

						tempBuffer = buffer << 2;
						tempBuffer = tempBuffer >> 2;

						tempBuffer = 0xC0 | tempBuffer;

						std::cout << "Sending a move signal to all clients, move command : " << std::hex << ((int)tempBuffer) << "\n" ;

						StreamHandler::sendPacketToAll ( clientStructs[0]->playerInfo , &tempBuffer , 1 ); 

						break;
					default:
						break;
				}

				std::cout << "On buffer : " << std::hex <<  ((int)buffer) << "\n" ;

				recvQueue->pop ();

			}
		} 
		catch ( std::exception const & ex )
		{
			std::cout << ex.what() << " : process loop\n" ;
		}

		
	}
	network->disconnectClients ();
}