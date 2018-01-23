#ifndef UTILS_H_
#define UTILS_H_

#include <string.h>
#include <sstream>
#include <vector>

namespace Backend
{
	class Utils
	{
	public:
		static std::string Int2String(int i)
		{
			std::stringstream ss;
			ss << i;
			return ss.str();
		}


		static int String2Int(std::string msg)
		{
			int numb = 0;
			std::istringstream(msg) >> numb;
			return numb;
		}

		static std::string Float2String(float i)
		{
			std::stringstream ss;
			ss << i;
			return ss.str();
		}

		static float String2Float(std::string msg)
		{
			float numb = 0;
			std::istringstream(msg) >> numb;
			return numb;
		}

		static double String2Double(std::string msg)
		{
			double numb = 0;
			std::istringstream(msg) >> numb;
			return numb;
		}

		static std::string Double2String(double i)
		{
			std::stringstream ss;
			ss << i;
			return ss.str();
		}

		static std::string Bool2String(bool i)
		{
			return i ? "true" : "false";
		}

		static bool String2Bool(std::string s)
		{
			return (strcmp(s.c_str(), "0") != 0 && strcmp(s.c_str(), "false") != 0);
		}

		static std::string ArrayToGuid(char* data)
		{
			std::string guid;
			for (int i = 3; i >= 0; i--)
			{
				guid += ByteToChar((data[i] >> 4) & 0x0F);
				guid += ByteToChar(data[i] & 0x0F);
			}
			guid += "-";
			for (int i = 1; i >= 0; i--)
			{
				guid += ByteToChar((data[i + 4] >> 4) & 0x0F);
				guid += ByteToChar(data[i + 4] & 0x0F);
			}
			guid += "-";
			for (int i = 1; i >= 0; i--)
			{
				guid += ByteToChar((data[i + 6] >> 4) & 0x0F);
				guid += ByteToChar(data[i + 6] & 0x0F);
			}
			guid += "-";
			for (int i = 0; i < 2; i++)
			{
				guid += ByteToChar((data[i + 8] >> 4) & 0x0F);
				guid += ByteToChar(data[i + 8] & 0x0F);
			}
			guid += "-";
			for (int i = 0; i < 6; i++)
			{
				guid += ByteToChar((data[i + 10] >> 4) & 0x0F);
				guid += ByteToChar(data[i + 10] & 0x0F);
			}
			return guid;
		}

		static char* GuidToArray(std::string guid)
		{
			char* data = new char[16];
			for (int i = 3; i >= 0; i--)
			{
				data[i] = CharToByte(guid[(3 - i) * 2]);
				data[i] = (data[i] << 4) + CharToByte(guid[((3 - i) * 2) + 1]);
			}
			int off = 4;
			for (int i = 1; i >= 0; i--)
			{
				//+1 skip -
				data[i + off] = CharToByte(guid[(((off + 1) - i) * 2) + 1]);
				data[i + off] = (data[i + off] << 4) + CharToByte(guid[(((off + 1) - i) * 2) + 2]);
			}
			off = 6;
			for (int i = 1; i >= 0; i--)
			{
				data[i + off] = CharToByte(guid[((off + 2) - i) * 2]);
				data[i + off] = (data[i + off] << 4) + CharToByte(guid[(((off + 2) - i) * 2) + 1]);
			}
			off = 8;
			for (int i = 0; i < 2; i++)
			{
				data[i + off] = CharToByte(guid[(((off + 1) + i) * 2) + 1]);
				data[i + off] = (data[i + off] << 4) + CharToByte(guid[(((off + 1) + i) * 2) + 2]);
			}
			off = 10;
			for (int i = 0; i < 6; i++)
			{
				data[i + off] = CharToByte(guid[((off + 2) + i) * 2]);
				data[i + off] = (data[i + off] << 4) + CharToByte(guid[(((off + 2) + i) * 2) + 1]);
			}
			return data;
		}

		static std::vector<std::string> Split(char delimiter, std::string line)
		{
			std::vector<std::string> elems;
			std::stringstream ss(line);
			std::string item;
			while (std::getline(ss, item, delimiter)) elems.push_back(item);
			return elems;
		}

		static char ByteToChar(unsigned char b)
		{
			switch (b)
			{
			case 10:
				return 'A';
			case 11:
				return 'B';
			case 12:
				return 'C';
			case 13:
				return 'D';
			case 14:
				return 'E';
			case 15:
				return 'F';
			default:
				return (char)('0' + b);
			}
		}

		static char CharToByte(char c)
		{
			switch (c)
			{
			case 'A':
				return 10;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				return c - '0';
			}
		}
	};
}

#endif
