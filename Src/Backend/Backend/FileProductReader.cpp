#include "FileProductReader.h"

using namespace System;
using namespace System::IO;
using namespace Backend;

FileProductReader::FileProductReader()
{
	// Read content
	auto file = File::ReadAllLines(".\\data.txt");
	for each (auto line in file)
	{
		auto splitData = line->Split('|');
		auto id = splitData[0];
		auto count = Int32::Parse(splitData[1]);
		_productCount.Add(id, count);
	}
}

int FileProductReader::GetAvailibility(String^ productId)
{
	if (!_productCount.ContainsKey(productId))
		return -1;
	return _productCount[productId];
}
