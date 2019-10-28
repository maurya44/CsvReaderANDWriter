using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NLog;

namespace CsvReaderWriter
{

    public class DirWalker
    {

        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static int CountSkippedRows = 0;
        public static int CountValidRows = 0;
        public static string OutputCSVPath = @"D:\MSc CDA\SoftwareDevl\output.csv";
        public static string OutputLogPath = @"D:\MSc CDA\SoftwareDevl\Application.Log";
        public static string inputDataPath = @"C:\Users\Maurya\Downloads\Sample Data";
    

        public void walk(String path)
        {
            int year = 0, month = 0, day = 0;
            StringBuilder st = new StringBuilder();
            StringBuilder sb = new StringBuilder();
                 
            try
            {
                string[] list = Directory.GetDirectories(path);
                if (list == null) return;
                foreach (string dirpath in list)
                {
                    if (Directory.Exists(dirpath))
                    {
                        walk(dirpath);
                        Console.WriteLine("Dir:" + dirpath);
                    }
                }

                string[] filelist = Directory.GetFiles(path, "*.csv");

                int.TryParse(path.Substring(path.LastIndexOf("\\") + 1), out day);
                path = path.Remove(path.LastIndexOf("\\"));
                int.TryParse(path.Substring(path.LastIndexOf("\\") + 1), out month);
                path = path.Remove(path.LastIndexOf("\\"));
                int.TryParse(path.Substring(path.LastIndexOf("\\") + 1), out year);

                foreach (string filepath in filelist)
                {
                    using (var reader = new StreamReader(filepath))
                    using (var csv = new CsvReader(reader))
                    {

                        csv.Read();
                        IEnumerable<Customer> customerinfo = null;
                        csv.Configuration.Delimiter = ",";
                        csv.Configuration.MissingFieldFound = null;
                        //List<Customer> customerList = new List<Customer>();
                        csv.Configuration.HasHeaderRecord = true;
                        if (csv != null)
                        {
                            customerinfo = csv.GetRecords<Customer>();
                        }
                        foreach (Customer cust in customerinfo)
                        {

                            if (!String.IsNullOrEmpty(Convert.ToString(cust)))
                            {
                                try
                                {
                                    string customerInfo = cust.FirstName + "  " + cust.LastName + "  " + cust.StreetNumber + "  " + cust.Street + "  " + cust.City + "  " + cust.Province + "  " + cust.PostalCode + "  " + cust.Country + "  " + cust.PhoneNumber + "  " + cust.EmailAddress;

                                    if (String.IsNullOrWhiteSpace(cust.FirstName))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("First Name is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.LastName))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Last Name is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(Convert.ToString(cust.StreetNumber)))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("StreetNumber is missing in row   " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Street))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Street is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.City))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("City is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Province))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Province is missing in row   " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Country) || cust.Country.Any(char.IsDigit))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Country is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.PostalCode) || !cust.PostalCode.Any(char.IsDigit))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("PostalCode is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace((cust.PhoneNumber)))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Phone Number is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.EmailAddress))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Email Address is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        CountSkippedRows++;
                                        continue;
                                    }
                                    else
                                    {
                                        
                                        sb.AppendLine(cust.FirstName + "," + cust.LastName + "," + cust.StreetNumber + "," + "\"" + cust.Street + "\"" + "," + cust.City + "," + cust.Province + "," + cust.PostalCode + "," + cust.Country + "," + cust.PhoneNumber + "," + cust.EmailAddress + ',' + year + "/" + month + "/" + day);
                                        CountValidRows++;
                                    }


                                }
                                catch (Exception e)
                                {
                                    //using (StreamWriter sw = File.AppendText(OutputLogPath))
                                    //{
                                        st.AppendLine("Execption Log Message :" + e.Message + "StackTrace :" + e.StackTrace);
                                    //}
                                }

                            }
                        }

                        Console.WriteLine("File:" + filepath);
                    }

                }
                if (!File.Exists(OutputCSVPath))
                {
                    File.WriteAllText(OutputCSVPath, sb.ToString());
                }

                File.AppendAllText(OutputCSVPath, sb.ToString());

                if (!File.Exists(OutputLogPath))
                {
                    File.WriteAllText(OutputLogPath, st.ToString());
                }

                File.AppendAllText(OutputLogPath, st.ToString());

            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(OutputLogPath))
               {
                    Log("Exeception Log Message : " + ex.Message + "StackTrace :" + ex.StackTrace,sw);
               }
            }
        }
        public static void Log(string logmessage, TextWriter tw)
        {

            tw.WriteLine("\r\n  Log File Entry");
            tw.WriteLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
            tw.WriteLine($" : {logmessage}");
            tw.WriteLine("-------------------------------------");

        }
        public static void Main(string[] args)
        {
            DirWalker dw = new DirWalker();
            DateTime startTime = DateTime.Now;
            StringBuilder header = new StringBuilder();
            try
            {
                header.AppendLine("FirstName" + "," + "LastName" + "," + "StreetNumber" + "," + "Street" + "," + "City" + "," + "Province" + "," + "PostalCode" + "," + "Country" + "," + "PhoneNumber" + "," + "EmailAddress" + "," + "Date");

                if (!File.Exists(OutputCSVPath))
                {
                    File.WriteAllText(OutputCSVPath, header.ToString());
                }
                else
                {
                    File.AppendAllText(OutputCSVPath, header.ToString());
                }

                dw.walk(inputDataPath);

                DateTime endTime = DateTime.Now;
                using (StreamWriter sw = File.AppendText(OutputLogPath))
                {
                Log("Total execution time in minutes : " + (endTime - startTime).TotalMinutes + "    Total Skipped Rows : " + CountSkippedRows + "    Total Valid Rows : " + CountValidRows,sw);
                }

            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(OutputLogPath)) 
                {
                    Log("Exception while processing : " + ex.Message + " Stacktrace : " + ex.StackTrace,sw);
                }
            }

        }
    }
}
