#include "stdafx.h"
#include "main.h"
#include "ServerProcess.h"

ServerProcess * server ;
char * inPort;

int main(int argc, char* argv[])
{
	for ( int i = 0 ; i < argc ; ++i ) 
	{
		std::cout << "Arg " << i <<" :: " << argv[i] << " of type :: " <<  typeid(argv[i]).name() << "\n";
	}
	std::cout << "\n" ;
	inPort = argv[1] ;

	server = new ServerProcess ();

}



