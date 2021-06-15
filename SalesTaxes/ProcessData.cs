using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxes
{
    public static class ProcessData
    {


        private static breakdown CalculateAmountAfterTaxes(int units, decimal Amount, decimal BaseTax, decimal ImportedTax)
        {
            breakdown out_ = new breakdown();
            decimal Percentage = (BaseTax + ImportedTax) / 100;
            out_.TaxPerUnit = Percentage > 0 ? Math.Round((Math.Round((Amount * Percentage) * 20, MidpointRounding.AwayFromZero) / 20), 2) : 0;
            out_.TotalTaxes = out_.TaxPerUnit * units;
            out_.TotalAmount = (Amount * units) + out_.TotalTaxes;
            out_.Outstr = units == 1 ? out_.TotalAmount.ToString().Trim() : out_.TotalAmount + " ( " + units + " @ " + (Amount + out_.TaxPerUnit) + " )";
            return out_;

        }
        public static string ConcatData(List<Products> Input, int BaseTax, int ImportedTax)
        {
            string Output = string.Empty;
            decimal Total = 0, taxes = 0;
            var query = Input.GroupBy(x => x)
              .Where(g => g.Count() > 0)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

            foreach (var elm in query)
            {
                var out_ = CalculateAmountAfterTaxes(elm.Counter, elm.Element.Amount, elm.Element.NoBaseTax ? 0 : BaseTax, elm.Element.Imported ? ImportedTax : 0);
                Output += elm.Element.Name + ": " + out_.Outstr + "\n";
                Total += out_.TotalAmount;
                taxes += out_.TotalTaxes;
            }
            //here we round the decimals
            Output += "Sales Taxes: " + taxes + "\n";
            Output += "Total: " + (Total);
            return Output;
        }
    }
}
    

