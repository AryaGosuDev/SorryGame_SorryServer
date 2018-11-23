#include "StdAfx.h"
#include <map>
#include <queue>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <time.h>

class ServerProcess
{

	public:

		ServerProcess(void);
		~ServerProcess(void);

		void service();

		int turn;

		

	private:

};