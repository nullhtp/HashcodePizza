using System.Collections.Generic;
using System.Linq;

namespace PizzaLibrary
{
    public class CuttedPizza : Pizza
    {
        public ICollection<Slice> Slices { get; }
        public int Pieces => Slices.Sum(s => s.CountPieces);

        public CuttedPizza(Pizza pizza) : base(pizza.Pieces)
        {
            Slices = new List<Slice>();
        }
        
        public void AddSlice(Slice slice)
        {
            CountMushrooms -= slice.CountMushrooms;
            CountTomatoes -= slice.CountTomatoes;
            CountPieces -= slice.CountPieces;
            Slices.Add(slice);
        }


    }
}