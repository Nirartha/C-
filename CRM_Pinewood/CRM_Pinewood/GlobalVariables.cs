using System;
using System.Collections.Generic;

namespace CRM_Pinewood
{
    public class GlobalVariables
    {
        // customer column
        public static string customerColumnValue =
            string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}",
            "CustomerId",
            "Forename",
            "Surname",
            "DateOfBirth",
            "VehicleId",
            "RegistrationNumber",
            "Manufacturer",
            "Model",
            "EngineSize",
            "RegistationDate",
            "InteriorColour",
            "HasHelmetStorage",
            "VehicleType");

        // initialize default path
        public static string CustomerInformationPath = "..//..//..//CustomerInfo//CustomerInformation.csv";
        public static string StoredCSVPath = "..//..//..//CustomerInfo//OutputCustomerInformation.csv";

        // initialize default conditions
        public static int con_minAges = 20;
        public static int con_maxAges = 30;
        public static DateTime con_registeredDate = new DateTime(2010, 1, 1);
        public static int con_engineSize = 1100;

        /**
	     * Settings of mimAges
	     * 
	     * @param new_minAges - from user entered
	     */
        public static void setCon_minAges(string new_minAges)
        {
            GlobalVariables.con_minAges = Int32.Parse(new_minAges);
        }

        /**
	     * Settings of maxAges
	     * 
	     * @param new_maxAges - from user entered
	     */
        public static void setCon_maxAges(string new_maxAges)
        {
            GlobalVariables.con_maxAges = Int32.Parse(new_maxAges);
        }

        /**
	     * Settings of registered Date
	     * 
	     * @param new_registeredDate - from user entered
	     */
        public static void setCon_registeredDate(string new_registeredDate)
        {
            String[] NewregisteredDate = new_registeredDate.Split('-');
            int year = Int32.Parse(NewregisteredDate[0]);
            int month = Int32.Parse(NewregisteredDate[1]);
            int day = Int32.Parse(NewregisteredDate[2]);

            GlobalVariables.con_registeredDate = new DateTime(year, month, day);
        }
        
        /**
	     * Settings of engine size
	     * 
	     * @param new_engineSize - from user entered
	     */
        public static void setcon_engineSize(string new_engineSize)
        {
            GlobalVariables.con_engineSize = Int32.Parse(new_engineSize);
        }
    }
}
