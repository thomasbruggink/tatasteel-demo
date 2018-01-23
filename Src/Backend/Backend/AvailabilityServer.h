#pragma once

#include "IProductReader.h"

namespace Backend
{
	public ref class AvailabilityServer
	{
	public:
		AvailabilityServer(IProductReader^ productReader);
		bool IsAlive();
		void Start();
		void Stop();
	private:
		IProductReader ^ _productReader;
		bool _isRunning;
	};
}