using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EighthLabaOS_1 {
	class Eighth_1 {
		static volatile bool isActive = true;

		static long result = 0;

		static void WorkWithMessage(TcpClient client) {
			try {
				using (Stream stream = client.GetStream()) {
					while (isActive) {
						byte message = (byte)stream.ReadByte();
						Console.Write($"{result}+{message}=");
						result+=message;
						Console.WriteLine(result);
						stream.Write(BitConverter.GetBytes(result), 0, 8);
						Thread.Sleep(150);
					}
				}
				client.Close();
			} catch { }
		}

		static async void ListenerAsync() {


			TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 1488);
			listener.Start();
			while (isActive) {
				TcpClient client = await listener.AcceptTcpClientAsync();

				await Task.Run(() => WorkWithMessage(client));

			}
			Console.WriteLine("Thread was interrupted!");
		}

		static void Main(string[] args) {
			Task.Run(() => ListenerAsync());
			Console.ReadKey();
			isActive=false;
		}

	}
}
