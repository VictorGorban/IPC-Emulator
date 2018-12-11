using Extras;
using System;
using static System.Console;
using static System.Threading.Thread;

namespace IPC_Read
{
	class Program
	{
		public const int reqNumBytes = 400;

		static void Main(string[] args)
		{
			Console.Title = "Write";

			while (true)
			{
				WriteLine("Введите строку и нажмите Enter чтобы записать эту строку в общую память");
				var strToWrite = ReadLine();

				var result = WriteToSharedMemoryWithSleep(strToWrite);
				WriteLine(result);
			}
		}

		public static string WriteToSharedMemoryWithSleep(string str)
		{
			
			return "OK";
		}
	}
}