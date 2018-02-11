using System.Linq;

namespace PizzaLibrary.Validators
{
    public class PizzaValidator
    {
        public static bool PieceValidate(int row, int col, CuttedPizza cuttedPizza)
        {
            var slice = cuttedPizza.Slices.FirstOrDefault(s =>
            {
                if (s.Coords.ColumnStart > col &&
                    s.Coords.ColumnStart > col)
                    return false;
                if (s.Coords.ColumnEnd < col &&
                    s.Coords.ColumnEnd < col)
                    return false;
                if (s.Coords.RowStart > row &&
                    s.Coords.RowStart > row)
                    return false;
                if (s.Coords.RowEnd < row &&
                    s.Coords.RowEnd < row)
                    return false;
                return true;
            });
            if (slice != null) return false;
            return true;
        }
    }
}