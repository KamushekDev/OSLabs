using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;

namespace ThirdLabaOS {
	class Third {

		static NamedPipeClientStream ClientPipe = new NamedPipeClientStream("myAwesomePipe");
		static NamedPipeServerStream ServerPipe = new NamedPipeServerStream("myAwesomePipe");

		static volatile bool flag1 = false, flag2 = false;

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

		static void Listener() {
			ClientPipe.Connect();
			using (StreamReader streamReader = new StreamReader(ClientPipe)) {
				while (!flag2) {
					if (streamReader.Peek()!=0)
						Console.WriteLine(streamReader.ReadLine());
					Thread.Sleep(7);
				}
			}
			ClientPipe.Close();
		}


		static void Main(string[] args) {

			Task.Run(() => Messenger());
			Task.Run(() => Listener());

			Console.ReadKey(true);
			flag1=flag2=true;

			Console.WriteLine();
			Console.WriteLine("Threads were interrupted.");

			Console.ReadKey(true);
		}
	}
}
