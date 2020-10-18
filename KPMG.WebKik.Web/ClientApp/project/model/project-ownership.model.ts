import { ProjectViewModel } from './project.model';
import { OwnershipGraphNode } from '../../shared/ownership-graph/ownership-graph-node';
import { OwnershipGraphLink } from '../../shared/ownership-graph/ownership-graph-link';

export
    class ProjectOwnershipViewModel {
    public Project: ProjectViewModel;
    public Nodes: Array<{ Id: number, DisplayName: string }>;
    public Links: Array<{ SourceId: number, TargetId: number, Share: number }>;
}
