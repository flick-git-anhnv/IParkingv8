using iParkingv8.Object.Enums.Bases;
using IParkingv8.Printer.BaoSon;
using IParkingv8.Printer.DefaultPrinters;
using IParkingv8.Printer.GoldenWestLake;
using IParkingv8.Printer.Maxcom;
using IParkingv8.Printer.OfficeHaus;
using IParkingv8.Printer.SeaStarsHotel;

namespace IParkingv8.Printer
{
    public static class PrinterFactory
    {
        public static IPrinter CreatePrinter(EmPrintTemplate emPrintTemplate)
        {
            switch (emPrintTemplate)
            {
                case EmPrintTemplate.BaseTemplate:
                    return new DefaultPrinter();
                case EmPrintTemplate.OfficeHaus:
                    return new OfficeHausPrinter();
                case EmPrintTemplate.Seastars_Hotel:
                    return new SeaStarsHotelPrinter();
                case EmPrintTemplate.BaoSon:
                    return new BaoSonPrinter();
                case EmPrintTemplate.MAXCOM:
                    return new MaxcomPrinter();
                case EmPrintTemplate.GoldenWestlake:
                    return new GoldenWestLakePrinter();
                default:
                    break;
            }
            return new DefaultPrinter();
        }
    }
}
