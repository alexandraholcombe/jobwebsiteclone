using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobWebsiteClone.Models
{
    public class EnvironmentVariables
    {
        public string PlacesKey { get; set; }

        public EnvironmentVariables()
        {
            PlacesKey = "https://maps.googleapis.com/maps/api/js?key=AIzaSyBedLH_agL7CvdxO9C1-D0JMgEGhFG6zIY&libraries=places";
        }
    }
}
