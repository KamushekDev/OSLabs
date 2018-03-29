using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstLabaOS {
	class First {

		static volatile bool flag1 = false, flag2 = false;

		static string FirstThread(int number) {
			while (!flag1) {
				Console.Write(number);
				Thread.Sleep(10);
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "First";
		}

		static string SecondThread(int number) {
			while (!flag2) {
				Console.Write(number);
				Thread.Sleep(10);
			}
			Console.WriteLine($"Thread {number} was interrupted.");
			return "Second";
		}


		static void Main(string[] args) {

			Task<string> task1, task2;

			task1=Task.Run(() => FirstThread(1));
			task2=Task.Run(() => SecondThread(2));

			Console.ReadKey(true);
			flag1=flag2=true;

			Console.WriteLine();
			Console.WriteLine($"First thread returned: {task1.Result}");
			Console.WriteLine($"Second thread returned: {task2.Result}");
			Console.WriteLine("Threads were interrupted.");
			Console.ReadKey(true);
		}
	}
}
