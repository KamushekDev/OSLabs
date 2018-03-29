using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;

namespace FifthLabaOS_1 {
	class Fifth_1 {

		static volatile bool flag1 = false;

		static string FirstThread(int number) {
			Random random = new Random();
			MemoryMappedFile sharedMemory = MemoryMappedFile.CreateNew("memka", 1);
			Semaphore semaphore = new Semaphore(2, 2, "semka");
			MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, 1, MemoryMappedFileAccess.ReadWrite);
			while (!flag1) {
				semaphore.WaitOne(10);
				writer.Write(0, (byte)random.Next(byte.MaxValue));
				semaphore.Release();
				Thread.Sleep(10);
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "First";
		}


		static void Main(string[] args) {

			Task<string> task1;

			task1=Task.Run(() => FirstThread(1));

			Console.ReadKey(true);
			flag1=true;

			Console.WriteLine();
			Console.WriteLine($"First thread returned: {task1.Result}");
			Console.WriteLine("Threads were interrupted.");
			Console.ReadKey(true);
		}

	}
}
