﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForthLabaOs_2 {
	class Forth_2 {
		static volatile bool flag2 = false;


		static string SecondThread(int number) {
			Semaphore semaphore = Semaphore.OpenExisting("sem");
			while (!flag2) {
				using (FileStream stream = new FileStream("../../../file.txt", FileMode.Append, FileAccess.Write, FileShare.Write)) {
					using (StreamWriter sw = new StreamWriter(stream)) {
						Console.WriteLine("start");
						sw.AutoFlush=true;
						semaphore.WaitOne(10);
						for (int i = 0; i<5; i++) {
							Console.Write("print+");
							sw.Write(number);
						}
						sw.Flush();
						stream.Flush();

						semaphore.Release();
						Console.WriteLine("end");
						Thread.Sleep(30);
					}
				}
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "Second";
		}


		static void Main(string[] args) {

			

			Task<string> task2;

			task2=Task.Run(() => SecondThread(2));

			Console.ReadKey(true);
			flag2=true;

			Console.WriteLine();
			Console.WriteLine($"Second thread returned: {task2.Result}");
			Console.WriteLine("Threads were interrupted.");
			Console.ReadKey(true);
		}
	}
}