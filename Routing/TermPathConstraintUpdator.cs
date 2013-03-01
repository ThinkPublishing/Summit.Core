using Summit.Core.Services;
using Orchard;
using Orchard.Environment;

namespace Summit.Core.Routing {
    public interface ITermPathConstraintUpdator : IDependency {
        void Refresh();
    }

    public class TermPathConstraintUpdator : ITermPathConstraintUpdator, IOrchardShellEvents {
        private readonly ITermPathConstraint _termPathConstraint;
        private readonly ITaxonomyService _taxonomyService;

        public TermPathConstraintUpdator(ITermPathConstraint termPathConstraint, ITaxonomyService taxonomyService) {
            _termPathConstraint = termPathConstraint;
            _taxonomyService = taxonomyService;
        }
        
        void IOrchardShellEvents.Activated() {
            Refresh();
        }

        void IOrchardShellEvents.Terminating() {
        }

        public void Refresh() {
            _termPathConstraint.SetPaths(_taxonomyService.GetTermPaths());
        }
    }
}