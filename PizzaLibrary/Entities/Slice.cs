using System.Linq;

namespace PizzaLibrary
{
    public class Slice
    {
        public int CountTomatoes { get; }
        public int CountMushrooms { get; }
        public IngridientType [][] Pieces { get; }
        public int CountPieces => Pieces.Length * Pieces[0].Length;
        public Coords Coords { get; }

        public Slice(IngridientType[][] pieces, Coords coords)
        {
            Pieces = pieces;
            CountMushrooms = pieces.Sum(i=>i.Count(p=>p == IngridientType.Mushroom));
            CountTomatoes = pieces.Sum(i=>i.Count(p=>p == IngridientType.Tomatoe));
            Coords = coords;
        }
    }
}