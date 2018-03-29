using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SixthLabaOS_2 {
	class Sixth_2 {
		static NamedPipeClientStream ClientPipe = new NamedPipeClientStream("myAwesomePipe");

		static volatile bool flag2 = false;


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
			Task.Run(() => Listener());

			Console.ReadKey(true);
			flag2=true;

			Console.WriteLine();
			Console.WriteLine("Threads were interrupted.");

			Console.ReadKey(true);
		}
	}
}
