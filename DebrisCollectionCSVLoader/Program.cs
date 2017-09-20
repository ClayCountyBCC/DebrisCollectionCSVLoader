using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace DebrisCollectionCSVLoader
{
  class Program
  {
    static void Main(string[] args)
    {

      try
      {
        CopyFileFromFTP();

        string FileDate = " " + DateTime.Today.AddDays(-1).Month + "-" + DateTime.Today.AddDays(-1).Day;
        Console.WriteLine("FileDate: " + FileDate);

        List<DebrisHaulTicket> values = File.ReadAllLines($@"C:\Users\westje\Documents\Visual Studio 2017\Projects\DebrisCollectionCSVLoader\DebrisCollectionCSVLoader\DebrisCSVBackup\CLAYCloseHaulTickets{FileDate}.csv")
                                 .Skip(1)
                                 .Select(line => new DebrisHaulTicket(line))
                                 .ToList();


        var ErrorTickets = DebrisHaulTicket.InsertDataToDB(values);


        if (ErrorTickets.Count > 0)
        {
          Console.WriteLine("The following records could not be saved:\n");
          Console.WriteLine("Record\tError Message\n******\t*************");

          foreach (var et in ErrorTickets)
            Console.WriteLine(et.ToString());
        }
        else
        {
          Console.WriteLine("All Records Saved");
        }

        // Uncomment to if running manually to validate record insert errors
        //Console.ReadKey();

      }
      catch (Exception ex)
      {
        Constants.Log(ex, "Error saving records");
        return;
      }
    }



    private static bool CopyFileFromFTP()
    {

      return false;
    }



  }
}
