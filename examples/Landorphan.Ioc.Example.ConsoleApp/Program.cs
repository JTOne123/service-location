using System;

namespace Landorphan.Ioc.Example.ConsoleApp
{
   using System.Globalization;
   using System.Linq;
   using System.Runtime.CompilerServices;
   using Landorphan.Ioc.ServiceLocation;
   using Landorphan.Ioc.ServiceLocation.Interfaces;

   class Program
   {
      private string[] args;

      public Program(string[] args)
      {
         this.args = args;
      }

      public void ShowUsage(string error)
      {
         Console.WriteLine(error);
         Console.WriteLine();
         Console.WriteLine("dotnet Landorphan.Ioc.Example.ConsoleApp {command}:");
         Console.WriteLine();
         Console.WriteLine("command:");
         Console.WriteLine("list");
         Console.WriteLine("   shows the available currency codes.");
         Console.WriteLine();
         Console.WriteLine("convert {fromCurrency: currency code} {amount: decimal} {toCurrency: currency code}");
         Console.WriteLine("   converts the {amount} from the {fromCurrency} code to the {toCurrency} code");
         Console.WriteLine("   use the list command to get the available currency codes");
         Console.WriteLine();
      }

      public IConsole Console => IocServiceLocator.Resolve<IConsole>();

      public void Run()
      {
         if (args.Length == 0)
         {
            ShowUsage("Insufficient arguments (no command specified)");
            return;
         }
         switch (args[0])
         {
            case "list":
               this.HandleListRequest();
               break;
            case "convert":
               this.HandleConvertRequest();
               break;
         }
      }

      public void HandleListRequest()
      {
         Console.WriteLine();
         if (args.Length > 1)
         {
            ShowUsage($"unknown command argument {args[1]}");
         }
         else
         {
            var converter = IocServiceLocator.Resolve<ICurrencyConverter>();
            var currencies = converter.GetSupportedCurrencies();
            foreach (var currency in currencies)
            {
               Console.WriteLine(currency);
            }

            Console.WriteLine();
         }
      }

      public void HandleConvertRequest()
      {
         if (args.Length < 4)
         {
            ShowUsage("insufficient command arguments:");
         }
         else if (args.Length > 4)
         {
            ShowUsage($"unknown command argument: {args[4]}");
         }
         else
         {
            var fromCurrency = args[1];
            var amountAsString = args[2];
            var toCurrency = args[3];
            if (!decimal.TryParse(amountAsString, out var ammount))
            {
               ShowUsage($"unable to convert the amount supplied {amountAsString}");
               return;
            }

            var converter = IocServiceLocator.Resolve<ICurrencyConverter>();
            var codes = converter.GetSupportedCurrencies();
            if (!codes.Contains(fromCurrency))
            {
               ShowUsage($"supplied fromCurrency not in the list of available codes {fromCurrency}");
               return;
            }
            if (!codes.Contains(toCurrency))
            {
               ShowUsage($"supplied toCurrency not in the list of available codes {toCurrency}");
            }

            var converted = converter.Convert(toCurrency, ammount, fromCurrency);
            Console.WriteLine(converted.ToString(CultureInfo.InvariantCulture));
         }
      }

      static void Main(string[] args)
      {
         // Register services local to this application... By Default this is not tested.
         IocServiceLocator.AmbientContainer.Registrar.RegisterImplementation<IConsole, ApplicationConsole>();
         // Transfer control to the application handler class..
         // This simulates how a windows application would hand control over the windowing subsystem
         // while also not providing a bad pattern to follow for console applications.
         var app = new Program(args);
         app.Run();
      }
   }
}
