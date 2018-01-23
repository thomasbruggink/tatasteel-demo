#include "AvailabilityServer.h"
#include "Socket.h"
#include "Utils.h"
#include <string>
#include <iostream>
#include <intrin.h>

using namespace Backend;
using namespace std;

AvailabilityServer::AvailabilityServer(IProductReader^ productReader)
{
	_productReader = productReader;
	_isRunning = false;
}

void AvailabilityServer::Start()
{
	cout << "Starting up server" << endl;
	_isRunning = true;
	Socket socket(TCP);
	if (!socket.Listen("127.0.0.1", 5000))
	{
		cout << "Unable to startup socket" << endl;
		return;
	}
	char* buffer = new char[1024];
	for(int i = 0; i < 1024; i++)
		buffer[i] = 0;
	cout << "Waiting for client" << endl;
	while (_isRunning)
	{
		if (!socket.AcceptClient(100))
			continue;
		cout << "Client connected" << endl;
		while(true)
		{
			// Receive the size of the Id
			if(!socket.GetData(&buffer, 2))
			{
				cout << "No data closing connection" << endl;
				socket.CloseClientConnection();
				break;
			}
			int idSize = (buffer[1] << 8) + buffer[0];
			int count = -1;
			if(idSize > 0)
			{
				// Read the id
				if(!socket.GetData(&buffer, idSize))
				{
					cout << "No data closing connection" << endl;
					socket.CloseClientConnection();
					break;
				}
				string productId(buffer);
				cout << "Request received for productId: " << productId << endl;
				// Find data
				count = _productReader->GetAvailibility(gcnew System::String(productId.c_str()));
			}
			// Create mapping
			buffer[3] = count >> 24;
			buffer[2] = count >> 16;
			buffer[1] = count >> 8;
			buffer[0] = count;
			socket.SendData(&buffer, 4);
		}
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

void AvailabilityServer::Stop()
{
	_isRunning = false;
}