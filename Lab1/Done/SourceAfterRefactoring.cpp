#include <iostream>
#include <cstdlib>
#include <ctime>
using std::cout;
using std::cin;
using std::endl;
using std::string;

class Matrix
{
public:
	int rows;
	int columns;
	int** myMatrix;

	Matrix()
	{
		rows = 1;
		columns = 1;
		myMatrix = new int*[rows];
		for (int i = 0; i < rows; i++)
		{
			myMatrix[i] = new int[columns];
			for (int j = 0; j < columns; j++)
				myMatrix[i][j] = 0;
		}
	}

	Matrix(int _rows, int _columns)
	{
		rows = _rows;
		columns = _columns;
		myMatrix = new int*[rows];
		for (int i = 0; i < rows; i++)
			myMatrix[i] = new int[columns];
	}

	void randomInit()
	{
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++)
				myMatrix[i][j] = rand() % 10;
	}

	void keyboardInit()
	{
		for (int i = 0; i < rows; i++)
			for (int j = 0; j < columns; j++)
				cin >> myMatrix[i][j];
	}

	void matrixByMatrixInit(int** _matrix, int size, int rowsPlus, int columnPlus)
	{
		for (int i = 0; i < size; i++)
			for (int j = 0; j < size; j++)
				myMatrix[i][j] = _matrix[i + rowsPlus][j + columnPlus];
	}

	void resizeMatrix(int _rows, int _columns)
	{
		int** newMatrix = new int*[_rows];
		for (int i = 0; i < _rows; i++)
			newMatrix[i] = new int[_columns];
		for (int i = 0; i < _rows; i++)
			for (int j = 0; j < _columns; j++)
				if (i < rows && j < columns)
					newMatrix[i][j] = myMatrix[i][j];
				else
					newMatrix[i][j] = 0;

		for (int i = 0; i < rows; i++)
			delete[] myMatrix[i];
		delete[] myMatrix;
		myMatrix = newMatrix;
		rows = _rows;
		columns = _columns;
	}

	void show()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
				cout << myMatrix[i][j] << " ";
			cout << endl;
		}
	}

	~Matrix()
	{
		for (int i = 0; i < rows; i++)
			delete[] myMatrix[i];
		delete[] myMatrix;
	}
};

void getSize(int& rows, int& columns, string message)
{
	do
	{
		cout << "Введите размеры " << message << " матрицы\n";
		cin >> rows >> columns;
	} while (rows <= 0 || columns <= 0);
}

int main()
{
	srand(time(NULL));
	int rows, columns, choice, currentSize = 2;
	system("chcp 1251");
	cout << "Вас приветствует программа" << endl << "быстрого перемножения матриц методом Штрассена\n\n";

	//Ввод размеров матрицы пользователем

	getSize(rows, columns, "первой");
	Matrix firstMatrix(rows, columns);
	getSize(rows, columns, "второй");
	Matrix secondMatrix(rows, columns);

	//Выбор способа заполнения и заполнение матриц

	do
	{
		cout << "Выберите способ заполнения матриц\n" <<
			"1 - Вручную \n2 - Случайным образом\n";
		cin >> choice;
	} while (choice < 1 || choice > 2);
	switch (choice)
	{
	case 1:
		cout << endl;
		firstMatrix.keyboardInit();
		cout << endl;
		secondMatrix.keyboardInit();
		cout << "\nМатрица 1\n\n";
		firstMatrix.show();
		cout << "\nМатрица 2\n\n";
		secondMatrix.show();
		break;
	case 2:
		firstMatrix.randomInit();
		secondMatrix.randomInit();
		cout << "\nМатрица 1\n\n";
		firstMatrix.show();
		cout << "\nМатрица 2\n\n";
		secondMatrix.show();
		break;
	}

	//Приведение матриц к требуемому размеру

	while (currentSize < firstMatrix.rows || currentSize < secondMatrix.rows || currentSize < firstMatrix.columns || currentSize < secondMatrix.columns)
		currentSize *= 2;
	firstMatrix.resizeMatrix(currentSize, currentSize);
	secondMatrix.resizeMatrix(currentSize, currentSize);

	cout << "Приведенные матрицы\n";
	cout << "\Матрица 1\n\n";
	firstMatrix.show();
	cout << "\Матрица 2\n\n";
	secondMatrix.show();

	//Разбиение матриц на подматрицы и их заполнение

	Matrix* partsOfFirstMatrix = new Matrix[4];
	for (int i = 0; i < 4; i++)
		partsOfFirstMatrix[i].resizeMatrix(currentSize / 2, currentSize / 2);

	partsOfFirstMatrix[0].matrixByMatrixInit(firstMatrix.myMatrix, currentSize / 2, 0, 0);
	partsOfFirstMatrix[1].matrixByMatrixInit(firstMatrix.myMatrix, currentSize / 2, 0, currentSize / 2);
	partsOfFirstMatrix[2].matrixByMatrixInit(firstMatrix.myMatrix, currentSize / 2, currentSize / 2, 0);
	partsOfFirstMatrix[3].matrixByMatrixInit(firstMatrix.myMatrix, currentSize / 2, currentSize / 2, currentSize / 2);

	Matrix* partsOfSecondMatrix = new Matrix[4];
	for (int i = 0; i < 4; i++)
		partsOfSecondMatrix[i].resizeMatrix(currentSize / 2, currentSize / 2);

	partsOfSecondMatrix[0].matrixByMatrixInit(secondMatrix.myMatrix, currentSize / 2, 0, 0);
	partsOfSecondMatrix[1].matrixByMatrixInit(secondMatrix.myMatrix, currentSize / 2, 0, currentSize / 2);
	partsOfSecondMatrix[2].matrixByMatrixInit(secondMatrix.myMatrix, currentSize / 2, currentSize / 2, 0);
	partsOfSecondMatrix[3].matrixByMatrixInit(secondMatrix.myMatrix, currentSize / 2, currentSize / 2, currentSize / 2);

	//Создание промежуточных матриц

	Matrix* intermediateMatrix = new Matrix[7];
	for (int i = 0; i < 7; i++)
		intermediateMatrix[i].resizeMatrix(currentSize / 2, currentSize / 2);

	//Вычисление значений промежуточных матриц

	for (int i = 0; i < currentSize / 2; i++)
	{
		for (int j = 0; j < currentSize / 2; j++)
		{
			for (int k = 0; k < 7; k++)
				intermediateMatrix[k].myMatrix[i][j] = 0;

			for (int z = 0; z < currentSize / 2; z++)
			{
				intermediateMatrix[0].myMatrix[i][j] += (partsOfFirstMatrix[0].myMatrix[i][z] + partsOfFirstMatrix[3].myMatrix[i][z])
					* (partsOfSecondMatrix[0].myMatrix[z][j] + partsOfSecondMatrix[3].myMatrix[z][j]);

				intermediateMatrix[1].myMatrix[i][j] += (partsOfFirstMatrix[2].myMatrix[i][z] + partsOfFirstMatrix[3].myMatrix[i][z])
					* partsOfSecondMatrix[0].myMatrix[z][j];

				intermediateMatrix[2].myMatrix[i][j] += partsOfFirstMatrix[0].myMatrix[i][z]
					* (partsOfSecondMatrix[1].myMatrix[z][j] - partsOfSecondMatrix[3].myMatrix[z][j]);

				intermediateMatrix[3].myMatrix[i][j] += partsOfFirstMatrix[3].myMatrix[i][z]
					* (partsOfSecondMatrix[2].myMatrix[z][j] - partsOfSecondMatrix[0].myMatrix[z][j]);

				intermediateMatrix[4].myMatrix[i][j] += (partsOfFirstMatrix[0].myMatrix[i][z] + partsOfFirstMatrix[1].myMatrix[i][z])
					* partsOfSecondMatrix[3].myMatrix[z][j];

				intermediateMatrix[5].myMatrix[i][j] += (partsOfFirstMatrix[2].myMatrix[i][z] - partsOfFirstMatrix[0].myMatrix[i][z])
					* (partsOfSecondMatrix[0].myMatrix[z][j] + partsOfSecondMatrix[1].myMatrix[z][j]);

				intermediateMatrix[6].myMatrix[i][j] += (partsOfFirstMatrix[1].myMatrix[i][z] - partsOfFirstMatrix[3].myMatrix[i][z])
					* (partsOfSecondMatrix[2].myMatrix[z][j] + partsOfSecondMatrix[3].myMatrix[z][j]);
			}
		}
	}

	//Создание вспомогательных матриц

	Matrix* auxiliaryMatrix = new Matrix[4];
	for (int i = 0; i < 4; i++)
		auxiliaryMatrix[i].resizeMatrix(currentSize / 2, currentSize / 2);

	//Подсчет значений вспомогательных матриц из промежуточных

	for (int i = 0; i < currentSize / 2; i++)
	{
		for (int j = 0; j < currentSize / 2; j++)
		{
			auxiliaryMatrix[0].myMatrix[i][j] = intermediateMatrix[0].myMatrix[i][j] + intermediateMatrix[3].myMatrix[i][j]
				- intermediateMatrix[4].myMatrix[i][j] + intermediateMatrix[6].myMatrix[i][j];
			auxiliaryMatrix[1].myMatrix[i][j] = intermediateMatrix[2].myMatrix[i][j] + intermediateMatrix[4].myMatrix[i][j];
			auxiliaryMatrix[2].myMatrix[i][j] = intermediateMatrix[1].myMatrix[i][j] + intermediateMatrix[3].myMatrix[i][j];
			auxiliaryMatrix[3].myMatrix[i][j] = intermediateMatrix[0].myMatrix[i][j] - intermediateMatrix[1].myMatrix[i][j]
				+ intermediateMatrix[2].myMatrix[i][j] + intermediateMatrix[5].myMatrix[i][j];
		}
	}

	//Создание результирующей матрицы

	Matrix resultMatrix(currentSize, currentSize);

	//Занесение информации из вспомогательных матриц в результирующую

	for (int i = 0; i < currentSize / 2; i++)
	{
		for (int j = 0; j < currentSize / 2; j++)
		{
			resultMatrix.myMatrix[i][j] = auxiliaryMatrix[0].myMatrix[i][j];
			resultMatrix.myMatrix[i][j + currentSize / 2] = auxiliaryMatrix[1].myMatrix[i][j];
			resultMatrix.myMatrix[i + currentSize / 2][j] = auxiliaryMatrix[2].myMatrix[i][j];
			resultMatrix.myMatrix[i + currentSize / 2][j + currentSize / 2] = auxiliaryMatrix[3].myMatrix[i][j];
		}
	}

	//Выравнивание границ результирующей матрицы

	int flag = 0, resultRows = 100, resultColumns = 100;
	for (int i = 0; i < currentSize; i++)
	{
		flag = 0;
		for (int j = 0; j < currentSize; j++)
		{
			if (resultMatrix.myMatrix[i][j] != 0)
			{
				flag++;
				resultRows = 100;
			}
		}
		if (flag == 0 && i < resultRows)
		{
			resultRows = i;
		}
	}

	for (int i = 0; i < currentSize; i++)
	{
		flag = 0;
		for (int j = 0; j < currentSize; j++)
		{
			if (resultMatrix.myMatrix[j][i] != 0)
			{
				flag++;
				resultColumns = 100;
			}
		}
		if (flag == 0 && i < resultColumns)
		{
			resultColumns = i;
		}
	}

	resultMatrix.resizeMatrix(resultRows, resultColumns);

	//Вывод результирующей матрицы

	cout << "\nРезультирующая матрица\n\n";
	resultMatrix.show();

	system("pause");
	return 0;
}
