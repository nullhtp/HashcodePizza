using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaLibrary
{
    public static class PizzaHelper
    {
        public static void SaveSlicesToFile(IEnumerable<Slice> slices, string filename)
        {
            var str = $"{slices.Count()}\n";
            str = slices.Aggregate(str,
                (current, slice) =>
                    current +
                    $"{slice.Coords.RowStart} {slice.Coords.ColumnStart} {slice.Coords.RowEnd} {slice.Coords.ColumnEnd}\n");
            File.WriteAllText(filename, str);
        }

        public static PizzaFileData LoadPizzaFromFile(string filename)
        {
            var strings = File.ReadAllLines(filename);
            var conditions = strings[0].Split(' ');
            var rows = int.Parse(conditions[0]);
            var cols = int.Parse(conditions[1]);
            var minIngridients = int.Parse(conditions[2]);
            var maxPieces = int.Parse(conditions[3]);

            var ingridients = CreateIngridients(cols, rows);
            FillIngridienstFromString(ingridients, strings.Skip(1).ToArray());

            var pizza = new Pizza(ingridients);

            return new PizzaFileData {MaxPieces = maxPieces, MinIngridients = minIngridients, Pizza = pizza};
        }

        private static void FillIngridienstFromString(IngridientType[][] ingridients, string[] data)
        {
            for (var rowIndex = 0; rowIndex < data.Length; rowIndex++)
            {
                var ingridientChar = data[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < ingridientChar.Length; colIndex++)
                {
                    if ('T' == ingridientChar[colIndex])
                    {
                        ingridients[colIndex][rowIndex] = IngridientType.Tomatoe;
                    }
                    else if ('M' == ingridientChar[colIndex])
                    {
                        ingridients[colIndex][rowIndex] = IngridientType.Mushroom;
                    }
                    else
                    {
                        throw new Exception("Wrong file format!");
                    }
                }
            }
        }

        public static IngridientType[][] CreateIngridients(int cols, int rows)
        {
            var ingridients = new IngridientType[cols][];
            for (var index = 0; index < cols; index++)
            {
                ingridients[index] = new IngridientType[rows];
            }
            return ingridients;
        }
    }
}