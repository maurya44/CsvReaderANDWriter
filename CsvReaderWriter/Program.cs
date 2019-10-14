using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvReaderWriter
{

    public class DirWalker
    {
        public static int countSkippedRows = 0;
        public static int countValidRows = 0;
        public static string outputCSVPath = @"D:\MSc CDA\SoftwareDevl\output.csv";
        public static string outputLogPath = @"D:\MSc CDA\SoftwareDevl\log.txt";
        public static string inputDataPath = @"C:\Users\Maurya\Downloads\Sample Data";
        public class Customer
        {
            [Index(0)]
            public string FirstName { get; set; }
            [Index(1)]
            public string LastName { get; set; }
            [Index(2)]
            public int StreetNumber { get; set; }
            [Index(3)]
            public string Street { get; set; }
            [Index(4)]
            public string City { get; set; }
            [Index(5)]
            public string Province { get; set; }
            [Index(6)]
            public string PostalCode { get; set; }
            [Index(7)]
            public string Country { get; set; }
            [Index(8)]
            public string PhoneNumber { get; set; }
            [Index(9)]
            public string EmailAddress { get; set; }
        }

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

                        List<Customer> customerList = new List<Customer>();
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
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.LastName))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Last Name is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(Convert.ToString(cust.StreetNumber)))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("StreetNumber is missing in row   " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Street))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Street is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.City))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("City is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Province))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Province is missing in row   " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.Country) || cust.Country.Any(char.IsDigit))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Country is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.PostalCode) || !cust.PostalCode.Any(char.IsDigit))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("PostalCode is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(Convert.ToString(cust.PhoneNumber)))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Phone Number is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else if (String.IsNullOrWhiteSpace(cust.EmailAddress))
                                    {
                                        st.AppendLine("\r\n  Log File Entry");
                                        st.AppendLine($"{DateTime.Now.ToLongTimeString()}   { DateTime.Now.ToLongDateString()}");
                                        st.AppendLine("Email Address is missing in row    " + customerInfo + "\n");
                                        st.AppendLine("-------------------------------------");
                                        countSkippedRows++;
                                        continue;
                                    }
                                    else
                                    {
                                        sb.AppendLine(cust.FirstName + "," + cust.LastName + "," + cust.StreetNumber + "," + "\"" + cust.Street + "\"" + "," + cust.City + "," + cust.Province + "," + cust.PostalCode + "," + cust.Country + "," + cust.PhoneNumber + "," + cust.EmailAddress + ',' + year + "/" + month + "/" + day);
                                        countValidRows++;
                                    }


                                }
                                catch (Exception e)
                                {
                                    using (StreamWriter sw = File.AppendText(outputLogPath))
                                    {
                                        Log("Execption Log Message :" + e.Message + "StackTrace :" + e.StackTrace, sw);
                                    }
                                }

                            }
                        }

                        Console.WriteLine("File:" + filepath);
                    }

                }
                if (!File.Exists(outputCSVPath))
                {
                    File.WriteAllText(outputCSVPath, sb.ToString());
                }
               
                    File.AppendAllText(outputCSVPath, sb.ToString());
                
                if (!File.Exists(outputLogPath))
                {
                    File.WriteAllText(outputLogPath, st.ToString());
                }
                
                    File.AppendAllText(outputLogPath, st.ToString());
                
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(outputLogPath))
                {
                    Log("Execption Log Message : " + ex.Message + "StackTrace :" + ex.StackTrace, sw);
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

            header.AppendLine("FirstName" + "," + "LastName" + "," + "StreetNumber" + "," + "Street" + "," + "City" + "," + "Province" + "," + "PostalCode" + "," + "Country" + "," + "PhoneNumber" + "," + "EmailAddress" + "," + "Date");

            if (!File.Exists(outputCSVPath))
            {
                File.WriteAllText(outputCSVPath, header.ToString());
            }
            else
            { 
                File.AppendAllText(outputCSVPath, header.ToString());
            }

            dw.walk(inputDataPath);

            DateTime endTime = DateTime.Now;
            TimeSpan difference = endTime - startTime;
            using (StreamWriter sw = File.AppendText(outputLogPath))
            {
                Log("Total execution time in minutes : " + difference.TotalMinutes + "    Total Skipped Rows : " + countSkippedRows + "    Total Valid Rows : " + countValidRows, sw);
            }
        }0

    }
}
