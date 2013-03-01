// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Permissions.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   summit permissions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core
{
    using System.Collections.Generic;

    using Orchard.Environment.Extensions.Models;
    using Orchard.Security.Permissions;

    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageTaxonomies = new Permission { Description = "Manage taxonomies", Name = "ManageTaxonomies" };
        public static readonly Permission CreateTaxonomy = new Permission { Description = "Create taxonomy", Name = "CreateTaxonomy", ImpliedBy = new[] { ManageTaxonomies } };
        public static readonly Permission ManageTerms = new Permission { Description = "Manage terms", Name = "ManageTerms", ImpliedBy = new[] { CreateTaxonomy } };
        public static readonly Permission CreateTerm = new Permission { Description = "Create term", Name = "CreateTerm", ImpliedBy = new[] { ManageTerms } };

        public static readonly Permission ManageDestinations = new Permission
            { Description = "Manage destinations for others", Name = "ManageDestinations" };

        public static readonly Permission ManageOwnDestinations = new Permission
            {
                Description = "Manage own destinations",
                Name = "ManageOwnDestinations",
                ImpliedBy = new[] { ManageDestinations }
            };

        public static readonly Permission PublishHotel = new Permission
            {
                Description = "Publish or unpublish hotels for others",
                Name = "PublishHotel",
                ImpliedBy = new[] { ManageDestinations }
            };

        public static readonly Permission PublishOwnHotel = new Permission
            {
                Description = "Publish or unpublish own hotels",
                Name = "PublishOwnHotelt",
                ImpliedBy = new[] { PublishHotel, ManageOwnDestinations }
            };

        public static readonly Permission EditHotel = new Permission
            { Description = "Edit hotels for others", Name = "EditHotel", ImpliedBy = new[] { PublishHotel } };

        public static readonly Permission EditOwnHotel = new Permission
            { Description = "Edit own hotels", Name = "EditOwnHotel", ImpliedBy = new[] { EditHotel, PublishOwnHotel } };

        public static readonly Permission DeleteHotel = new Permission
            { Description = "Delete hotels for others", Name = "DeleteHotel", ImpliedBy = new[] { ManageDestinations } };

        public static readonly Permission DeleteOwnHotel = new Permission
            {
                Description = "Delete own hotels",
                Name = "DeleteOwnHotel",
                ImpliedBy = new[] { DeleteHotel, ManageOwnDestinations }
            };

        public static readonly Permission MetaListDestinations = new Permission
            { ImpliedBy = new[] { EditHotel, PublishHotel, DeleteHotel } };

        public static readonly Permission MetaListOwnDestinations = new Permission
            { ImpliedBy = new[] { EditOwnHotel, PublishOwnHotel, DeleteOwnHotel } };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
                {
                    ManageOwnDestinations, ManageDestinations, EditOwnHotel, EditHotel, PublishOwnHotel, PublishHotel,
                    DeleteOwnHotel, DeleteHotel, ManageTaxonomies, CreateTaxonomy
                };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
                {
                    new PermissionStereotype { Name = "Administrator", Permissions = new[] { ManageDestinations, ManageTaxonomies } },
                    new PermissionStereotype
                        { Name = "Editor", Permissions = new[] { ManageTaxonomies, PublishHotel, EditHotel, DeleteHotel } },
                    new PermissionStereotype { Name = "Moderator", },
                    new PermissionStereotype { Name = "Author", Permissions = new[] { ManageOwnDestinations } },
                    new PermissionStereotype { Name = "Contributor", Permissions = new[] { EditOwnHotel } },
                };
        }
    }
}


