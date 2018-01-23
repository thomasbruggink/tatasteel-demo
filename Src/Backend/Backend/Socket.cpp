#include "Socket.h"
#include "Utils.h"

using namespace Backend;
using namespace std;

Socket::Socket(NetworkProtocol protocol)
{
	_protocol = protocol;
	ZeroMemory(&_hints, sizeof(_hints));
	_hints.ai_family = AF_INET;
	switch (protocol)
	{
	case TCP:
		_hints.ai_socktype = SOCK_STREAM;
		_hints.ai_protocol = IPPROTO_TCP;
		break;
	case UDP:
		_hints.ai_socktype = SOCK_DGRAM;
		_hints.ai_protocol = IPPROTO_UDP;
		break;
	}
	_socket = INVALID_SOCKET;
	_setup = false;
}

bool Socket::Listen(string ip, int port)
{
	WSAData data;
	struct addrinfo *result;

	if (WSAStartup(MAKEWORD(2, 2), &data) != 0)
	{
		return false;
	}

	string portstring = Utils::Int2String(port);

	if (getaddrinfo(ip.c_str(), portstring.c_str(), &_hints, &result) != 0)
	{
		WSACleanup();
		return false;
	}

	_socket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);

	if (_socket == INVALID_SOCKET)
	{
		int error = WSAGetLastError();
		freeaddrinfo(result);
		WSACleanup();
		return false;
	}

	if (_protocol == TCP)
	{
		int bindResult = bind(_socket, result->ai_addr, (int)result->ai_addrlen);
		if (bindResult == SOCKET_ERROR)
		{
			int error = WSAGetLastError();
			freeaddrinfo(result);
			WSACleanup();
			return false;
		}
	}
	else if (_protocol == UDP)
	{
		_udpin.sin_addr.s_addr = htonl(INADDR_ANY);
		_udpin.sin_port = htons(0);
		_udpin.sin_family = AF_INET;
		inet_pton(AF_INET, ip.c_str(), &_udpout.sin_addr.s_addr);
		_udpout.sin_port = htons(port);
		_udpout.sin_family = AF_INET;
		if (bind(_socket, (sockaddr*)&_udpin, sizeof(_udpin)) == SOCKET_ERROR)
		{
			int error = WSAGetLastError();
			closesocket(_socket);
			_socket = INVALID_SOCKET;
		}
	}

	freeaddrinfo(result);

	if(listen(_socket, SOMAXCONN) == SOCKET_ERROR)
	{
		closesocket(_socket);
		WSACleanup();
		return false;
	}

	_setup = true;

	return true;
}

bool Socket::AcceptClient(int timeout)
{
	if(!IsDataAvailable(&_socket, timeout))
		return false;

	_clientSocket = accept(_socket, NULL, NULL);
	if (_clientSocket == INVALID_SOCKET)
	{
		int error = WSAGetLastError();
		return false;
	}
	return true;
}

bool Socket::IsSetup()
{
	return _setup;
}

void Socket::SendData(char ** data, int size)
{
	if (_protocol == TCP)
		send(_clientSocket, *data, size, 0);
	else if (_protocol == UDP)
		sendto(_clientSocket, *data, size, 0, (sockaddr*)&_udpout, sizeof(_udpout));
}

bool Socket::GetData(char ** data, int size)
{
	for(int i = 0; i < 2; i++)
	{
		if(IsDataAvailable(&_clientSocket, 1000))
			break;
		if (i == 1)
			return false;
	}
	if (_protocol == TCP)
		return recv(_clientSocket, *data, size, MSG_WAITALL) > 0;
	else if (_protocol == UDP)
		return recvfrom(_clientSocket, *data, size, 0, NULL, 0) > 0;
	return false;
}

bool Socket::CloseClientConnection()
{
	shutdown(_clientSocket, SD_SEND | SD_RECEIVE);
	return true;
}

bool Socket::CloseConnection()
{
	shutdown(_socket, SD_SEND | SD_RECEIVE);
	_setup = false;
	return true;
}

bool Socket::IsDataAvailable(SOCKET* socket, int timeout)
{
	FD_SET readSet;
	FD_ZERO(&readSet);
	FD_SET(*socket, &readSet);
	timeval time;
	time.tv_sec = 0;
	time.tv_usec = timeout * 1000;
	return select(0, &readSet, NULL, NULL, &time) == 1;
}

Socket::~Socket()
{
	if (_socket != INVALID_SOCKET)
		closesocket(_socket);
	WSACleanup();
}