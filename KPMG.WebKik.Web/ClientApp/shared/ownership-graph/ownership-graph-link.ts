import { OwnershipGraphNode } from './ownership-graph-node';

export class OwnershipGraphLink {
    constructor(public source: OwnershipGraphNode, public target: OwnershipGraphNode, public share: number) {
    }

    get sourceX(): number {
        return this.getConnectionPointX(this.source, this.target);
    }

    get sourceY(): number {
        return this.getConnectionPointY(this.source, this.target);
    }

    get targetX(): number {
        return this.getConnectionPointX(this.target, this.source);
    }

    get targetY(): number {
        return this.getConnectionPointY(this.target, this.source);
    }

    private getConnectionPointX(source: OwnershipGraphNode, target: OwnershipGraphNode): number {
        const side = this.getConnectionSide(source, target);
        switch (side) {
            case ConnectionSide.Left: return source.x;
            case ConnectionSide.Right: return source.x2;
            case ConnectionSide.Top: return source.x + source.width / 2;
            case ConnectionSide.Bottom: return source.x + source.width / 2;
            default:
                return source.x + source.width / 2;
        }
    }

    private getConnectionPointY(source: OwnershipGraphNode, target: OwnershipGraphNode): number {
        const side = this.getConnectionSide(source, target);
        switch (side) {
            case ConnectionSide.Top: return source.y;
            case ConnectionSide.Bottom: return source.y2;
            case ConnectionSide.Left: return source.y + source.height / 2;
            case ConnectionSide.Right: return source.y + source.height / 2;
            default:
                return source.y + source.height / 2;
        }
    }

    private getConnectionSide(source: OwnershipGraphNode, target: OwnershipGraphNode): ConnectionSide {
        let result: ConnectionSide;

        if (source.x > target.x2) { result = ConnectionSide.Left }
        else if (source.x2 < target.x) { result = ConnectionSide.Right }
        else if (source.y < target.y2) { result = ConnectionSide.Bottom }
        else if (source.y2 > target.y) { result = ConnectionSide.Top }
        else { result = ConnectionSide.Center; }

        return result;
    }
}

enum ConnectionSide {
    Left = 1,
    Right,
    Top,
    Bottom,
    Center
}
