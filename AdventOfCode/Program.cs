using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AdventOfCode
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length < 3)
			{
				Console.WriteLine("Brak parametrów. 1 - dzień, 2 - część, 3 - daneWejściowe");
				return -1;
			}

			string typ = $"{args[0]}.{args[1]}";
			switch (typ)
			{
				case "1.1": return AoC_1a(args[2]);
				case "1.2": return AoC_1b(args[2]);
				case "2.1": return AoC_2a(args[2]);
				case "2.2": return AoC_2b(args[2]);
				case "3.1": return AoC_3a(args[2]);
				case "3.2": return AoC_3b(args[2]);
				case "4.1": return AoC_4a(args[2]);
				case "4.2": return AoC_4b(args[2]);
			}

			return 0;
		}

		static int AoC_1a(string input)
		{
			input = System.IO.File.ReadAllText(input);

			int wynik = input.Count(c => c == '(') - input.Count(c => c == ')');
			Console.WriteLine(wynik);
			return wynik;
		}

		static int AoC_1b(string input)
		{
			input = System.IO.File.ReadAllText(input);

			int wynik = 0;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '(')
					wynik++;
				if (input[i] == ')')
					wynik--;
				if (wynik < 0)
				{
					wynik = i + 1;
					break;
				}
			}
			Console.WriteLine(wynik);
			return wynik;
		}

		static int AoC_2a(string input)
		{
			input = System.IO.File.ReadAllText(input);

			string[] packs = input.Split("\r\n");
			int total = 0;
			foreach (string line in packs)
			{
				string[] dimensions = line.Split("x");
				int l = Convert.ToInt32(dimensions[0]), w = Convert.ToInt32(dimensions[1]), h = Convert.ToInt32(dimensions[2]);
				total += 2 * l * w + 2 * l * h + 2 * w * h + Math.Min(Math.Min(l * w, h * w), l * h);
			}
			Console.WriteLine(total);
			return total;
		}

		static int AoC_2b(string input)
		{
			input = System.IO.File.ReadAllText(input);
			string[] packs = input.Split("\r\n");
			int total = 0;
			foreach (string line in packs)
			{
				string[] dimensions = line.Split("x");
				int[] num = dimensions.Select(s => Convert.ToInt32(s)).OrderBy(i => i).ToArray();
				total += 2 * num[0] + 2 * num[1] + num[0] * num[1] * num[2];
			}
			Console.WriteLine(total);
			return total;
		}

		static int AoC_3a(string input)
		{
			input = System.IO.File.ReadAllText(input);
			List<(int x, int y)> houses = new List<(int x, int y)>();
			houses.Add((0, 0));
			(int x, int y) current = (0, 0);
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '<') current = (current.x - 1, current.y);
				if (input[i] == '>') current = (current.x + 1, current.y);
				if (input[i] == '^') current = (current.x, current.y + 1);
				if (input[i] == 'v') current = (current.x, current.y - 1);

				if (!houses.Contains(current))
					houses.Add(current);
			}
			
			Console.WriteLine(houses.Count);
			return houses.Count;
		}

		static int AoC_3b(string input)
		{
			input = System.IO.File.ReadAllText(input);
			List<(int x, int y)> houses = new List<(int x, int y)>();
			houses.Add((0, 0));
			(int x, int y) current1 = (0, 0);
			(int x, int y) current2 = (0, 0);
			for (int i = 0; i < input.Length; i += 2)
			{
				switch (input[i])
				{
					case '<': current1 = (current1.x - 1, current1.y); break;
					case '>': current1 = (current1.x + 1, current1.y); break;
					case '^': current1 = (current1.x, current1.y + 1); break;
					case 'v': current1 = (current1.x, current1.y - 1); break;
				}

				if (!houses.Contains(current1))
					houses.Add(current1);
			}
			for (int i = 1; i < input.Length; i += 2)
			{

				switch (input[i])
				{
					case '<': current2 = (current2.x - 1, current2.y); break;
					case '>': current2 = (current2.x + 1, current2.y); break;
					case '^': current2 = (current2.x, current2.y + 1); break;
					case 'v': current2 = (current2.x, current2.y - 1); break;
				}

				if (!houses.Contains(current2))
					houses.Add(current2);
			}

			Console.WriteLine(houses.Count);
			return houses.Count;
		}

		static int AoC_4a(string input)
		{
			int number = 0;
			string hash = "";
			while (!hash.StartsWith("00000"))
				hash = string.Concat(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input + ++number)).Select(b => b.ToString("X2")));

			Console.WriteLine(number);
			return number;
		}

		static int AoC_4b(string input)
		{
			int number = 0;
			string hash = "";
			while (!hash.StartsWith("000000"))
				hash = string.Concat(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input + ++number)).Select(b => b.ToString("X2")));

			Console.WriteLine(number);
			return number;
		}
	}
}
