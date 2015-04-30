using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSV_RealEstate
{
    // WHERE TO START?
    // 1. Complete the RealEstateType enumeration
    // 2. Complete the RealEstateSale object.  Fill in all properties, then create the constructor.
    // 3. Complete the GetRealEstateSaleList() function.  This is the function that actually reads in the .csv document and extracts a single row from the document and passes it into the RealEstateSale constructor to create a list of RealEstateSale Objects.
    // 4. Start by displaying the the information in the Main() function by creating lambda expressions.  After you have acheived your desired output, then translate your logic into the function for testing.
    class Program
    {

        static void Main(string[] args)
        {
            List<RealEstateSale> realEstateSaleList = GetRealEstateSaleList();




            
            //Display the average square footage of a Condo sold in the city of Sacramento, 
            //Use the GetAverageSquareFootageByRealEstateTypeAndCity() function.
            Console.WriteLine("Display the average square footage of a Condo sold in the city of Sacramento:");
            Console.WriteLine(GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo,"Sacramento"));
            Console.WriteLine("");

            //Display the total sales of all residential homes in Elk Grove.  Use the GetTotalSalesByRealEstateTypeAndCity() function for testing.
            Console.WriteLine("Display the total sales of all residential homes in Elk Grove");
            Console.WriteLine(string.Format("{0:c}",GetTotalSalesByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Residential, "Elk Grove")));
            Console.WriteLine("");
            //Display the total number of residential homes sold in the zip code 95842.  Use the GetNumberOfSalesByRealEstateTypeAndZip() function for testing.
            Console.WriteLine("Display the total number of residential homes sold in the zip code 95842");
            Console.WriteLine(GetNumberOfSalesByRealEstateTypeAndZip(realEstateSaleList,RealEstateType.Residential,"95842"));
            Console.WriteLine("");

            //Display the average sale price of a lot in Sacramento.  Use the GetAverageSalePriceByRealEstateTypeAndCity() function for testing.
            Console.WriteLine("Display the average sale price of a lot in Sacrament0");
            Console.WriteLine(string.Format("{0:c}",GetAverageSalePriceByRealEstateTypeAndCity(realEstateSaleList,RealEstateType.Lot, "sacramento")));
            Console.WriteLine("");
            //Display the average price per square foot for a condo in Sacramento. Round to 2 decimal places. Use the GetAveragePricePerSquareFootByRealEstateTypeAndCity() function for testing.

            Console.WriteLine("Display the average price per square foot for a condo in Sacramento.");
            Console.WriteLine(string.Format("{0:c}",GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList,RealEstateType.Condo, "sacramento")));
            Console.WriteLine("");
            //Display the number of all sales that were completed on a Wednesday.  Use the GetNumberOfSalesByDayOfWeek() function for testing.
            Console.WriteLine("Display the number of all sales that were completed on a Wednesday");
            Console.WriteLine(string.Format("{0:c}",GetNumberOfSalesByDayOfWeek(realEstateSaleList, DayOfWeek.Wednesday)));
            Console.WriteLine("");

            //Display the average number of bedrooms for a residential home in Sacramento when the 
            // price is greater than 300000.  Round to 2 decimal places.  Use the GetAverageBedsByRealEstateTypeAndCityHigherThanPrice() function for testing.
            Console.WriteLine("Display the average number of bedrooms for a residential home in Sacramento when the price is greater than 3000000");
            Console.WriteLine(GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(realEstateSaleList,RealEstateType.Residential,"sacramento",300000m));
            Console.WriteLine("");
            //Extra Credit:
            //Display top 5 cities by the number of homes sold (using the GroupBy extension)
            // Use the GetTop5CitiesByNumberOfHomesSold() function for testing.
            Console.WriteLine("Display top 5 cities by the number of homes sold (using the GroupBy extension)");
            foreach (string city in GetTop5CitiesByNumberOfHomesSold(realEstateSaleList))
            {
                Console.WriteLine(city);
            }
            Console.WriteLine("");

            Console.ReadKey();

        }



        public static List<RealEstateSale> GetRealEstateSaleList()
        {
         
            //read in the realestatedata.csv file.  As you process each row, you'll add a new 
            // RealEstateData object to the list for each row of the document, excluding the first.  bool skipFirstLine = true;
            List<RealEstateSale> realEstateList = new List<RealEstateSale>();
            // load data
            using (StreamReader reader = new StreamReader("realestatedata.csv"))
            {
                // Get and don't use the first line
                string firstline = reader.ReadLine();
                // Loop through the rest of the lines
                while (!reader.EndOfStream)
                {
                    realEstateList.Add(new RealEstateSale(reader.ReadLine()));
                }
            }
            return realEstateList;
        }

        public static double GetAverageSquareFootageByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city) 
        {
            //Gets the average square foot by real estate type and ciyu
           // return realEstateDataList.Where(x => x.City.ToLower().Contains(city.ToLower())).Where(y => y.Type == realEstateType).Select(z => z.SqFt).Average();
            return realEstateDataList.Where(x => x.City.ToLower() == city.ToLower()).Where(y => y.Type == realEstateType).Average(z => z.SqFt);
        }

        public static decimal GetTotalSalesByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
         //Gets total of sales by real estate type and city   
            return realEstateDataList.Where(x => x.City.ToLower().Contains(city.ToLower())).Where(y => y.Type.ToString().Contains(realEstateType.ToString())).Select(z => z.Price).Sum();
        }

        public static int GetNumberOfSalesByRealEstateTypeAndZip(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string zipcode)
        {

            //Get the number of sales by real estate by type and zip
            //return realEstateDataList.Where(x => x.Zip == zipcode && x.Type == realEstateType).Count();
            return realEstateDataList.Where(x => x.Zip == zipcode).Where(y => y.Type == realEstateType).Count() ;
        }

        
        public static decimal GetAverageSalePriceByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            //Gets average sales price by real estate type and city
            return Math.Round(Convert.ToDecimal( realEstateDataList.Where(x => x.City.ToLower() == city.ToLower()).Where(y => y.Type == realEstateType).Average(z => z.Price).ToString()),2);
        }
        public static decimal GetAveragePricePerSquareFootByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {

         //Get average price type per square foot by real estate type and city
            return Math.Round(realEstateDataList.Where(x => x.City.ToLower() == city.ToLower()).Where(y => y.Type == realEstateType).Average(z => ((decimal)z.Price /(decimal) z.SqFt)),2);
        }

        public static int GetNumberOfSalesByDayOfWeek(List<RealEstateSale> realEstateDataList, DayOfWeek dayOfWeek)
        {
            //Get number of sales by day of the week
            return realEstateDataList.Where(x => x.SaleDate.DayOfWeek == dayOfWeek).Count();
        }

        public static double GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city, decimal price)
        {
            //Must round to 2 decimal points
            //Gets average beds by real estate type higher than price given by user
            //realEstateDataList.Where(x => x.Type == realEstateType && x.City.ToLower() == city.ToLower() && x.Price > x.Price).Average(x => x.Beds);
            return Math.Round( realEstateDataList.Where(x => x.Type == realEstateType).Where(y => (decimal)y.Price > price).Where(z => z.City.ToLower() ==city.ToLower()).Select(u => u.Beds).Average(), 2);
        }

        public static List<string> GetTop5CitiesByNumberOfHomesSold(List<RealEstateSale> realEstateDataList)
        {
            
           //Gets the top 5 cities by number of homes sold
            return realEstateDataList.GroupBy(x => x.City).OrderByDescending(y => y.Count()).Select(t => t.Key).Take(5).ToList();
        }
    }

    public enum RealEstateType
    {
        Residential,
        MultiFamily,
        Condo,
        Lot
        //fill in with enum types: Residential, MultiFamily, Condo, Lot
    }
    class RealEstateSale
    {
        //Create properties, using the correct data types (not all are strings) for all columns of the CSV
        //street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
        // Properties

        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SqFt { get; set; }
        public RealEstateType Type { get; set; }
        public DateTime SaleDate { get; set; }
        public int Price { get; set; }

        //The constructor will take a single string arguement.  This string will be one line of the real estate data.
        // Inside the constructor, you will seperate the values into their corrosponding properties, and do the necessary conversions

        public RealEstateSale (string inputLine)
        {
            // Split using the tab character due to the tab delimited data format
            string[] realEstateData = inputLine.Split(',');

            // Get the time in milliseconds and convert to C# DateTime
           
            this.Street = realEstateData[0];
            this.City = realEstateData[1];
            this.Zip = realEstateData[2];
            this.State = realEstateData[3];
            this.Beds = int.Parse(realEstateData[4]);
            this.Baths = int.Parse(realEstateData[5]);
            this.SqFt = int.Parse(realEstateData[6]);
            //Enumerates the values of real estate type
            switch(realEstateData[7].ToLower())
            {
                case "residential":
                    this.Type = RealEstateType.Residential;
                    break;
                case "multi-family":
                    this.Type = RealEstateType.MultiFamily;
                    break;
                case "condo":
                    this.Type = RealEstateType.Condo;
                    break;

                default:
                    break;


            }
            this.SaleDate = DateTime.Parse(realEstateData[8]);
            this.Price = int.Parse(realEstateData[9]);



            //When computing the RealEstateType, if the square footage is 0, then it is of the Lot type, otherwise, use the string
            // value of the "Type" column to determine its corresponding enumeration type.
            

            if( realEstateData[6] == "0")
            {
                this.Type = RealEstateType.Lot;
               // Console.WriteLine(" {0}  {1} {2} {3}", SqFt, Price, City, Type);
            }
            

        }

  

    }
}
