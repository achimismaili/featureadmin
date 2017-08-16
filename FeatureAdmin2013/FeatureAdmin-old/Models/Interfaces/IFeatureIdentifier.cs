using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Models.Interfaces
{
    public interface IFeatureIdentifier
    {
        Guid Id { get; }
        SPFeatureScope Scope { get; }
    }
}
