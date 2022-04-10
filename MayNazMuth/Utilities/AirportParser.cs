﻿using MayNazMuth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MayNazMuth.Utilities
{
    class AirportParser
    {
        public static List<Airport> airportsList = new List<Airport>();
        public static List<Airport> parseAirportFile(String contents)
        {


            //Get the file into Lines
            string[] lines = contents.Split('\n');
            foreach (string line in lines)
            {
                //Cut the line into the fileds
                string[] fields = line.Trim().Split(',');

                if (fields.Length != 7)
                {
                    //MessageBox.Show("Problem parsing file, check format");
                    break;

                }
                else
                {
                    try
                    {                       
                        Airport newAirport = new Airport(                           
                           
                            Convert.ToInt32(fields[0]),
                            fields[1].Trim(),
                            fields[2].Trim(),
                            fields[3].Trim(),
                            fields[4].Trim(),
                            fields[5].Trim(),
                            fields[6].Trim(),
                            fields[7].Trim(),
                            fields[8].Trim()
                            );

                        airportsList.Add(newAirport);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }



            }
           
            return airportsList;
        }
    }
}
