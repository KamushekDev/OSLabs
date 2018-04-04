using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace test {
	class Test {

		static void First() {
			Mutex mutex = Mutex.OpenExisting("lalka");
			while (true)
				if (!mutex.WaitOne(100))
					Console.WriteLine("Мьютекс забрали :с Плак плак");
				else
					Console.WriteLine("Отвоевал!!!");
		}

		static void Main(string[] args) {

			//Mutex mutex = new Mutex(false, "lalka");

			//mutex.WaitOne();

			//Task.Run(()=> First());

			//Thread.Sleep(10000);

			//mutex.ReleaseMutex();

			int[] nk = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

			int max = 0, current = 0, lessons = 0;


			int[] lections = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

			int[] snooze = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

			lessons=lections.Sum();

			for (int i = 0; i<nk[0]; i++) {
				if (snooze[i]==1) 
					lections[i]=0;
				else
					lessons-=lections[i];
			}

			for (int i = 0; i<nk[1]; i++) {
				current+=lections[i];
			}
			max=current;
			for (int i = nk[1]; i<nk[0]; i++) {
				current-=lections[i-nk[1]];
				current+=lections[i];
				if (max<current)
					max=current;
			}
			Console.WriteLine(lessons+max);

			Console.ReadLine();
		}
	}
}
