using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace SecondLabaOS {
	class Second {

		static volatile bool flag1 = false, flag2 = false;

		const int N = 5;

		static string FirstThread(int number) {
			if (Mutex.TryOpenExisting("myAwesomeMutex", out Mutex mutex)) {
				while (!flag1) {
					if (!mutex.WaitOne(10)) {
						//doing smth
					} else {
						for (int i = 0; i<N; i++) {
							Console.Write(number);
						}
						mutex.ReleaseMutex();
						Thread.Sleep(10);
					}
				}
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "First";
		}

		static string SecondThread(int number) {
			if (Mutex.TryOpenExisting("myAwesomeMutex", out Mutex mutex)) {
				while (!flag2) {
					if (!mutex.WaitOne(10)) {
						//doing smth
					} else {
						for (int i = 0; i<N; i++) {
							Console.Write(number);
						}
						mutex.ReleaseMutex();
						Thread.Sleep(17);
					}
				}
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "Second";
		}


		static void Main(string[] args) {

			Task<string> task1, task2;

			Mutex mutex = new Mutex(false, "myAwesomeMutex");

			task1=Task.Run(() => FirstThread(1));
			task2=Task.Run(() => SecondThread(2));

			Console.ReadKey(true);
			flag1=flag2=true;

			Console.WriteLine();
			Console.WriteLine($"First thread returned: {task1.Result}");
			Console.WriteLine($"Second thread returned: {task2.Result}");
			Console.WriteLine("Threads were interrupted.");
			mutex.Dispose();
			Console.ReadKey(true);
		}
	}
}
