using PizzaLibrary;

namespace HashcodePizza
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzaData = PizzaHelper.LoadPizzaFromFile("Data/big.in");
            var slicer = new Slicer(pizzaData.MinIngridients, pizzaData.MaxPieces);
            var cuttedPizza = slicer.Cut(pizzaData.Pizza);

            PizzaHelper.SaveSlicesToFile(cuttedPizza.Slices,"big.out");
            
            System.Console.WriteLine(cuttedPizza.Slices.Count);
            foreach (var slice in cuttedPizza.Slices)
            {
                System.Console.WriteLine(
                    $"{slice.Coords.RowStart} {slice.Coords.ColumnStart} {slice.Coords.RowEnd} {slice.Coords.ColumnEnd}");
            }
        }
    }
}