#ifndef Socket_H
#define Socket_H

#include <WS2tcpip.h>
#include <WinSock2.h>
#include <queue>

enum NetworkProtocol
{
	TCP,
	UDP
};

class Socket
{
public:
	Socket(NetworkProtocol protocol);
	bool Listen(std::string ip, int port);
	bool IsSetup();
	bool AcceptClient(int timeout);
	void SendData(char ** data, int size);
	bool DataAvailable(int timeout);
	void GetData(char ** data, int size);
	bool CloseClientConnection();
	bool CloseConnection();
	~Socket();
private:
	struct addrinfo _hints;
	sockaddr_in _udpin, _udpout;
	SOCKET _socket, _clientSocket;
	NetworkProtocol _protocol;
	bool _setup;
	char* _buffer;
	int _buffersize;
	void ClearBuffer();
};

#endif