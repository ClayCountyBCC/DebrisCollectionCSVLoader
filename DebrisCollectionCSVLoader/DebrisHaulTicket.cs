using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DebrisCollectionCSVLoader
{
  public class DebrisHaulTicket
  {
    public string HaulTicketBarcode { get; set; } = "" ;
    public string TruckId { get; set; } = "";
    public string DebrisType { get; set; } = "";
    public string Latitude { get; set; } = "";
    public string Longitude { get; set; } = "";
    public string Sector { get; set; } = "";
    public string Zone { get; set; } = "";
    public string PropertyType { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string CreateDate { get; set; } = "";

    public DebrisHaulTicket(string line)
    {
      string[] values = line.Split(',');
      HaulTicketBarcode = Convert.ToString(values[4]);
      TruckId = Convert.ToString(values[7]);
      DebrisType = Convert.ToString(values[5]);
      Latitude = Convert.ToString(values[0]);
      Longitude = Convert.ToString(values[1]);
      Sector = Convert.ToString(values[8]);
      Zone = Convert.ToString(values[9]);
      PropertyType = Convert.ToString(values[6]);
      CreatedBy = Convert.ToString(values[2]);
      CreateDate = Convert.ToString(values[3]);

    }

    public static List<string> InsertDataToDB(List<DebrisHaulTicket> tickets)
    {
      var ErrorTickets = new List<string>();
      int record = 1;
      foreach (DebrisHaulTicket t in tickets)
      {
        var dbArgs = new Dapper.DynamicParameters();
        dbArgs.Add("@HaulTicketBarcode", t.HaulTicketBarcode.Trim());
        dbArgs.Add("@TruckId", t.TruckId.Trim());
        dbArgs.Add("@DebrisType", t.DebrisType.Trim());
        dbArgs.Add("@Latitude", t.Latitude.Trim());
        dbArgs.Add("@Longitude", t.Longitude.Trim());
        dbArgs.Add("@Sector", t.Sector.Trim());
        dbArgs.Add("@Zone", t.Zone.Trim());
        dbArgs.Add("@PropertyType", t.PropertyType.Trim() );
        dbArgs.Add("@CreatedBy", t.CreatedBy.Trim());
        dbArgs.Add("@CreateDate", t.CreateDate.Trim());

        // This will probably be better if using a merge insert only. 

        string sql = $@"
          USE DebrisCollectionTest;      
          insert into DebrisCollection
              (HaulTicketBarcode,
              TruckId,
              PropertyType,
              DebrisType,
              Latitude,
              Longitude,
              Sector,
              Zone,
              CreatedBy,
              CreateDate)
          VALUES
              (@HaulTicketBarcode,
              @TruckId,
              @PropertyType,
              @DebrisType,
              CAST(@Latitude as FLOAT),
              CAST(@Longitude as FLOAT),
              @Sector,
              @Zone,
              @CreatedBy,
              @CreateDate)";

        try
        {
          if (Constants.Exec_Query_Insert(sql, dbArgs) < 0)
          {
            ErrorTickets.Add( record.ToString().PadLeft(5) + "\tHaulTicketBarcode " + t.HaulTicketBarcode + " already exists.");
          }
          record++;
        }
        catch (Exception ex)
        {
          Constants.Log(ex, sql);
        }
      }
      return ErrorTickets;
    }


  }

  
}
