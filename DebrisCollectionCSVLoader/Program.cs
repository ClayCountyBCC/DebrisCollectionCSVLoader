using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebrisCollectionCSVLoader
{
  class Program
  {
    static void Main(string[] args)
    {
      List<DebrisHaulTicket> values = File.ReadAllLines(@"C:\Users\westje\Documents\Visual Studio 2017\Projects\DebrisCollectionCSVLoader\DebrisCollectionCSVLoader\Debris_Haul_Teckets_Test.csv")
                               .Skip(1)
                               .Select(line => DebrisHaulTicket.FromCsv(line))
                               .ToList();

      foreach (var value in values)
        Console.WriteLine(value);

      Console.ReadKey();
    }



  }
}
