#include "AvailabiliyServer.h"
#include "Socket.h"
#include "Utils.h"
#include <string>
#include <iostream>

using namespace std;

AvailabilityServer::AvailabilityServer()
{
	_isRunning = false;
}

void AvailabilityServer::Start()
{
	cout << "Starting up server" << endl;
	Socket socket(TCP);
	if (!socket.Listen("127.0.0.1", 5000))
	{
		cout << "Unable to startup socket" << endl;
		return;
	}
	char* buffer = new char[1024];
	while (_isRunning)
	{
		cout << "Waiting for client" << endl;
		if (!socket.AcceptClient(5000))
			continue;
		cout << "Client connected" << endl;
		// Receive the size of the Id
		socket.GetData(&buffer, 2);
		int idSize = (buffer[1] << 8) | (buffer[0]);
		// Read the id
		socket.GetData(&buffer, idSize);
		string productId(buffer);
		cout << "Request received for productId: " + productId << endl;
		// Find data
		int count = 5;
		// Create mapping
		buffer[0] = count >> 24;
		buffer[1] = count >> 16;
		buffer[2] = count >> 8;
		buffer[3] = count;
		socket.SendData(&buffer, 4);
		if (!socket.CloseClientConnection())
		{
			cout << "Client connection could not be closed" << endl;
		}
		cout << "Client connection closed" << endl;
	}
	cout << "Server stopped" << endl;
}

bool AvailabilityServer::IsAlive()
{
	return _isRunning;
}