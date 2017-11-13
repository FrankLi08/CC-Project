using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TheDream.DAL.Extension;
using TheDream.DAL.Model;

namespace TheDream.API.Models
{
    public class SQL
    {
        public string ConnString;
        public SQL()
        {
            ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["Cloud_ChefEntities"].ConnectionString;
        }
        /// <summary>
        /// Get Vegetable flavour from dbo.GetVegFlavour SPROC
        /// </summary>
        /// <param name="VegetableList"></param>
        public List<VegFlavour> GetVegetableFlavour(List<string> VegetableList)
        {
            if (VegetableList == null || VegetableList.Count == 0)
            {
                throw new ArgumentNullException("VegetableList");
            }
            List<VegFlavour> results = new List<VegFlavour>();
            using (var context = new SqlConnection(this.ConnString))
            {
                using (var cmd = new SqlCommand("dbo.GetVegFlavour", context))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var Vegs = new DataTable();
                    Vegs.Columns.Add("VegCatagorys", typeof(int));
                    foreach (var veg in VegetableList)
                    {
                        Vegs.Rows.Add(veg);
                    }
                    cmd.Parameters.AddTVPParameter("@VegCatagorys", "dbo.VegCatagoryVPT", Vegs);
                    var returnParam = cmd.Parameters.AddReturnCodeParameter();
                    context.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                results.Add(
                                    new VegFlavour()
                                    {
                                        VegId = reader.GetValue<int>("VegId"),
                                        VegName = reader.GetValue<string>("VegName"),
                                        FlavourName = reader.GetValue<string>("FlavourName")
                                    });
                            }
                        }
                    }

                    var rc = Convert.ToInt32(returnParam.Value);

                    if (rc != 0)
                    {
                        switch (rc)
                        {
                            case -1:
                                throw new ArgumentOutOfRangeException("requestId");
                        }
                    }
                    return results;

                }
            }
        }
    }
}