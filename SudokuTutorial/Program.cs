using System;

public class Program
{
	public static void Main()
	{
		foo();
		string[,] sudoku = new string[9, 9];
		for (int i = 0; i < fileContent.Length; i++)
		{
			if (i % 12 <= 8)
			{
				int row = i / 12;
				int column = i % 12;
				// Console.WriteLine(fileContent[i]);
				if (fileContent[i] == '0')
				{
					sudoku[row, column] = "123456789";
				}
				else
				{
					sudoku[row, column] = fileContent[i].ToString();
				}
			}
		}
		//Löse das Sudoku        
		Sudokuout(sudoku);
		int maxcounter = 0;
		while (!IsSudokuSolved(sudoku) && maxcounter < 50)
		{
			sudoku = OptimzeAll(sudoku);
			maxcounter++;
		}
		Sudokuout(sudoku);
		Console.ReadKey();
	}

	public static bool IsSudokuSolved(string[,] sudoku)
	{
		int rowLength = sudoku.GetLength(0);
		int colLength = sudoku.GetLength(1);

		for (int i = 0; i < rowLength; i++)
		{
			for (int j = 0; j < colLength; j++)
			{
				if (sudoku[i, j].Length > 1)
				{
					Console.WriteLine("Sudoku is not yet solved - Need more Optimization Loops!!!");
					return false;
				}
			}
		}
		Console.WriteLine("Sudoku is solved!!! Yeah!");
		return true;
	}

	public static void foo()
	{
		Console.WriteLine("fileContent.Length: " + fileContent.Length);
	}
	public static string[,] OptimzeAll(string[,] sudoku)
	{
		sudoku = OptimzeRow(sudoku);
		Sudokuout(sudoku);
		sudoku = OptimzeColumn(sudoku);
		Sudokuout(sudoku);
		sudoku = OptimzeBox(sudoku);
		Sudokuout(sudoku);
		return sudoku;
	}

	public static string[,] OptimzeBox(string[,] sudoku)
	{
		sudoku = OptimzeBox(sudoku, 0, 0);
		sudoku = OptimzeBox(sudoku, 0, 1);
		sudoku = OptimzeBox(sudoku, 0, 2);
		sudoku = OptimzeBox(sudoku, 1, 0);
		sudoku = OptimzeBox(sudoku, 1, 1);
		sudoku = OptimzeBox(sudoku, 1, 2);
		sudoku = OptimzeBox(sudoku, 2, 0);
		sudoku = OptimzeBox(sudoku, 2, 1);
		sudoku = OptimzeBox(sudoku, 2, 2);
		return sudoku;
	}

	public static string[,] OptimzeBox(string[,] sudoku, int boxIndexRow, int boxIndexColumn)
	{
		// (0->3),(0->3) box 1 (up left)
		// (0->3),(4->6) box 2 (up middle)
		// (0->3),(7->9) box 3 (up right)
		// (4->6),(0->3) ...
		// (4->6),(4->6)
		// (4->6),(7->9)
		// (7->9),(0->3)
		// (7->9),(4->6)
		// (7->9),(7->9)

		for (int row = 0 + 3 * boxIndexRow; row < 3 + 3 * boxIndexRow; row++)
		{
			for (int column = 0 + 3 * boxIndexColumn; column < 3 + 3 * boxIndexColumn; column++)
			{
				if (sudoku[row, column].Length == 1)
				{
					Console.WriteLine("Remove from Box " + boxIndexRow + ", " + boxIndexColumn + ": " + sudoku[row, column]);
					sudoku = RemoveFromBox(sudoku, boxIndexRow, boxIndexColumn, sudoku[row, column]);
				}
			}
		}
		return sudoku;
	}

	public static string[,] RemoveFromBox(string[,] sudoku, int boxIndexRow, int boxIndexColumn, string valueToDelete)
	{
		for (int row = 0 + 3 * boxIndexRow; row < 3 + 3 * boxIndexRow; row++)
		{
			for (int column = 0 + 3 * boxIndexColumn; column < 3 + 3 * boxIndexColumn; column++)
			{
				if (sudoku[row, column].Length != 1)
				{
					sudoku[row, column] = sudoku[row, column].Replace(valueToDelete, "");
				}
			}
		}
		return sudoku;
	}

	public static void Sudokuout(string[,] sudoku)
	{
		int rowLength = sudoku.GetLength(0);
		int colLength = sudoku.GetLength(1);

		for (int i = 0; i < rowLength; i++)
		{
			for (int j = 0; j < colLength; j++)
			{
				Console.Write(string.Format("{0} ", sudoku[i, j]));
			}
			Console.Write(Environment.NewLine);
		}

		Console.Write(Environment.NewLine); Console.Write(Environment.NewLine);
	}

	public static string[,] OptimzeRow(string[,] sudoku)
	{
		Console.WriteLine("Optimize Row");
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				if (sudoku[row, column].Length == 1)
				{
					for (int optimizeCol = 0; optimizeCol < 9; optimizeCol++)
					{
						if (optimizeCol != column && sudoku[row, optimizeCol].Length != 1)
						{
							sudoku[row, optimizeCol] = sudoku[row, optimizeCol].Replace(sudoku[row, column], "");
						}
					}
				}
			}
		}
		return sudoku;
	}

	public static string[,] OptimzeColumn(string[,] sudoku)
	{
		Console.WriteLine("Optimize Column");
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				if (sudoku[row, column].Length == 1)
				{
					for (int optimizeRow = 0; optimizeRow < 9; optimizeRow++)
					{
						if (optimizeRow != row && sudoku[optimizeRow, column].Length != 1)
						{
							sudoku[optimizeRow, column] = sudoku[optimizeRow, column].Replace(sudoku[row, column], "");
						}
					}
				}
			}
		}
		return sudoku;
	}
	private static string fileContent = "003400020xxx645200030xxx000038094xxx007506048xxx009000300xxx860307200xxx710960000xxx090003152xxx030002700xxx";
}