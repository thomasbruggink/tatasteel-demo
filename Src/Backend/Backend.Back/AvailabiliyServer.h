#pragma once

class AvailabilityServer
{
public:
	AvailabilityServer();
	bool IsAlive();
	void Start();
private:
	bool _isRunning;
};