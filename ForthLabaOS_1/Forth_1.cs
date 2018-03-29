using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace ForthLabaOS_1 {
	class Forth_1 {
		static volatile bool flag1 = false, flag2 = false;



		static string FirstThread(int number) {
			Semaphore semaphore = new Semaphore(2, 2, "sem");
			while (!flag1) {
				using (FileStream stream = new FileStream("../../../file.txt", FileMode.Append, FileAccess.Write, FileShare.Write)) {
					using (StreamWriter sw = new StreamWriter(stream)) {
						Console.WriteLine("start");
						sw.AutoFlush=true;
						semaphore.WaitOne(10);
						for (int i = 0; i<5; i++) {
							Console.Write("print+");
							sw.Write(number); }
						sw.Flush();
						stream.Flush();

						semaphore.Release();
						Console.WriteLine("end");
						Thread.Sleep(30);
					}
				}
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
