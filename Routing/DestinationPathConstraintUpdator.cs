// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestinationPathConstraintUpdator.cs" company="Zaust">
//   Copyright (©)2013, zaust.com. All rights reserved.
// </copyright>
// <summary>
//   updater for des constraints
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Summit.Core.Routing 
{
    using System.Linq;

    using JetBrains.Annotations;

    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Aspects;
    using Orchard.Environment;
    using Orchard.Tasks;

    using Summit.Core.Services;

    [UsedImplicitly]
    public class DestinationPathConstraintUpdator : IOrchardShellEvents, IBackgroundTask {
        private readonly IDestinationPathConstraint destinationPathConstraint;
        private readonly IDestinationService destinationService;

        public DestinationPathConstraintUpdator(IDestinationPathConstraint destinationPathConstraint, IDestinationService destinationService)
        {
            this.destinationPathConstraint = destinationPathConstraint;
            this.destinationService = destinationService;
        }

        void IOrchardShellEvents.Activated() {
            Refresh();
        }

        void IOrchardShellEvents.Terminating() {
        }

        void IBackgroundTask.Sweep() {
            Refresh();
        }

        private void Refresh() {
            destinationPathConstraint.SetPaths(destinationService.Get().Select(b => b.As<IAliasAspect>().Path).ToList());
        }
    }
}