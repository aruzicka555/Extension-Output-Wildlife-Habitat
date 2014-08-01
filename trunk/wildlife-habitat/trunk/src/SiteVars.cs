//  Copyright 2005-2010 Portland State University, University of Wisconsin-Madison
//  Authors:  Robert M. Scheller, Jimm Domingo

using Landis.Core;
using Landis.SpatialModeling;
using Landis.Library.BiomassCohorts;
using System.Collections.Generic;

namespace Landis.Extension.Output.WildlifeHabitat
{
    public static class SiteVars
    {
        private static ISiteVar<Landis.Library.BiomassCohorts.ISiteCohorts> biomassCohorts;
        private static ISiteVar<Landis.Library.AgeOnlyCohorts.ISiteCohorts> ageCohorts;

        private static ISiteVar<string> prescriptionName;
        private static ISiteVar<byte> fireSeverity;

        private static ISiteVar<int> yearOfFire;
        private static ISiteVar<int> yearOfHarvest;
        private static ISiteVar<Dictionary<int,int[]>> dominantAge;
        private static ISiteVar<Dictionary<int, int[]>> forestType;
        private static ISiteVar<Dictionary<int, double>> suitabilityValue;

        //---------------------------------------------------------------------

        public static void Initialize()
        {
            biomassCohorts = PlugIn.ModelCore.GetSiteVar<Landis.Library.BiomassCohorts.ISiteCohorts>("Succession.BiomassCohorts");
            ageCohorts = PlugIn.ModelCore.GetSiteVar<Landis.Library.AgeOnlyCohorts.ISiteCohorts>("Succession.AgeCohorts");
            prescriptionName = PlugIn.ModelCore.GetSiteVar<string>("Harvest.PrescriptionName");
            fireSeverity = PlugIn.ModelCore.GetSiteVar<byte>("Fire.Severity");

            yearOfFire = PlugIn.ModelCore.Landscape.NewSiteVar<int>();
            yearOfHarvest = PlugIn.ModelCore.Landscape.NewSiteVar<int>();
            dominantAge = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int,int[]>>();
            forestType = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, int[]>>();
            suitabilityValue = PlugIn.ModelCore.Landscape.NewSiteVar<Dictionary<int, double>>();

            if (biomassCohorts == null && ageCohorts == null)
            {
                string mesg = string.Format("Cohorts are empty.  Please double-check that this extension is compatible with your chosen succession extension.");
                throw new System.ApplicationException(mesg);
            }
            SiteVars.YearOfFire.ActiveSiteValues = -999;
            SiteVars.YearOfHarvest.ActiveSiteValues = -999;
            foreach (Site site in PlugIn.ModelCore.Landscape.ActiveSites)
            {
                Dictionary<int, int[]> domAgeDict = new Dictionary<int, int[]>();
                int[] domAgeArray = new int[2];
                domAgeDict.Add(0, domAgeArray);
                SiteVars.DominantAge[site] = domAgeDict;

                Dictionary<int, int[]> forTypeDict = new Dictionary<int, int[]>();
                int[] forTypeArray = new int[2];
                forTypeDict.Add(0, forTypeArray);
                SiteVars.ForestType[site] = forTypeDict;

                Dictionary<int, double> suitValDict = new Dictionary<int, double>();
                suitValDict.Add(0, 0.0);
                SiteVars.SuitabilityValue[site] = suitValDict;
            }     
        }
       
        //---------------------------------------------------------------------
        public static ISiteVar<Landis.Library.BiomassCohorts.ISiteCohorts> BiomassCohorts
        {
            get
            {
                return biomassCohorts;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<Landis.Library.AgeOnlyCohorts.ISiteCohorts> AgeCohorts
        {
            get
            {
                return ageCohorts;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<string> PrescriptionName
        {
            get
            {
                return prescriptionName;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<byte> FireSeverity
        {
            get
            {
                return fireSeverity;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<int> YearOfFire
        {
            get
            {
                return yearOfFire;
            }
            set
            {
                yearOfFire = value;
            }
        }
        //---------------------------------------------------------------------
        public static ISiteVar<int> YearOfHarvest
        {
            get
            {
                return yearOfHarvest;
            }
            set
            {
                yearOfHarvest = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to an array of this year's and last year's dominant age class
        // [0] is this year, [1] is last year
        public static ISiteVar<Dictionary<int, int[]>> DominantAge
        {
            get
            {
                return dominantAge;
            }
            set
            {
                dominantAge = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to an array of this year's and last year's dominant forest type
        // [0] is this year, [1] is last year
        public static ISiteVar<Dictionary<int, int[]>> ForestType
        {
            get
            {
                return forestType;
            }
            set
            {
                forestType = value;
            }
        }
        //---------------------------------------------------------------------
        // Dictionary with key equal the index of the suitability file and 
        // value equal to the calculated suitability
        public static ISiteVar<Dictionary<int, double>> SuitabilityValue
        {
            get
            {
                return suitabilityValue;
            }
            set
            {
                suitabilityValue = value;
            }
        }
        //---------------------------------------------------------------------
    }
}
