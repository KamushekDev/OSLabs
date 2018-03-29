using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SixthLabaOS_1 {
	class Sixth_1 {
		static NamedPipeServerStream ServerPipe = new NamedPipeServerStream("myAwesomePipe");

		static volatile bool flag1 = false;

		static void Messenger() {
			ServerPipe.WaitForConnection();
			Random random = new Random();
			try {
				using (StreamWriter streamWriter = new StreamWriter(ServerPipe)) {
					while (!flag1) {
						streamWriter.WriteLine($"Number is {random.Next(byte.MaxValue)}");
						Thread.Sleep(10);
					}
				}
			} catch (IOException) { }
			ServerPipe.Close();
		}


		static void Main(string[] args) {

			Task.Run(() => Messenger());

			Console.ReadKey(true);
			flag1=true;

			Console.WriteLine();
			Console.WriteLine("Threads were interrupted.");

			Console.ReadKey(true);
		}
	}
}
