using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using System.Threading.Tasks;

namespace SeventhLabaOS_1 {
	class Seventh_1 {
		static volatile bool isActive = true;

		static void Messenger() {
			MessageQueue messageQueue = new MessageQueue(".\\myAwesomeQueue");

			long i = 0;
			while (isActive) {
				messageQueue.Send($" Message: {i++}");
			}
			Console.WriteLine("Thread was interrupted!");
		}

		static void Main(string[] args) {
			Task.Run(() => Messenger());
			Console.ReadKey();
			isActive=false;
		}
	}
}
