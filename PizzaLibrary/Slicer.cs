using System;
using System.Collections.Generic;
using System.Linq;
using PizzaLibrary.Validators;

namespace PizzaLibrary
{
    public class Slicer
    {
        private readonly int _minPieces;
        private readonly int _maxPieces;
        private readonly List<Coords> _sliceShapes = new List<Coords>();
        private readonly SliceValidator _sliceValidator;

        public Slicer(int minIngridients, int maxPieces)
        {
            _maxPieces = maxPieces;
            _minPieces = minIngridients * 2;
            InitSliceShapes(_minPieces, _maxPieces);
            _sliceValidator = new SliceValidator(minIngridients, maxPieces);
        }

        public CuttedPizza Cut(Pizza pizza)
        {
            var cuttedPizza = new CuttedPizza(pizza);
            for (var col = 0; col < pizza.Width; col++)
            {
                for (var row = 0; row < pizza.Height; row++)
                {
                    if (!PizzaValidator.PieceValidate(row, col, cuttedPizza)) continue;
                    var slices = GetAllSugestSlices(pizza, row, col);
                    var slice = ChooseSlice(slices, cuttedPizza);
                    if (slice != null)
                    {
                        cuttedPizza.AddSlice(slice);
                        row = slice.Coords.RowEnd;
                    }
                }
                var progress = (float)col / pizza.Width * 100;
                Console.WriteLine($"Pregress {progress}%");
            }
            return cuttedPizza;
        }

        private Slice ChooseSlice(IEnumerable<Slice> slices, CuttedPizza cuttedPizza)
        {
            slices = slices.Where(s => _sliceValidator.Validate(s, cuttedPizza));

            if (!slices.Any()) return null;
            var slice = slices
                .Aggregate((s1, s2) =>
                {
                    var s1DiffMushroom = cuttedPizza.CountMushrooms - s1.CountMushrooms;
                    var s1DiffTomatoe = cuttedPizza.CountTomatoes - s1.CountTomatoes;
                    if (s1DiffMushroom == 0 || s1DiffTomatoe == 0) return s2;
                    var s2DiffMushroom = cuttedPizza.CountMushrooms - s2.CountMushrooms;
                    var s2DiffTomatoe = cuttedPizza.CountTomatoes - s2.CountTomatoes;
                    if (s2DiffMushroom == 0 || s2DiffTomatoe == 0) return s1;

                    var s1Diff = s1DiffMushroom > s1DiffTomatoe
                        ? s1DiffMushroom / s1DiffTomatoe
                        : s1DiffTomatoe / s1DiffMushroom;
                    var s2Diff = s2DiffMushroom > s2DiffTomatoe
                        ? s2DiffMushroom / s2DiffTomatoe
                        : s2DiffTomatoe / s2DiffMushroom;
                    if (s1Diff > s2Diff)
                    {
                        return s2;
                    }
                    if (s1Diff == s2Diff)
                    {
                        return s1.CountPieces > s2.CountPieces ? s2 : s1;
                    }
                    return s1;
                });
            return slice;
        }

        private IEnumerable<Slice> GetAllSugestSlices(Pizza pizza, int pizzaRowStart, int pizzaColStart)
        {
            var result = new List<Slice>();
            foreach (var sliceShape in _sliceShapes)
            {
                // Check size
                if (pizzaRowStart + sliceShape.RowEnd > pizza.Height ||
                    pizzaColStart + sliceShape.ColumnEnd > pizza.Width)
                {
                    continue;
                }

                var sliceIngridients = new IngridientType[sliceShape.ColumnEnd][];
                for (var col = 0; col < sliceShape.ColumnEnd; col++)
                {
                    sliceIngridients[col] = new IngridientType[sliceShape.RowEnd];
                    for (var row = 0; row < sliceShape.RowEnd; row++)
                    {
                        sliceIngridients[col][row] = pizza[col + pizzaColStart, row + pizzaRowStart];
                    }
                }
                var sliceCoord = new Coords
                {
                    ColumnStart = pizzaColStart,
                    ColumnEnd = pizzaColStart + sliceShape.ColumnEnd - 1,
                    RowStart = pizzaRowStart,
                    RowEnd = pizzaRowStart + sliceShape.RowEnd - 1
                };
                result.Add(new Slice(sliceIngridients, sliceCoord));
            }
            return result;
        }

        private void InitSliceShapes(int min, int max)
        {
            for (var i = min; i <= max; i++)
            {
                _sliceShapes.AddRange(FindSliceShapes(i));
            }
        }

        private IEnumerable<Coords> FindSliceShapes(int size)
        {
            var result = new List<Coords>();
            for (var i = 1; i <= size; i++)
            {
                if (size % i == 0)
                {
                    result.Add(new Coords
                    {
                        RowStart = 0,
                        ColumnStart = 0,
                        RowEnd = i,
                        ColumnEnd = size / i
                    });
                }
            }
            return result;
        }
    }
}