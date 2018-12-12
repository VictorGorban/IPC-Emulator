using Extras;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
			Console.Title = "Read";


			using (MemoryMappedFile.CreateOrOpen("MyMmf", 400 + FileHeader.SizeInBytes))
			{
				while (true)
				{
					WriteLine("Нажмите Enter чтобы считать данные из общей памяти");
					ReadLine();

					var result = ReadFromSharedMemoryWithSleep(MemoryMappedFile.CreateOrOpen("MyMmf", 400 + FileHeader.SizeInBytes));
					WriteLine(result);
				} 
			}
		}

		public static string ReadFromSharedMemoryWithSleep(MemoryMappedFile mmf)
		{
			// создаю view для header
			// считываю header, проверяю, что читать можно
			// если нет, return "Не могу прочитать, занято";
			// изменяю header, что нельзя писать
			// записываю изменный header
			// записываю данные
			// опять изменяю header на писать можно
			// записываю изменный header

			// Так, а теперь семафор
			pool.WaitOne();

			var view = mmf.CreateViewAccessor();
			var bytes = new byte[reqNumBytes];
			view.ReadArray(0, bytes, 0, bytes.Length);

			WriteLine("Sleeping 2 sec");
			Sleep(2000);
			pool.Release();

			var str = bytes.ToUnicodeString();
			return str;

		}
	}

}
