#include "Socket.h"
#include "Utils.h"

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
	_buffersize = 500;
	_buffer = new char[_buffersize];
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

	if (_socket == INVALID_SOCKET)
	{
		WSACleanup();
		return false;
	}

	_setup = true;

	return true;
}

bool Socket::AcceptClient(int timeout)
{
	_clientSocket = accept(_socket, NULL, NULL);
	if (_clientSocket == INVALID_SOCKET)
		return false;
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

bool Socket::DataAvailable(int timeout)
{
	timeval time;
	time.tv_sec = timeout;
	time.tv_usec = 0;
	fd_set set;
	FD_ZERO(&set);
	FD_SET(_clientSocket, &set);
	return select(_clientSocket, &set, NULL, NULL, &time) == 1;
}

void Socket::GetData(char ** data, int size)
{
	if (_protocol == TCP)
		recv(_clientSocket, *data, size, MSG_WAITALL);
	else if (_protocol == UDP)
		recvfrom(_clientSocket, *data, size, 0, NULL, 0);
}

void Socket::ClearBuffer()
{
	for (int i = 0; i < _buffersize; i++)
	{
		_buffer[i] = (char)0;
	}
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

Socket::~Socket()
{
	delete _buffer;
	if (_socket != INVALID_SOCKET)
		closesocket(_socket);
	WSACleanup();
}