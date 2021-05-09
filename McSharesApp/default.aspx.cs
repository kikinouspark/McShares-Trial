using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Net;
using System.Web.Services.Description;
using System.Text;
using System.Web.UI.WebControls;

namespace McSharesApp
{
    public partial class _default : System.Web.UI.Page
    {
        String filename;
        Boolean validFile = true;

        public double Balance = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
            if (IsPostBack)
            {
            }
        }

        protected void BtnValidate_Click(object sender, EventArgs e)
        {
            string xsdPath = Server.MapPath($"~/XmlData/Shares3.xsd");
            String path = Server.MapPath("~/XML/");
            String[] allowedExtension = { ".xml" };
            String fileExtension = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();

            if (fileExtension == allowedExtension[0])
            {
                fileUpload.PostedFile.SaveAs(path + fileUpload.FileName);
                LblStatus.Text = "Upload successful";
                filename = path + fileUpload.FileName;
            }
            else
            {
                LblStatus.Text = "File extension " + fileExtension + " is not allowed.";
            }

            ValidateSchema(filename, xsdPath);

            if (false)
            {
                Response.Write(HttpStatusCode.BadRequest.ToString());
                Response.Write("Invalid File Error Please load page again");
            }
            else
            {
                Response.Write(HttpStatusCode.Accepted.ToString());
                ReadFile(filename);
                BindData();
            }
        }


        public bool ValidateSchema(string xmlPath, string xsdPath)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);

            xml.Schemas.Add(null, xsdPath);

            try
            {
                xml.Validate(null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }
            return true;
        }



        private void ReadFile(string filename)
        {
            string docRef = null;
            string customerID = null;
            string cType = null;
            string DOB = null;
            string dateIncorp = null;
            string regNo = null;
            string address1 = null;
            string address2 = null;
            string town_city = null;
            string country = null;
            string name = null;
            string contactNo = null;
            int numShares = 0;
            double sharePrice = 0;
            double balance = 0;

            XmlReader xmlFile = XmlReader.Create(filename, new XmlReaderSettings());

            XDocument xmlDoc = XDocument.Load(filename);


            var doc = xmlDoc.Descendants("Doc_Ref");
            foreach (var d in doc)
            {
                if (d.Value != null)
                {
                    docRef = d.Value;
                }
                else
                {
                }
            }

            foreach (XElement xe in xmlDoc.Descendants("DataItem_Customer"))
            {
                if (xe.Element("customer_id").Value != null)
                {
                    customerID = xe.Element("customer_id").Value;
                }

                if (xe.Element("Customer_Type").Value != null)
                {
                    cType = xe.Element("Customer_Type").Value;
                }

                if (xe.Element("Date_Of_Birth").Value != null)
                {
                    DOB = xe.Element("Date_Of_Birth").Value;
                }

                if (xe.Element("Date_Incorp").Value != null)
                {
                    dateIncorp = xe.Element("Date_Incorp").Value;
                    //Response.Write(xe.Element("Date_Incorp").Value);
                }

                if (xe.Element("REGISTRATION_NO") != null)
                {
                    {
                        regNo = xe.Element("REGISTRATION_NO").Value;
                    }
                }

                if (xe.Element("Registration_No") != null)
                {
                    if (xe.Element("Registration_No").Value != null)
                    {
                        regNo = xe.Element("Registration_No").Value;
                    }
                }

                //Mailing Address
                if (xe.Element("Mailing_Address").Element("Address_Line1").Value != null)
                {
                    address1 = xe.Element("Mailing_Address").Element("Address_Line1").Value;
                }

                if (xe.Element("Mailing_Address").Element("Address_Line2").Value != null)
                {
                    address2 = xe.Element("Mailing_Address").Element("Address_Line2").Value;
                }

                if (xe.Element("Mailing_Address").Element("Town_City").Value != null)
                {
                    town_city = xe.Element("Mailing_Address").Element("Town_City").Value;
                }

                if (xe.Element("Mailing_Address").Element("Country").Value != null)
                {
                    country = xe.Element("Mailing_Address").Element("Country").Value;
                }

                //Contact Details
                if (xe.Element("Contact_Details").Element("Contact_Name").Value != null)
                {
                    name = xe.Element("Contact_Details").Element("Contact_Name").Value;
                }

                if (xe.Element("Contact_Details").Element("Contact_Number").Value != null)
                {
                    contactNo = xe.Element("Contact_Details").Element("Contact_Number").Value;
                }

                //Shares Details 
                if (xe.Element("Shares_Details").Element("Num_Shares").Value != null)
                {
                    numShares = Convert.ToInt32(xe.Element("Shares_Details").Element("Num_Shares").Value);
                }

                if (xe.Element("Shares_Details").Element("Share_Price").Value != null)
                {
                    sharePrice = Convert.ToDouble(xe.Element("Shares_Details").Element("Share_Price").Value);
                }

                balance = numShares * sharePrice;


                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("insertCustomer", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@docRef", docRef);
                        command.Parameters.AddWithValue("@customerId", customerID);
                        command.Parameters.AddWithValue("@type", cType);
                        command.Parameters.AddWithValue("@dob", DOB);
                        command.Parameters.AddWithValue("@dateIncorp", dateIncorp);
                        command.Parameters.AddWithValue("@regNo", regNo);
                        command.Parameters.AddWithValue("@address1", address1);
                        command.Parameters.AddWithValue("@address2", address2);
                        command.Parameters.AddWithValue("@town_city", town_city);
                        command.Parameters.AddWithValue("@country", country);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@contactNo", contactNo);
                        command.Parameters.AddWithValue("@numShares", numShares);
                        command.Parameters.AddWithValue("@sharePrice", sharePrice);
                        command.Parameters.AddWithValue("@balance", balance);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            Console.WriteLine("Error inserting data into Database!");
                    }
                    connection.Close();
                }
            }

        }


        private void BindData()
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                String query = "SELECT * from dbo.tblCustomer";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                customerDL.DataSource = dt;
                customerDL.DataBind();

                connection.Close();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
                conn.Open();
            //SqlCommand cmd = new SqlCommand("UPDATE * FROM tblCustomer WHERE Name like %'@SearchByname'%", conn);
            try
            {
            }
            catch (Exception ex)
            {
            }
        }

        protected void SearchByTagButton_Click(object sender, EventArgs e)
        {
            string searchkey = SearchByname.Text.Trim();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                conn.Open();
                try
                {


                    String query = "SELECT [RecordNo],[DocRef],[CustomerId],[Type],[DOB],[DateIncorp],[RegNo],[Address1],[Address2],[Town_City],[Country],[Name],[ContactNo],[NumShares],[SharePrice],[Balance] FROM[dbo].[tblCustomer] where name like '%" + searchkey + "%'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sda.Fill(dt);
                    customerSRCDL.DataSource = dt;
                    customerSRCDL.DataBind();

                    conn.Close();

                    //SqlParameter search = new SqlParameter();
                    //search.ParameterName = "@SearchByname";
                    //search.Value = SearchByname.Text.Trim();

                    //cmd.Parameters.Add(search);

                    ////SqlDataReader dr = cmd.ExecuteReader();

                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //DataTable dt = new DataTable();


                    //dt.Load(dr);
                    //sda.Fill(dt);

                    //customerSRC.DataSource = dt;
                    //customerSRC.DataBind();


                    //Console.WriteLine(dt);
                    //Console.WriteLine(dr);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    //Connection Object Closed
                    conn.Close();
                }
            }

        }

        protected void dtlList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                customerDL.EditItemIndex = e.Item.ItemIndex;
                BindData();
            }
            else if (e.CommandName.Equals("Update"))
            {
                var dataListItem =customerDL.Items[customerDL.EditItemIndex];
                var Name = ((TextBox)dataListItem.FindControl("Name")).Text;
                var Type= ((TextBox)dataListItem.FindControl("Type")).Text;
                var DOB = ((TextBox)dataListItem.FindControl("DOB")).Text;
                var NumShares = ((TextBox)dataListItem.FindControl("NumShares")).Text;
                var SharePrice = ((TextBox)dataListItem.FindControl("SharePrice")).Text;

                // update operation
                // ... 
                customerDL.EditItemIndex = -1;
                BindData();
            }
        }

        protected void ExportCSV(object sender, EventArgs e)
        {
            //string constr = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblCustomer"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the CSV file data as a Comma separated string.
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                //Add the Header row for CSV file.
                                csv += column.ColumnName + ',';
                            }

                            //Add new line.
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Data rows.
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }

                                //Add new line.
                                csv += "\r\n";
                            }

                            //Download the CSV file.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

    }
}