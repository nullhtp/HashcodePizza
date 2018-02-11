using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PizzaLibrary
{
    public class Pizza
    {
        public IngridientType[][] Pieces { get; }
        public int Width { get; }
        public int Height { get; }
        public int CountTomatoes { get; protected set; }
        public int CountMushrooms { get; protected set; }
        public int CountPieces { get; protected set; }

        public IngridientType this[int col, int row] => Pieces[col][row];

        public Pizza(IngridientType[][] pieces)
        {
            Pieces = pieces;
            Width = pieces.Length;
            Height = pieces[0].Length;
            CountPieces = Width * Height;
            CountMushrooms = pieces.Sum(r => r.Count(i => i == IngridientType.Mushroom));
            CountTomatoes = pieces.Sum(r => r.Count(i => i == IngridientType.Tomatoe));
        }
    }
}