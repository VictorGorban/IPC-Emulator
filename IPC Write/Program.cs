using Extras;
using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace IPC_Read
{
	class Program
	{
		public const int reqNumBytes = 400;

		// Если в системе уже существует семафор с данным именем, то не пересоздает.
		public static Semaphore pool = new Semaphore(1, 1, "MySystemwideSemaphore");

		static void Main(string[] args)
		{
			Console.Title = "Write";

			using (var mmf = MemoryMappedFile.CreateOrOpen("MyMmf", reqNumBytes))
			{
				while (true)
				{
					WriteLine("Введите строку и нажмите Enter чтобы записать эту строку в общую память");
					var strToWrite = ReadLine();

					var result = WriteToSharedMemoryWithSleep(mmf, strToWrite);
					WriteLine(result);
				} 
			}
			// Когда последний поток/процесс завершит работу, файл уничтожится сборщиком мусора.
		}

		public static string WriteToSharedMemoryWithSleep(MemoryMappedFile mmf, string str)
		{
			

			// Так, а теперь семафор
			pool.WaitOne();

			var view = mmf.CreateViewAccessor();
			var bytes = str.ToUnicodeBytes(reqNumBytes);
			view.WriteArray(0, bytes, 0, bytes.Length);

			WriteLine("Sleeping 4 sec");
			Sleep(4000);
			pool.Release();


			return "OK";
		}
	}
}