using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLabaOS_2 {
	class Seventh_2 {
		static volatile bool isActive = true;

		static void Listener() {
			MessageQueue messageQueue = new MessageQueue(".\\myAwesomeQueue");

			while (isActive) {
				Console.WriteLine(messageQueue.Receive());
			}
			Console.WriteLine("Thread was interrupted!");
		}

		static void Main(string[] args) {
			Task.Run(() => Listener());
			Console.ReadKey();
			isActive=false;
		}
	}
}
