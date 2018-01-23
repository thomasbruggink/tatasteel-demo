#pragma once

#include "IProductReader.h"

namespace Backend
{
	public ref class FileProductReader : IProductReader
	{
	public:
		FileProductReader();
		// Inherited via IProductReader
		virtual int GetAvailibility(System::String^ productId);
	private:
		System::Collections::Generic::Dictionary<System::String^, int> _productCount;
	};
}