using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EighthLabaOS_2 {
	class Eighth_2 {
		static volatile bool isActive = true;

		static void Messenger() {

			try {

				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				do {
					try {
						socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1488));
					} catch { }
				} while (!socket.Connected&&isActive);

				Console.WriteLine("Connected!");

				while (isActive) {
					socket.Send(new byte[] { (byte)(new Random().Next(byte.MaxValue)) });
					byte[] longer = new byte[8];
					socket.Receive(longer);
					Console.WriteLine(BitConverter.ToInt64(longer, 0));
					Thread.Sleep(270);
				}
				socket.Close();
			} catch { }
			Console.WriteLine("Thread was interrupted!");
		}

		static void Main(string[] args) {
			Task.Run(() => Messenger());
			Console.ReadKey();
			isActive=false;
		}
	}
}
