﻿using Umbraco.Core;
using Vendr.Core.Models;

namespace Vendr.Checkout.Pipeline.Implement
{
    public class InstallPipelineContext
    {
        public int SiteRootNodeId { get; set; }

        public StoreReadOnly Store { get; set; }

        public string CartPageUrl { get; set; }

        public string ConfirmationPageUrl { get; set; }
    }
}