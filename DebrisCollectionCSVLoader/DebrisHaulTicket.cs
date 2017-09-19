using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DebrisCollectionCSVLoader
{
  class DebrisHaulTicket
  {
    public static string HaulTicketBarcode { get; set; } = "" ;
    public static string TruckId { get; set; } = "";
    public static string DebrisType { get; set; } = "";
    public static string Latitude { get; set; } = "";
    public static string Longitude { get; set; } = "";
    public static string Sector { get; set; } = "";
    public static string Zone { get; set; } = "";
    public static string PropertyType { get; set; } = "";
    public static string CreatedBy { get; set; } = "";
    public static string CreateDate { get; set; } = "";

    public DebrisHaulTicket()
    {

    }


    public static DebrisHaulTicket FromCsv(string csvLine)
    {
      string[] values = csvLine.Split(',');
      DebrisHaulTicket debrisHaulTicket = new DebrisHaulTicket();
      DebrisHaulTicket.HaulTicketBarcode = Convert.ToString(values[4]);
      DebrisHaulTicket.TruckId =  Convert.ToString(values[7]);
      DebrisHaulTicket.DebrisType =  Convert.ToString(values[5]);
      DebrisHaulTicket.Latitude = Convert.ToString(values[0]);
      DebrisHaulTicket.Longitude = Convert.ToString(values[1]);
      DebrisHaulTicket.Sector = Convert.ToString(values[8]);
      DebrisHaulTicket.Zone = Convert.ToString(values[9]);
      DebrisHaulTicket.PropertyType = Convert.ToString(values[6]);
      DebrisHaulTicket.CreatedBy = Convert.ToString(values[2]);
      DebrisHaulTicket.CreateDate = Convert.ToString(values[3]);

      return debrisHaulTicket;

    }


  }
}
