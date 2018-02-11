using System.Linq;

namespace PizzaLibrary.Validators
{
    public class SliceValidator
    {
        private readonly int _minIngridients;
        private readonly int _maxPieces;

        public SliceValidator(int minIngridients, int maxPieces)
        {
            _minIngridients = minIngridients;
            _maxPieces = maxPieces;
        }

        public bool Validate(Slice slice, CuttedPizza cuttedPizza)
        {
            if(!ValidateSliceRules(slice)) return false;
            if(!ValidatePizzaRules(slice, cuttedPizza)) return false;
            return true;
        }

        private static bool ValidatePizzaRules(Slice slice, CuttedPizza cuttedPizza)
        {
            var slices = cuttedPizza.Slices.Where(s =>
                s.Coords.ColumnEnd >= slice.Coords.ColumnStart &&
                s.Coords.RowEnd >= slice.Coords.RowStart 
            );
            
            var sliceUnvalid = slices.FirstOrDefault(s =>
            {
                if (s.Coords.ColumnStart > slice.Coords.ColumnStart &&
                    s.Coords.ColumnStart > slice.Coords.ColumnEnd)
                    return false;
                if (s.Coords.ColumnEnd < slice.Coords.ColumnStart &&
                    s.Coords.ColumnEnd < slice.Coords.ColumnEnd)
                    return false;
                if (s.Coords.RowStart > slice.Coords.RowStart &&
                    s.Coords.RowStart > slice.Coords.RowEnd)
                    return false;
                if (s.Coords.RowEnd < slice.Coords.RowStart &&
                    s.Coords.RowEnd < slice.Coords.RowEnd)
                    return false;
                return true;
            });
            if (sliceUnvalid != null) return false;
            return true;
        }

        private bool ValidateSliceRules(Slice slice)
        {
            if (slice.CountMushrooms < _minIngridients ||
                slice.CountTomatoes < _minIngridients)
            {
                return false;
            }
            if (slice.CountPieces > _maxPieces)
            {
                return false;
            }
            return true;
        }
    }
}