import { OwnershipGraphConfig } from './ownership-graph-config';

export
    class OwnershipGraphNode {
    x: number;
    y: number;

    constructor(private config: OwnershipGraphConfig, public id: number, public displayName: string) { }

    get x2() {
        return this.x + this.config.rectWidth;
    }

    get y2() {
        return this.y + this.config.rectHeight;
    }

    get width() {
        return this.config.rectWidth;
    }

    get height() {
        return this.config.rectHeight;
    }
}
