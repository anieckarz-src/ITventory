using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITventory.Domain.Enums;
using ITventory.Domain.ValueObjects;
using ITventory.Shared.Abstractions.Commands;

namespace ITventory.Application.Services.LocationService.AddLocation
{
    public record AddLocation(string name, Guid countryId, ZipCode zipCode, string city, Latitude latitude, Longitude longitude, TypeOfPlant typeOfPlant) : ICommand_;
    
}
