using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM_Pinewood
{
    public class HelperMethods
    {
        /**
	     * Starting the CRM
	     */
        public static void startFunctions()
        {
            // initialize
            int noOfFunctions = 0;
            string inputStr = "";
            Boolean continuous = true;

            // Showing Menu and the current conditions
            HelperMethods.listMenu();

            // Getting the input value
            inputStr = Console.ReadLine();

            // input no.
            if (string.IsNullOrEmpty(inputStr))
            {
                continuous = false;
            }
            else
            {
                noOfFunctions = Int32.Parse(inputStr);
            }

            while (continuous)
            {
                switch (noOfFunctions)
                {
                    case 1:
                        HelperMethods.setUpCondtions();
                        continuous = false;
                        break;
                    case 2:
                        ooenCSVFile();
                        Console.WriteLine("\nFinished, path= " + GlobalVariables.StoredCSVPath);
                        continuous = false;
                        break;
                    default:
                        continuous = false;
                        break;
                }
            }
        }

        /**
	     * List the menu
	     */
        public static void listMenu()
        {
            Console.WriteLine("[Menu]");
            Console.WriteLine("1: Set conditions");
            Console.WriteLine("2: Output CSV file (with default conditions");
            Console.WriteLine("others: Exist");
            // List the current conditions
            listCurrConditions();
            Console.WriteLine("Choosing which number of function to do..\n");
        }

        /**
	     * List the current conditions
	     */
        public static void listCurrConditions()
        {
            Console.WriteLine("\n");
            Console.WriteLine("[Current Conditions:]");
            Console.WriteLine("1. Age from: " + GlobalVariables.con_minAges +
                              " to " + GlobalVariables.con_maxAges);
            Console.WriteLine("2. Registered Date: " + GlobalVariables.con_registeredDate.ToString("dd/MM/yyyy "));
            Console.WriteLine("3. Engine Size: " + GlobalVariables.con_engineSize);
            Console.WriteLine("\n");
        }

        /**
	     * Settings of engine size
	     * 
	     * @param new_engineSize - from user entered
	     */
        static void ooenCSVFile()
        {
            // initialize
            List<CustomerInfo> CustomerInfoList = new List<CustomerInfo>();
            List<CustomerInfo> OutputCustomerInfoList = new List<CustomerInfo>();

            // Open CSV file
            using (var reader = new StreamReader(GlobalVariables.CustomerInformationPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    CustomerInfoList.Add(
                        new CustomerInfo()
                        {
                            CustomerId = values[0],
                            Forename = values[1],
                            Surname = values[2],
                            DateOfBirth = values[3],
                            VehicleId = values[4],
                            RegistrationNumber = values[5],
                            Manufacturer = values[6],
                            Model = values[7],
                            EngineSize = values[8],
                            RegistationDate = values[9],
                            InteriorColour = values[10],
                            HasHelmetStorage = values[11],
                            VehicleType = values[12]
                        });
                }
            }

            // deal with the column
            foreach (var data in CustomerInfoList.Skip(1))
            {
                if (((HelperMethods.getAgeByBirthdate(data.DateOfBirth) >= GlobalVariables.con_minAges) && (HelperMethods.getAgeByBirthdate(data.DateOfBirth) <= GlobalVariables.con_maxAges)) &&
                     (DateTime.Compare(HelperMethods.formatStrToDateTime(data.RegistationDate), GlobalVariables.con_registeredDate) >= 0) &&
                     (Int32.Parse(data.EngineSize) > GlobalVariables.con_engineSize)
                       )
                {
                    OutputCustomerInfoList.Add(
                        new CustomerInfo()
                        {
                            CustomerId = data.CustomerId,
                            Forename = data.Forename,
                            Surname = data.Surname,
                            DateOfBirth = data.DateOfBirth,
                            VehicleId = data.VehicleId,
                            RegistrationNumber = data.RegistrationNumber,
                            Manufacturer = data.Manufacturer,
                            Model = data.Model,
                            EngineSize = data.EngineSize,
                            RegistationDate = data.RegistationDate,
                            InteriorColour = data.InteriorColour,
                            HasHelmetStorage = data.HasHelmetStorage,
                            VehicleType = data.VehicleType
                        });
                }
            }

            HelperMethods.outputToCSV(GlobalVariables.StoredCSVPath, OutputCustomerInfoList);
        }

        /**
	     * Calculate the age from birthday
	     * 
	     * @param str_Birthdate - the value from csv file
	     * @return - age
	     */
        public static int getAgeByBirthdate(string str_Birthdate)
        {
            DateTime birthdate = DateTime.Parse(str_Birthdate);

            DateTime now = DateTime.Now;
            int age = now.Year - birthdate.Year;
            if (now.Month < birthdate.Month || (now.Month == birthdate.Month && now.Day < birthdate.Day))
            {
                age--;
            }
            return age < 0 ? 0 : age;
        }

        /**
	     * Format the Date from string to Datetime
	     * 
	     * @param Date - the value from csv file
	     * @return - NewDate (Datetime type)
	     */
        public static DateTime formatStrToDateTime(string Date)
        {
            DateTime NewDate = DateTime.Parse(Date);
            return NewDate;
        }

        /**
	     * Output the csv file as report
	     * 
	     * @param storedPath - the path for storing csv file from GlobalVariables.cs
         * @param data - the List of customers' infomation
	     */
        public static void outputToCSV(String storedPath, List<CustomerInfo> data)
        {
            using (var file = new StreamWriter(storedPath))
            {
                // writing customer column
                file.WriteLine(GlobalVariables.customerColumnValue);

                foreach (var element in data)
                {
                    file.WriteLineAsync($"{element.CustomerId}," +
                                        $"{element.Forename}," +
                                        $"{element.Surname}," +
                                        $"{element.DateOfBirth}," +
                                        $"{element.VehicleId}," +
                                        $"{element.RegistrationNumber}," +
                                        $"{element.Manufacturer}," +
                                        $"{element.Model}," +
                                        $"{element.EngineSize}," +
                                        $"{element.RegistationDate}," +
                                        $"{element.InteriorColour}," +
                                        $"{element.HasHelmetStorage}," +
                                        $"{element.VehicleType}"
                                        );
                }
            }
        }

        /**
	     * Setting the conditions
	     */
        public static void setUpCondtions()
        {
            // initialize
            int chosenCon = 0;
            string inputStr = "";
            string typeVar = "";
            Boolean continuous = true;

            HelperMethods.listCurrConditions();
            Console.WriteLine("0. Back the menu");
            Console.WriteLine("Enter which number of condition you would like to change: (no comment)");

            // input no.
            inputStr = Console.ReadLine();
            if (string.IsNullOrEmpty(inputStr))
            {
                continuous = false;
            }
            else
            {

                chosenCon = Int32.Parse(inputStr);
            }

            while (continuous)
            {
                switch (chosenCon)
                {
                    case 0:
                        continuous = false;
                        break;
                    case 1:
                        Console.WriteLine("Enter the new range: \n(like from 20 to 40, type '20,40' pls)");
                        typeVar = Console.ReadLine();
                        String[] NewAges = typeVar.Split(',');
                        GlobalVariables.setCon_minAges(NewAges[0]);
                        GlobalVariables.setCon_maxAges(NewAges[1]);

                        Console.WriteLine("New Age from: " + GlobalVariables.con_minAges +
                                          " to " + GlobalVariables.con_maxAges);
                        continuous = false;
                        break;
                    case 2:
                        Console.WriteLine("Enter the new date: \n(like 2019-12-31)");
                        typeVar = Console.ReadLine();
                        GlobalVariables.setCon_registeredDate(typeVar);

                        Console.WriteLine("New Registered Date: " + GlobalVariables.con_registeredDate.ToString("dd/MM/yyyy "));
                        continuous = false;
                        break;
                    case 3:
                        Console.WriteLine("Enter the new engine size:");
                        typeVar = Console.ReadLine();
                        GlobalVariables.setcon_engineSize(typeVar);
                        Console.WriteLine("New Engine Size: " + GlobalVariables.con_engineSize);
                        continuous = false;
                        break;
                    default:
                        continuous = false;
                        break;
                }
            }
        }

    }
}
