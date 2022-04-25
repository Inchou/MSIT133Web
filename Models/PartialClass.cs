using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT133Web.Models
{
    public class PartialClass
    {
    }
    [ModelMetadataTypeAttribute(typeof(MemberMetadata))]
    public partial class Member
    {

    }

    [ModelMetadataTypeAttribute(typeof(AddressMetadata))]
    public partial class Address
    {

    }
}
