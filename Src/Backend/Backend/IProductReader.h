#pragma once

namespace Backend
{
	public interface class IProductReader
	{
	public:
		int GetAvailibility(System::String^ productId);
	};
}