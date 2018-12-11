using Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;



namespace IPC_Read
{
	class Program
	{
		public const int reqNumBytes = 400;

		static void Main(string[] args)
		{
			Console.Title = "Read";

			while (true)
			{
				WriteLine("Нажмите Enter чтобы считать данные из общей памяти");
				ReadLine();

				var result = ReadFromSharedMemoryWithSleep();
				WriteLine(result);
			}
		}

		public static string ReadFromSharedMemoryWithSleep()
		{
			
			return "OK";
		}
	}

}
